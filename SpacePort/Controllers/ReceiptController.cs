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

        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceiptByDriverId(int driverId)
        {
            try
            {
                var result = await _repo.GetReceiptByDriverId(driverId);
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

        [HttpPut]
        public async Task<ActionResult<Receipt>> UpdateReceipt(Receipt receipt)
        {
            try
            {
                var oldReceipt = await _repo.GetReceiptById(receipt.ReceiptId);
                if (oldReceipt==null)
                {
                    return NotFound($"Receipt with id: {receipt.ReceiptId} could not be found");
                }

                DateTime currentTime = DateTime.Now;

                oldReceipt.EndTime = currentTime;
                oldReceipt.Parkingspot.Occupied = true;

                DateTime start = oldReceipt.RegistrationTime;
                DateTime end = currentTime;

                TimeSpan span = end - start;
                int hPrice = (10 * span.Hours);
                float mPrice = (10 / 60.0f) * span.Minutes;
                int totalPrice = Convert.ToInt32(hPrice + mPrice);
                oldReceipt.Price = totalPrice;

                _repo.Update(oldReceipt);
                if (await _repo.Save())
                {
                    return Ok(oldReceipt);
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        public class PostReceipt
        {
            public int ParkingspotId { get; set; }
            public int DriverId { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<Receipt>> CreateReceipt(PostReceipt receipt)
        {
            try
            {
                var getDriver = await _driverRepo.GetDriverById(receipt.DriverId);
                var getParkingspot = await _spotRepo.GetparkingspotById(receipt.ParkingspotId);


                Receipt entity = new Receipt
                {
                    RegistrationTime = DateTime.Now,
                    Driver = getDriver,
                    Parkingspot = getParkingspot
                };

                _repo.Add(entity);
                if (await _repo.Save())
                {
                    return Created($"/api/v1.0/Receipts/{entity.ReceiptId}", new Receipt 
                    { 
                        ReceiptId=entity.ReceiptId,
                        RegistrationTime=entity.RegistrationTime,
                        Driver=entity.Driver,
                        Parkingspot=entity.Parkingspot
                    });
                }
                return BadRequest();

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure{e.Message}");
            }
        }
    }
}
