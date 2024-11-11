using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserService.Domain.Users;

namespace UserService.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(UserManager<User> userManager, ILogger<ResetPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                _logger.LogWarning("Password reset attempt with invalid user ID: {UserId}", command.UserId);
                return new ResetPasswordResponse(false, "Invalid user.");
            }

            _logger.LogInformation("Attempting password reset for user {UserId} with token {Token}", command.UserId, command.Token);

            // Проверка валидности токена
            var isValidToken = await _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", command.Token);
            if (!isValidToken)
            {
                _logger.LogWarning("Password reset failed for user {UserId} due to invalid or expired token.", command.UserId);
                return new ResetPasswordResponse(false, "Invalid or expired token.");
            }

            var result = await _userManager.ResetPasswordAsync(user, command.Token, command.NewPassword);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Password reset failed for user {UserId} due to invalid or expired token.", command.UserId);
                return new ResetPasswordResponse(false, "Failed to reset password.");
            }

            _logger.LogInformation("Password reset successfully for user {UserId}", user.Id);
            return new ResetPasswordResponse(true, "Password has been reset successfully!");
        }
    }
}
