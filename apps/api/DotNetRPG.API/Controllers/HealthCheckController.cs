using DotNetRPG.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRPG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase 
    {
        private readonly RpgDataContext _datacontext;
        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(RpgDataContext datacontext, ILogger<HealthCheckController> logger)
        {
            _datacontext = datacontext;
            _logger = logger;
        }

        [HttpGet("dbcheck")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            var canConnect = await _datacontext.CanConnectAsync();
            if (canConnect)
            {
                _logger.LogInformation("Database connection is successful");
                return Ok("Database connection is successful.");
            }
            else
            {
                _logger.LogError("Unable to connect to the database");
                return StatusCode(500, "Unable to connect to the database.");
            }
        }
    }
}
