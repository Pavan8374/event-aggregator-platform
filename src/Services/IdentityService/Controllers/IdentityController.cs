using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("identity")]  // Changed to lowercase to match Ocelot configuration
    public class IdentityController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Identity endpoint called at: {time}", DateTimeOffset.UtcNow);
            return Ok(new
            {
                message = "Identity service is working!",
                data = Summaries,
                timestamp = DateTimeOffset.UtcNow
            });
        }
    }
}