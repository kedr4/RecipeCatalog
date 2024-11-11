using MediatR;
using UserService.Domain.Users;

namespace UserService.Application.Users.Commands.GenerateJwtToken
{
    public class GenerateJwtTokenCommand : IRequest<string>
    {
        public User User { get; }

        public GenerateJwtTokenCommand(User user)
        {
            User = user;
        }
    }

}
