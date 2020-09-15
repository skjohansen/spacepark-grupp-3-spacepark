using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;

namespace SpacePort.Services.Repositories
{
    public class ParkingspotRepository : Repository, IParkingspotRepository
    {
        public ParkingspotRepository(DataContext context, ILogger<ParkingspotRepository> logger) : base(context, logger)
        {

        }

        public virtual async Task<Parkingspot[]> GetAll()
        {
            _logger.LogInformation("Getting all Parkingspots");
            IQueryable<Parkingspot> query = _context.Parkingspots;
            return await query.ToArrayAsync();
        }
    }
}
