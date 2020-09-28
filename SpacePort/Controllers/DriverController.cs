using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePort.Models;
using SpacePort.Services;
using SpacePort.Services.Interfaces;

namespace SpacePort.Controllers
{
    [ApiController]
    [Route("api/v1.0/drivers")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _repo;

        public DriverController(IDriverRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<Driver[]>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpGet("test")]
        public IActionResult Feedback()
        {
            return Json("Hej från API");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriverById(int id)
        {
            try
            {
                var result = await _repo.GetDriverById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> CreateDriver(Driver driver) 
        {
            try
            {
                bool driverInStarWars = await StarWarsApi.GetDriverName(driver.Name);

                if (driverInStarWars)
                {
                    _repo.Add(driver);
                    if(await _repo.Save())
                    {
                        return Created("/api/v1.0/drivers/" + driver.DriverId, new Driver { DriverId = driver.DriverId, Name = driver.Name });
                    }
                    return BadRequest();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }
    }
}
