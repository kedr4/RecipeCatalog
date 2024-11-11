using MediatR;

namespace UserService.Application.Users.Commands.Delete

{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public string UserId { get; set; }

        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
