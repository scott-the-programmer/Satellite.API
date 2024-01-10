using Microsoft.AspNetCore.Mvc;

namespace Satellite.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteService _nasaSatelliteService;
        public SatelliteController(ISatelliteService nasaSatelliteService)
        {
            _nasaSatelliteService = nasaSatelliteService;
        }

        [HttpGet("weatherstation")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetWeatherStations()
        {
            var satellites = await _nasaSatelliteService.GetWeatherStationsAsync();
            return Ok(satellites);
        }

        [HttpGet("iss")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetISS()
        {
            var satellites = await _nasaSatelliteService.GetISSAsync();
            return Ok(satellites);
        }

        [HttpGet("starlink")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetStarlinks()
        {
            var satellites = await _nasaSatelliteService.GetStarlinksAsync();
            return Ok(satellites);
        }

        [HttpGet("iridium")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetIridiums()
        {
            var satellites = await _nasaSatelliteService.GetIridiumsAsync();
            return Ok(satellites);
        }

        [HttpGet("tv")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetTV()
        {
            var satellites = await _nasaSatelliteService.GetTVAsync();
            return Ok(satellites);
        }

        [HttpGet("celestis")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetCeletis()
        {
            var satellites = await _nasaSatelliteService.GetCelestisAsync();
            return Ok(satellites);
        }

        [HttpGet("brightest")]
        public async Task<ActionResult<IEnumerable<Models.Satellite>>> GetBrightest()
        {
            var satellites = await _nasaSatelliteService.GetBrightestAsync();
            return Ok(satellites);
        }

    }
}

