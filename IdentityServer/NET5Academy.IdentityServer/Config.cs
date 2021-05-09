using IdentityServer4;
using IdentityServer4.Models;
using NET5Academy.Shared.Constants;
using System.Collections.Generic;

namespace NET5Academy.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {

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
                    ClientId = OkIdentityConstans.ClientInfo.Id,
                    ClientName = OkIdentityConstans.ClientInfo.Name,
                    ClientSecrets = { new Secret(OkIdentityConstans.ClientInfo.Secret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { IdentityServerConstants.LocalApi.ScopeName, OkIdentityConstans.ScopeName.CatalogAPI, OkIdentityConstans.ScopeName.PhotoStockAPI }
                }
            };
    }
}