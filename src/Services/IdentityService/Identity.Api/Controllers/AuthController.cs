using Identity.Application.Commands.Users.CreateUser;
using Identity.Application.Commands.Users.SigninUser;
using Identity.Application.DTOs.Users;
using Identity.Application.Queries.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    /// <summary>
    /// Identity controller
    /// </summary>
    [ApiController]
    [Route("identity/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sign up account
        /// </summary>
        /// <param name="command">Create user command</param>
        /// <returns>Auth responnse dto</returns>
        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponseDto>> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Sign in account
        /// </summary>
        /// <param name="command">Sign in user command</param>
        /// <returns>Auth response dto</returns>
        [HttpPost("signin")]
        public async Task<ActionResult<AuthResponseDto>> SignInUser(SigninUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id">User identity</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
