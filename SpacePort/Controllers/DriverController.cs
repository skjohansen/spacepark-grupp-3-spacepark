using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpacePort.Models;
using SpacePort.Services.Interfaces;

namespace SpacePort.Controllers
{
    [ApiController]
    [Route("api/v1.0/drivers")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _repo;
        private readonly HttpClient _client;

        public DriverController(IDriverRepository repo, HttpClient client)
        {
            _repo = repo;
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<Driver[]>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();
                if(result == null)
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

        [HttpPost]
        public async Task<ActionResult> CreateDriver(Driver driver)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://swapi.dev/api/people/name/" + driver.Name);
                if (response.IsSuccessStatusCode)
                {
                    Driver d1 = new Driver
                    {
                        DriverId = driver.DriverId,
                        Name = driver.Name,
                        Receipts = driver.Receipts
                    };
                    _repo.Add(d1);
                    if(await _repo.Save())
                    {
                        return Created($"/api/v1.0/driver/{driver.DriverId}", d1);
                    }
                    return BadRequest();
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, $"You are not part of Star Wars universe");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }
    }
}
