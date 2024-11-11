using UserService.Domain.Users;

namespace UserService.Application.Users.Commands.Authenticate
{
    public class AuthenticateUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public User User { get; set; }

        public AuthenticateUserResponse(bool isSuccess, string message, User user = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            User = user;
        }
    }

}
