using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Controllers.Requests;
using UserService.Application.Users.Commands.Authenticate;
using UserService.Application.Users.Commands.Create;
using UserService.Application.Users.Commands.Delete;
using UserService.Application.Users.Commands.ForgotPassword;
using UserService.Application.Users.Commands.GenerateJwtToken;
using UserService.Application.Users.Commands.ResetPassword;
using UserService.Application.Users.Commands.Update;
using UserService.Application.Users.Queries;
using UserService.Domain.Users;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Email;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly ILogger<UsersController> _logger;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        public UsersController(DatabaseContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration, ILogger<UsersController> logger, IEmailService emailService, UserManager<User> userManager, IMediator mediator)
        //public UsersController(DatabaseContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration, ILogger<UsersController> logger, UserManager<User> userManager, IMediator mediator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtSecret = configuration["JwtSettings:Secret"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
            _logger = logger;
            _emailService = emailService;
            _userManager = userManager;
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await _mediator.Send(query);
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(string id, [FromBody] UpdateUserRequest updateUser)
        {

            // Отправляем команду через MediatR
            var command = new UpdateUserCommand()
            {
                Id = id,
                User = new Domain.Users.User()
                {
                    Name = updateUser.Name,
                    Email = updateUser.Name,
                    Lastname = updateUser.Lastname,
                    Role = updateUser.Role
                }

            };
            var result = await _mediator.Send(command);  // Отправляем команду через MediatR

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);  // Если обновление не удалось, возвращаем 404 с сообщением
            }

            return NoContent();  // Если обновление прошло успешно, возвращаем 204 No Content
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);  // Send the command via MediatR

            if (!result.Success)
            {
                return NotFound();  // If result is false, return NotFound
            }

            return NoContent();  // Return 204 No Content if deletion is successful
        }


        [HttpPost("register")]
        public async Task<ActionResult<DeleteUserResponse>> Register([FromBody] CreateUserRequest request)
        {
            // Создаем команду для MediatR
            var command = new CreateUserCommand
            {
                Email = request.Email,
                Name = request.Name,
                Lastname = request.Lastname,
                Password = request.Password,
                Role = request.Role,
                Scheme = Request.Scheme,
                Host = Request.Host.Value
            };

            // Отправляем команду и получаем ответ
            var response = await _mediator.Send(command);

            // Проверяем, успешно ли создан пользователь
            if (response == null)
            {
                return BadRequest("A user with this email already exists or an error occurred.");
            }

            // Возвращаем результат
            return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok("Email confirmed successfully!");
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            // Создаем команду для аутентификации
            var authenticateCommand = new AuthenticateUserCommand(email, password);
            var authResponse = await _mediator.Send(authenticateCommand);

            // Если аутентификация не удалась, возвращаем ошибку
            if (!authResponse.IsSuccess)
            {
                return BadRequest(authResponse.Message);
            }

            // Создаем команду для генерации JWT токена
            var generateTokenCommand = new GenerateJwtTokenCommand(authResponse.User);
            var token = await _mediator.Send(generateTokenCommand);

            // Возвращаем токен
            return Ok(new { token });
        }


        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] string email)
        {
            var command = new ForgotPasswordCommand(email);
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] UserService.Application.Users.Commands.ResetPassword.ResetPasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            var command = new ResetPasswordCommand(request.UserId, request.Token, request.NewPassword);
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

    }
}
