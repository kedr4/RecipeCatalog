using MediatR;
using UserService.Domain.Users;

namespace UserService.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string Id { get; }

        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
