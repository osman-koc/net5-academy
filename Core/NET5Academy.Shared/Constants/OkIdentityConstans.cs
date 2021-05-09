namespace NET5Academy.Shared.Constants
{
    public class OkIdentityConstans
    {
        public static class ScopeName
        {
            public const string CatalogAPI = "catalog_fullpermission";
            public const string PhotoStockAPI = "photostock_fullpermission";
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
    }
}
