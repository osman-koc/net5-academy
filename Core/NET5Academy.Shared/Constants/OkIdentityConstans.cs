namespace NET5Academy.Shared.Constants
{
    public class OkIdentityConstans
    {
        public static class ScopeName
        {
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

        public static class ClientInfo
        {
            public const string Id = "WebMvcClient";
            public const string Name = "Asp.Net Core MVC";
            public const string Secret = "secret";
        }
    }
}
