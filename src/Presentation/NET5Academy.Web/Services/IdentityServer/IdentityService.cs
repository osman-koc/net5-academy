using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NET5Academy.Shared.Constants;
using NET5Academy.Shared.Models;
using NET5Academy.Web.Models.Config;
using NET5Academy.Web.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public class IdentityService : IIdentityService
    {
        private const string AUTH_SCHEMA = CookieAuthenticationDefaults.AuthenticationScheme;

        private readonly HttpClient _httpClient;
        private readonly ApiGatewaySettings _gatewaySettings;
        private readonly IHttpContextAccessor _contextAccessor;
        public IdentityService(HttpClient httpClient, ApiGatewaySettings gatewaySettings, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _gatewaySettings = gatewaySettings;
            _contextAccessor = contextAccessor;
        }

        public async Task<OkResponse<bool>> SignInAsync(SignInModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return OkResponse<bool>.Error(HttpStatusCode.BadRequest, "Model is not valid");
            }

            DiscoveryDocumentResponse discovery = await GetDiscoveryDocumentAsync();
            if (discovery == null || discovery.IsError)
            {
                return OkResponse<bool>.Error(discovery.HttpStatusCode, discovery?.Exception?.Message ?? "GetDiscoveryDocument error");
            }

            TokenResponse token = await GetTokenAsync(discovery.TokenEndpoint, model.Email, model.Password);
            if (token == null || token.IsError)
            {
                return OkResponse<bool>.Error(token.HttpStatusCode, token?.Exception?.Message ?? "RequestPasswordToken error");
            }

            UserInfoResponse userInfo = await GetUserInfoAsync(discovery.UserInfoEndpoint, token.AccessToken);
            if (userInfo == null || userInfo.IsError)
            {
                return OkResponse<bool>.Error(userInfo.HttpStatusCode, userInfo?.Exception?.Message ?? "GetUserInfo error");
            }

            ClaimsPrincipal claimsPrincipal = GetClaimsPrincipal(userInfo.Claims);
            AuthenticationProperties authProperties = GetAuthenticationProperties(token.AccessToken, token.RefreshToken, token.ExpiresIn, model.RememberMe);

            await _contextAccessor.HttpContext.SignInAsync(AUTH_SCHEMA, claimsPrincipal, authProperties);
            return OkResponse<bool>.Success(HttpStatusCode.OK, true);
        }

        public async Task<TokenResponse> GetAccessTokenByRefreshTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }


        #region PrivateMethods
        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync()
        {
            var discoverRequest = new DiscoveryDocumentRequest
            {
                Address = _gatewaySettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            };

            return await _httpClient.GetDiscoveryDocumentAsync(discoverRequest);
        }

        private async Task<TokenResponse> GetTokenAsync(string tokenEndpoint, string userName, string password)
        {
            var tokenRequest = new PasswordTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = OkIdentityConstants.Clients.WebMvcClientForUser.Id,
                ClientSecret = OkIdentityConstants.Clients.WebMvcClientForUser.Secret,
                UserName = userName,
                Password = password
            };

            return await _httpClient.RequestPasswordTokenAsync(tokenRequest);
        }

        private async Task<UserInfoResponse> GetUserInfoAsync(string userInfoEndpoint, string accessToken)
        {
            var userInfoRequest = new UserInfoRequest
            {
                Address = userInfoEndpoint,
                Token = accessToken,
            };

            return await _httpClient.GetUserInfoAsync(userInfoRequest);
        }

        private AuthenticationProperties GetAuthenticationProperties(string accessToken, string refreshToken, int expiresIn, bool isPersistent)
        {
            var authTokens = new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = accessToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = refreshToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.Now.AddSeconds(expiresIn).ToString("O", CultureInfo.InvariantCulture) },
            };

            var authProperties = new AuthenticationProperties();
            authProperties.StoreTokens(authTokens);
            authProperties.IsPersistent = isPersistent;

            return authProperties;
        }

        private ClaimsPrincipal GetClaimsPrincipal(IEnumerable<Claim> userClaims)
        {
            var identityClaims = new ClaimsIdentity(userClaims, AUTH_SCHEMA, "name", "role");
            return new ClaimsPrincipal(identityClaims);
        }
        #endregion
    }
}
