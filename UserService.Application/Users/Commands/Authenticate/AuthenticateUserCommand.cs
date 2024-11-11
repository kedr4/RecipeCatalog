using MediatR;

namespace UserService.Application.Users.Commands.Authenticate
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

}
