using DotNetRPG.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase 
    {
        private readonly RpgDataContext _datacontext;
        public HealthCheckController(RpgDataContext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpGet("dbcheck")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            var canConnect = await _datacontext.CanConnectAsync();
            if (canConnect)
            {
                return Ok("Database connection is successful.");
            }
            else
            {
                return StatusCode(500, "Unable to connect to the database.");
            }
        }
    }
}
