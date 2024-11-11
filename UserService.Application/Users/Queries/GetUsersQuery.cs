using MediatR;
using UserService.Domain.Users;


namespace UserService.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<List<User>>
    {
    }
}
