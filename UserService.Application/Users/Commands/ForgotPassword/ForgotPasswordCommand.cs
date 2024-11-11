using MediatR;

namespace UserService.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<ForgotPasswordResponse>
    {
        public string Email { get; set; }

        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
    }

}
