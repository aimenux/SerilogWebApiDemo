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
            _logger.LogTrace("Hey, {Name}!", ToTitleCase(name));
            _logger.LogDebug("Hey, {Name}!", ToTitleCase(name));
            _logger.LogInformation("Hey, {Name}!", ToTitleCase(name));
            _logger.LogWarning("Hey, {Name}!", ToTitleCase(name));
            _logger.LogError("Hey, {Name}!", ToTitleCase(name));
            _logger.LogCritical("Hey, {Name}!", ToTitleCase(name));
            return $"Hey, {name}!";
        }

        [HttpGet(nameof(Bye))]
        public string Bye([FromQuery] string name = "Walter")
        {
            _logger.LogTrace("Bye, {Name}!", ToTitleCase(name));
            _logger.LogDebug("Bye, {Name}!", ToTitleCase(name));
            _logger.LogInformation("Bye, {Name}!", ToTitleCase(name));
            _logger.LogWarning("Bye, {Name}!", ToTitleCase(name));
            _logger.LogError("Bye, {Name}!", ToTitleCase(name));
            _logger.LogCritical("Bye, {Name}!", ToTitleCase(name));
            return $"Bye, {name}!";
        }

        private static string ToTitleCase(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : $"{char.ToLowerInvariant(name[0])}{name.Substring(1)}";
        }
    }
}
