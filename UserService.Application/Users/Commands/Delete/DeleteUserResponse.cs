namespace UserService.Application.Users.Commands.Delete

{
    public class DeleteUserResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteUserResponse(bool success, string errorMessage = null)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }

}
