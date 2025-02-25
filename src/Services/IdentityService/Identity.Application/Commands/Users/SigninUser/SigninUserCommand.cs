using Identity.Application.DTOs.Users;
using Identity.Domain.Common;
using MediatR;

namespace Identity.Application.Commands.Users.SigninUser
{
    public class SigninUserCommand : IRequest<Result<AuthResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
