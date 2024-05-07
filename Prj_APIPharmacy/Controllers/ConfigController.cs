using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ConfigController : Controller
{
    private readonly ILogger<ConfigController> _logger;

    public ConfigController(ILogger<ConfigController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/api/test")]
    public IActionResult Test()
    {
        var now = DateTime.Now;

        _logger.LogInformation("Test endpoint called at {time}", now);

        return Ok(new
        {
            Message = "Test works!",
            Time = now
        });
    }
}