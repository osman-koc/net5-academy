namespace NET5Academy.IdentityServer.Application.Dtos
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
