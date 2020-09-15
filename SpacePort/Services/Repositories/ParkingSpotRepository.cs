using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpacePort.Services.Repositories
{
    public class ParkingSpotRepository
    {
        private readonly ILogger<ParkingSpotRepository> _logger;
        public ParkingSpotRepository(ILogger<ParkingSpotRepository> logger)
        {
            _logger = logger;
        }
    }
}
