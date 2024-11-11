namespace UserService.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ForgotPasswordResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
