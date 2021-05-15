namespace NET5Academy.Shared.Models
{
    public class OkClient
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Secret { get; protected set; }

        public OkClient(string id, string name, string secret)
        {
            Id = id;
            Name = name;
            Secret = secret;
        }
    }
}
