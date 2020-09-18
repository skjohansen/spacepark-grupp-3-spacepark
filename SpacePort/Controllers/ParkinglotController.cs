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

        public class PostParkingspotRequest
        {
            public int ParkinglotId { get; set; }
            public int Shipsize { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<Parkingspot>> GetAvailableParkingspot(PostParkingspotRequest requestJson)
        {
            try
            {
                Parkinglot lot = await _repo.GetParkinglotById(requestJson.ParkinglotId);
                Parkingspot spot = lot.Parkingspot.Where(x => x.Occupied == false && x.Size == requestJson.Shipsize).FirstOrDefault();
                return Ok(spot);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
            
        }

    }
}