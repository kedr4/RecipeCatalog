using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Email;

namespace UserService.Application.Users.Commands.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;

        public CreateUserHandler(
            DatabaseContext context,
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Проверка на существующего пользователя
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            // Создаем нового пользователя
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                Lastname = request.Lastname,
                Role = request.Role
            };

            // Хешируем пароль
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Генерация токена подтверждения
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{request.Scheme}://{request.Host}/api/Users/confirm?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var body = $"Hello, {user.Name},<br><br>Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.";
            var subject = "Confirm your email";

            // Отправка письма

            var emailSent = await _emailService.SendEmailAsync(user.Email, subject, body);
           if (!emailSent)
           {
               throw new Exception("Error sending confirmation email.");
           } 

            return new CreateUserResponse { Id = user.Id, Email = user.Email };
        }

    }
}
