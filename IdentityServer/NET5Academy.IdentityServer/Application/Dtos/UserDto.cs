namespace NET5Academy.IdentityServer.Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public UserDto() { }
        public UserDto(string id, string userName, string email, bool emailConfirmed)
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
        }
    }
}
