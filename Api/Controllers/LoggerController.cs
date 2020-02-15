using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<LoggerController> _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(nameof(Hey))]
        public string Hey([FromQuery] string name = "Walter")
        {
            _logger.LogInformation("Hey, {Name}!", ToTitleCase(name));
            return $"Hey, {name}!";
        }

        [HttpGet(nameof(Bye))]
        public string Bye([FromQuery] string name = "Walter")
        {
            _logger.LogInformation("Bye, {Name}!", ToTitleCase(name));
            return $"Bye, {name}!";
        }

        private static string ToTitleCase(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : $"{char.ToLowerInvariant(name[0])}{name.Substring(1)}";
        }
    }
}
