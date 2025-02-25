using Identity.Application.Common;
using Identity.Application.DTOs.Users;
using Identity.Domain.Common;
using Identity.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.Application.Commands.Users.SigninUser
{
    public class SigninUserCommandHandler : IRequestHandler<SigninUserCommand, Result<AuthResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public SigninUserCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<Result<AuthResponseDto>> Handle(SigninUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (!user.Password.VerifyPassword(request.Password))
            {
                return Result<AuthResponseDto>.Failure("Invalid email or password.");
            }

            // Generate JWT Token
            var authClaims = JWTService.GetTokenClaims(user.Email.Value, user.Id.ToString(), $"{user.FirstName} {user.LastName}");

            var token = JWTService.GetJWTToken(
                authClaims,
                _configuration["JWT:Secret"],
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                _configuration["JWT:JWTExpiryDays"]
            );

            var response = new AuthResponseDto()
            {
                Id = user.Id,
                Email = user.Email.Value,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiry = token.ValidTo
            };
            return Result.Success(response);
        }
    }
}
