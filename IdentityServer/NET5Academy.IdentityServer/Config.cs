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
                           Name = OkIdentityConstans.ScopeName.Roles,
                           DisplayName = "Roles",
                           Description = "Identity user roles",
                           UserClaims = new []{ OkIdentityConstans.ClaimName.Role }
                       },
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName, OkIdentityConstans.ScopeDisplay.IdentityAPI),
                new ApiScope(OkIdentityConstans.ScopeName.CatalogAPI, OkIdentityConstans.ScopeDisplay.CatalogAPI),
                new ApiScope(OkIdentityConstans.ScopeName.PhotoStockAPI, OkIdentityConstans.ScopeDisplay.PhotoStockAPI),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
                new ApiResource(OkIdentityConstans.ResourceName.CatalogAPI) { Scopes = { OkIdentityConstans.ScopeName.CatalogAPI } },
                new ApiResource(OkIdentityConstans.ResourceName.PhotoStockAPI) { Scopes = { OkIdentityConstans.ScopeName.PhotoStockAPI } },
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = OkIdentityConstans.Clients.WebMvcClient.Id,
                    ClientName = OkIdentityConstans.Clients.WebMvcClient.Name,
                    ClientSecrets = { new Secret(OkIdentityConstans.Clients.WebMvcClient.Secret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { IdentityServerConstants.LocalApi.ScopeName, OkIdentityConstans.ScopeName.CatalogAPI, OkIdentityConstans.ScopeName.PhotoStockAPI }
                },
                new Client
                {
                    ClientId = OkIdentityConstans.Clients.WebMvcClientForUser.Id,
                    ClientName = OkIdentityConstans.Clients.WebMvcClientForUser.Name,
                    ClientSecrets = { new Secret(OkIdentityConstans.Clients.WebMvcClient.Secret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess, //for refresh token
                        OkIdentityConstans.ScopeName.Roles
                    },
                    AccessTokenLifetime = 3*3600, //3 hour
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, //60 days
                    RefreshTokenUsage = TokenUsage.ReUse,
                }
            };
    }
}