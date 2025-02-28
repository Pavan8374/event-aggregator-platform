using Identity.Application.DTOs.Users;
using Identity.Domain.Common;
using MediatR;

namespace Identity.Application.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Result<AuthResponseDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsOrganizer { get; set; }
    }
}
