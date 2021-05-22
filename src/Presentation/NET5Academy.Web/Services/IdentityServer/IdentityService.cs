using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
        private readonly OkServiceSettings _okServiceSettings;
        private readonly IHttpContextAccessor _contextAccessor;
        public IdentityService(HttpClient httpClient, IOptions<OkServiceSettings> ossOptions, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _okServiceSettings = ossOptions.Value;
            _contextAccessor = contextAccessor;
        }

        public async Task<OkResponse<bool>> SignInAsync(SignInModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return OkResponse<bool>.Error(HttpStatusCode.BadRequest, "Model is not valid");
            }

            var discovery = await GetDiscoveryDocumentAsync();
            if (discovery == null || discovery.IsError)
            {
                return OkResponse<bool>.Error(discovery.HttpStatusCode, discovery?.Error ?? "GetDiscoveryDocument error");
            }

            var token = await GetTokenAsync(discovery.TokenEndpoint, model.Email, model.Password);
            if (token == null || token.IsError)
            {
                return OkResponse<bool>.Error(token.HttpStatusCode, token?.ErrorDescription ?? "RequestPasswordToken error");
            }

            var userInfo = await GetUserInfoAsync(discovery.UserInfoEndpoint, token.AccessToken);
            if (userInfo == null || userInfo.IsError)
            {
                return OkResponse<bool>.Error(userInfo.HttpStatusCode, userInfo?.Error ?? "GetUserInfo error");
            }

            await LoginAsync(userInfo, token, model.RememberMe);
            return OkResponse<bool>.Success(HttpStatusCode.OK, true);
        }

        public async Task<TokenResponse> GetAccessTokenByRefreshTokenAsync()
        {
            var discovery = await GetDiscoveryDocumentAsync();
            if (discovery == null || discovery.IsError)
            {
                return TokenResponse.FromException<TokenResponse>(discovery.Exception, discovery?.Error ?? "GetDiscoveryDocument error");
            }

            var token = await GetTokenWithRefreshTokenAsync(discovery.TokenEndpoint);
            if (token != null && !token.IsError)
            {
                await LoginWithRefreshTokenAsync(token);
            }

            return token;
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
                Address = _okServiceSettings.IdentityServerUri,
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
        private async Task<TokenResponse> GetTokenWithRefreshTokenAsync(string tokenEndpoint)
        {
            string refreshToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = OkIdentityConstants.Clients.WebMvcClientForUser.Id,
                ClientSecret = OkIdentityConstants.Clients.WebMvcClientForUser.Secret,
                RefreshToken = refreshToken,
                Address = tokenEndpoint,
            };

            return await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
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

        private List<AuthenticationToken> GetAuthTokens(string accessToken, string refreshToken, int expiresIn)
        {
            return new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = accessToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = refreshToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.Now.AddSeconds(expiresIn).ToString("O", CultureInfo.InvariantCulture) },
            };
        }
        private async Task LoginAsync(UserInfoResponse userInfo, TokenResponse token, bool rememberMe)
        {
            var claimsPrincipal = GetClaimsPrincipal(userInfo.Claims);
            var authTokens = GetAuthTokens(token.AccessToken, token.AccessToken, token.ExpiresIn);

            var authProperties = new AuthenticationProperties();
            authProperties.StoreTokens(authTokens);
            authProperties.IsPersistent = rememberMe;

            await _contextAccessor.HttpContext.SignInAsync(AUTH_SCHEMA, claimsPrincipal, authProperties);
        }
        private async Task LoginWithRefreshTokenAsync(TokenResponse token)
        {
            var authTokens = GetAuthTokens(token.AccessToken, token.RefreshToken, token.ExpiresIn);
            
            var authenticationResult = await _contextAccessor.HttpContext.AuthenticateAsync();
            var authProps = authenticationResult?.Properties;
            if (authProps != null)
            {
                authProps.StoreTokens(authTokens);
            }

            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal, authProps);
        }

        private ClaimsPrincipal GetClaimsPrincipal(IEnumerable<Claim> userClaims)
        {
            var identityClaims = new ClaimsIdentity(userClaims, AUTH_SCHEMA, "name", "role");
            return new ClaimsPrincipal(identityClaims);
        }
        #endregion
    }
}
