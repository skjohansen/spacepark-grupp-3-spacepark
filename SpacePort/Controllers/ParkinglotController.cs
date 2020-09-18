using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Controllers
{
    [ApiController]
    [Route("api/v1.0/parkinglots")]
    public class ParkinglotController : Controller
    {
        private readonly IParkinglotRepository _repo;

        public ParkinglotController(IParkinglotRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<Parkinglot[]>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Parkinglot>> GetParkinglotById(int id)
        {
            try
            {
                var result = await _repo.GetParkinglotById(id);
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
        public async Task<ActionResult<Parkingspot>>GetAvailableSpot(Parkinglot parkinglot, Parkingspot parkingspot)
        {
            try
            {
                Parkinglot lot = await _repo.GetParkinglotById(parkinglot.ParkinglotId);
                if (lot == null)
                {
                    return NotFound($"Parkinglot with id {lot.ParkinglotId} not found");
                }

                Parkingspot spot = lot.Parkingspot.Where(x => x.Occupied == false && x.Size == parkingspot.Size).FirstOrDefault();
                if (spot == null)
                {
                    return NotFound($"There is no avaible parkingspot with this specification: {lot.ParkinglotId}");
                }
                return Ok(spot);
            }

            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

    }
}