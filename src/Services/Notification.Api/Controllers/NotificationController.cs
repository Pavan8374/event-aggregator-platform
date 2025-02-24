using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("notification")]  // Changed to lowercase to match Ocelot configuration
    public class NotificationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger)
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
