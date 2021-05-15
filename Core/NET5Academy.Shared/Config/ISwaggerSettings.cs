namespace NET5Academy.Shared.Config
{
    public interface ISwaggerSettings
    {
        public string ApiName { get; set; }
        public string Version { get; set; }
        public string EndpointUrl { get; set; }
        public string EndpointName { get; set; }
    }
}
