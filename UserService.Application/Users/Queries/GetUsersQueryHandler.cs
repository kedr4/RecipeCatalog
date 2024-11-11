using MediatR;
using UserService.Domain.Users;

namespace UserService.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly IUserRepository _repository;

        public GetUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllUsersAsync();
        }
    }
}
