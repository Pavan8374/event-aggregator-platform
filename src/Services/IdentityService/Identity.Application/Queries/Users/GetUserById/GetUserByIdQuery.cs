using Identity.Application.DTOs.Users;
using Identity.Domain.Common;
using MediatR;

namespace Identity.Application.Queries.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
