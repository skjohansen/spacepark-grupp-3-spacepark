using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpacePort.Services.Repositories
{
    public class ParkingspotRepository
    {
        private readonly ILogger<ParkingspotRepository> _logger;
        public ParkingspotRepository(ILogger<ParkingspotRepository> logger)
        {
            _logger = logger;
        }
    }
}
