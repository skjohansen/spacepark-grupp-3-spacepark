using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePort.Models;
using SpacePort.Services.Interfaces;

namespace SpacePort.Controllers
{
    [ApiController]
    [Route("api/v1.0/receipts")]
    public class ReceiptController : Controller
    {
        private readonly IReceiptRepository _repo;
        private readonly IParkingspotRepository _spotRepo;
        private readonly IDriverRepository _driverRepo;
        public ReceiptController(IReceiptRepository repo, IDriverRepository d, IParkingspotRepository s)
        {
            _repo = repo;
            _spotRepo = s;
            _driverRepo = d;
        }

        [HttpGet]
        public async Task<ActionResult<Receipt[]>> GetAll()
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
        public async Task<ActionResult<Receipt>> GetReceiptById(int id)
        {
            try
            {
                var result = await _repo.GetReceiptById(id);
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
        public async Task<ActionResult<Receipt>>CreateReceipt([FromQuery]Parkingspot parkingspot, [FromQuery]Driver driver)
        {
            try
            {
                var getDriver = await _driverRepo.GetDriverById(driver.DriverId);
                var getParkingspot = await _spotRepo.GetparkingspotById(parkingspot.ParkingspotId);


                Receipt entity = new Receipt
                {
                    RegistrationTime = DateTime.Now,
                    Driver = getDriver,
                    Parkingspot =getParkingspot
                };

                _repo.Add(entity);
                if (await _repo.Save())
                {
                    return Created($"/api/v1.0/Receipts/{entity.ReceiptId}", new Receipt 
                    { 
                        ReceiptId=entity.ReceiptId,
                        RegistrationTime=entity.RegistrationTime,
                        Driver=entity.Driver,
                        Parkingspot=entity.Parkingspot,
                    });
                }
                return BadRequest();

                //var entity = new 
                //{
                //    RegistrationTime = DateTime.Now, 
                //    DriverId= driver.DriverId,
                //    ParkingspotId=parkingspot.ParkingspotId
                //};

                //_repo.Add(entity);
                //if (await _repo.Save())
                //{
                //    return Created($"/api/v1.0/Receipts", new Receipt { });
                //}
                //return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure{e.Message}");
            }
        }
    }
}
