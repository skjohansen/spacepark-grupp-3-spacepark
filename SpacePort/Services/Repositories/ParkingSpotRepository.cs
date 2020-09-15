using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;

namespace SpacePort.Services.Repositories
{
    public class ParkingspotRepository : IParkingspotRepository
    {
        public ParkingspotRepository(DataContext context, ILogger<ParkingspotRepository> logger) : base(context, logger)
        {

        }

        public virtual async Task<Parkingspot[]> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
