using NET5Academy.Shared.Models;

namespace NET5Academy.Shared.Constants
{
    public class OkIdentityConstants
    {
        public const string UserIdKey = "sub";

        public static class ScopeName
        {
            public const string Roles = "roles";
            public const string CatalogAPI = "CatalogApi";
            public const string PhotoStockAPI = "PhotoStockApi";
            public const string BasketAPI = "BasketApi";
            public const string DiscountAPI = "DiscountApi";
            public const string OrderAPI = "OrderApi";
            public const string PaymentAPI = "PaymentApi";

            public const string ApiGateway = "ApiGateway";
        }
        public static class ScopeDisplay
        {
            public const string Roles = "Roles";
            public const string IdentityAPI = "IdentityServer API full permission";
            public const string CatalogAPI = "Catalog API full permission";
            public const string PhotoStockAPI = "PhotoStock API full permission";
            public const string BasketAPI = "Basket API full permission";
            public const string DiscountAPI = "Discount API full permission";
            public const string OrderAPI = "Order API full permission";
            public const string PaymentAPI = "Payment API full permission";

            public const string ApiGateway = "API Gateway full permission";
        }

        public static class ResourceName
        {
            public const string CatalogAPI = "resource_catalog";
            public const string PhotoStockAPI = "resource_photostock";
            public const string BasketAPI = "resource_basket";
            public const string DiscountAPI = "resource_discount";
            public const string OrderAPI = "resource_order";
            public const string PaymentAPI = "resource_payment";
            public const string ApiGateway = "resource_apigateway";
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
