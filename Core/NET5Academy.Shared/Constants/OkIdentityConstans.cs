using NET5Academy.Shared.Models;

namespace NET5Academy.Shared.Constants
{
    public class OkIdentityConstans
    {
        public const string UserIdKey = "sub";

        public static class ScopeName
        {
            public const string Roles = "roles";
            public const string CatalogAPI = "CatalogApi";
            public const string PhotoStockAPI = "PhotoStockApi";
        }
        public static class ScopeDisplay
        {
            public const string IdentityAPI = "IdentityServer API";
            public const string CatalogAPI = "Catalog API full permission";
            public const string PhotoStockAPI = "PhotoStock API full permission";
        }

        public static class ResourceName
        {
            public const string CatalogAPI = "resource_catalog";
            public const string PhotoStockAPI = "resource_photostock";
        }

        public static class Clients
        {
            public static readonly OkClient WebMvcClient = new OkClient("WebMvcClient", "Asp.Net Core MVC", "secret");
            public static readonly OkClient WebMvcClientForUser = new OkClient("WebMvcClientForUser", "Asp.Net Core MVC", "secret");
        }

        public static class ClaimName
        {
            public const string Role = "role";
        }
    }
}
