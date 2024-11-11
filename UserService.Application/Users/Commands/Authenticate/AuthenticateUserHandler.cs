using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Users.Commands.Authenticate;
using UserService.Domain.Users;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenticateUserCommandHandler(UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        // Поиск пользователя по email
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == command.Email);

        // Если пользователь не найден
        if (user == null)
        {
            return new AuthenticateUserResponse(false, "No user found with that email address.");
        }

        // Проверка пароля
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return new AuthenticateUserResponse(false, "Incorrect password.");
        }

        // Возврат успешного ответа с пользователем
        return new AuthenticateUserResponse(true, "Authentication successful", user);
    }
}
