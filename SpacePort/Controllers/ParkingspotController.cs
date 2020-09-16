using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SpacePort.Controllers
{
    [ApiController]
    [Route("api/v1.0/parkingspots")]
    public class ParkingspotController : Controller
    {
        private readonly IParkingspotRepository _repo;

        public ParkingspotController(IParkingspotRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<Parkingspot[]>> GetAll()
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }
    }
}