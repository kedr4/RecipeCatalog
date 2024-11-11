using MediatR;
using UserService.Infrastructure.Database;

namespace UserService.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly DatabaseContext _context;

        public DeleteUserCommandHandler(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(command.UserId);

            if (user == null)
            {
                return new DeleteUserResponse(false, "User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new DeleteUserResponse(true);
        }
    }


}
