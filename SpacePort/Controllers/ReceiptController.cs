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
    [Route("api/v1.0/[Receipts]")]
    [ApiController]
    public class ReceiptController : Controller
    {
        private readonly IReceiptRepository _repo;
        public ReceiptController(IReceiptRepository repo)
        {
            _repo = repo;
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
    }
}
