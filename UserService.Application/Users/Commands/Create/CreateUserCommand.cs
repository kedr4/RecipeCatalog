using MediatR;

namespace UserService.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
    }
}
