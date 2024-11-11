using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Database;

namespace UserService.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly DatabaseContext _context;

        public UpdateUserCommandHandler(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(command.Id);

            if (user == null)
            {
                return new UpdateUserResponse(false, "User not found");
            }

            // Обновляем поля пользователя
            user.Name = command.User.Name;
            user.Lastname = command.User.Lastname;
            user.Email = command.User.Email;
            user.Role = command.User.Role;

            try
            {
                await _context.SaveChangesAsync();
                return new UpdateUserResponse(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new UpdateUserResponse(false, "Concurrency error occurred");
            }
        }
    }

}
