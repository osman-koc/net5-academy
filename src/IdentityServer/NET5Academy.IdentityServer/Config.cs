using IdentityServer4;
using IdentityServer4.Models;
using NET5Academy.Shared.Constants;
using System;
using System.Collections.Generic;

namespace NET5Academy.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.OpenId(), //sub keyword
                       new IdentityResources.Email(),
                       new IdentityResources.Profile(),
                       new IdentityResource()
                       {
                           Name = OkIdentityConstants.ScopeName.Roles,
                           DisplayName = "Roles",
                           Description = "Identity user roles",
                           UserClaims = new []{ OkIdentityConstants.ClaimName.Role }
                       },
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName, OkIdentityConstants.ScopeDisplay.IdentityAPI),
                new ApiScope(OkIdentityConstants.ScopeName.CatalogAPI, OkIdentityConstants.ScopeDisplay.CatalogAPI),
                new ApiScope(OkIdentityConstants.ScopeName.PhotoStockAPI, OkIdentityConstants.ScopeDisplay.PhotoStockAPI),
                new ApiScope(OkIdentityConstants.ScopeName.BasketAPI, OkIdentityConstants.ScopeDisplay.BasketAPI),
                new ApiScope(OkIdentityConstants.ScopeName.DiscountAPI, OkIdentityConstants.ScopeDisplay.DiscountAPI),
                new ApiScope(OkIdentityConstants.ScopeName.OrderAPI, OkIdentityConstants.ScopeDisplay.OrderAPI),
                new ApiScope(OkIdentityConstants.ScopeName.PaymentAPI, OkIdentityConstants.ScopeDisplay.PaymentAPI),
                new ApiScope(OkIdentityConstants.ScopeName.ApiGateway, OkIdentityConstants.ScopeDisplay.ApiGateway),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
                new ApiResource(OkIdentityConstants.ResourceName.CatalogAPI) { Scopes = { OkIdentityConstants.ScopeName.CatalogAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.PhotoStockAPI) { Scopes = { OkIdentityConstants.ScopeName.PhotoStockAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.BasketAPI) { Scopes = { OkIdentityConstants.ScopeName.BasketAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.DiscountAPI) { Scopes = { OkIdentityConstants.ScopeName.DiscountAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.OrderAPI) { Scopes = { OkIdentityConstants.ScopeName.OrderAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.PaymentAPI) { Scopes = { OkIdentityConstants.ScopeName.PaymentAPI } },
                new ApiResource(OkIdentityConstants.ResourceName.ApiGateway) { Scopes = { OkIdentityConstants.ScopeName.ApiGateway } },
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = OkIdentityConstants.Clients.WebMvcClient.Id,
                    ClientName = OkIdentityConstants.Clients.WebMvcClient.Name,
                    ClientSecrets = { new Secret(OkIdentityConstants.Clients.WebMvcClient.Secret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        IdentityServerConstants.LocalApi.ScopeName,
                        OkIdentityConstants.ScopeName.CatalogAPI,
                        OkIdentityConstants.ScopeName.PhotoStockAPI,
                        OkIdentityConstants.ScopeName.ApiGateway,
                    }
                },
                new Client
                {
                    ClientId = OkIdentityConstants.Clients.WebMvcClientForUser.Id,
                    ClientName = OkIdentityConstants.Clients.WebMvcClientForUser.Name,
                    ClientSecrets = { new Secret(OkIdentityConstants.Clients.WebMvcClient.Secret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName, //required for identity
                        IdentityServerConstants.StandardScopes.OpenId, //required (open id connect protocol)
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess, //for refresh token

                        OkIdentityConstants.ScopeName.Roles,
                        OkIdentityConstants.ScopeName.BasketAPI,
                        OkIdentityConstants.ScopeName.DiscountAPI,
                        OkIdentityConstants.ScopeName.OrderAPI,
                        OkIdentityConstants.ScopeName.PaymentAPI,
                        OkIdentityConstants.ScopeName.ApiGateway,
                    },
                    AccessTokenLifetime = 3*3600, //3 hour
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, //60 days
                    RefreshTokenUsage = TokenUsage.ReUse,
                }
            };
    }
}