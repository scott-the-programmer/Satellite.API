using Microsoft.AspNetCore.Mvc;

namespace Satellite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteService _nasaSatelliteService;
        public SatelliteController(ISatelliteService nasaSatelliteService)
        {
            _nasaSatelliteService = nasaSatelliteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetAsync()
        {
            var satellites = await _nasaSatelliteService.GetSatellitesAsync();
            return Ok(satellites);
        }
    }
}

