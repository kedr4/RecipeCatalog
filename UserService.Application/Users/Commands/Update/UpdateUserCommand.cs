using MediatR;
using UserService.Domain.Users;

namespace UserService.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public string Id { get; set; }
        public User User { get; set; }

    }

}
