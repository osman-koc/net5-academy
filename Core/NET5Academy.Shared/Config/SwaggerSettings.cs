namespace NET5Academy.Shared.Config
{
    public class SwaggerSettings : ISwaggerSettings
    {
        public string ApiName { get; set; }
        public string Version { get; set; }
        public string EndpointUrl { get; set; }
        public string EndpointName { get; set; }
    }
}
