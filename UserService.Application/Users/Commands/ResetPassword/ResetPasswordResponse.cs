namespace UserService.Application.Users.Commands.ResetPassword
{

    public class ResetPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ResetPasswordResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }


}
