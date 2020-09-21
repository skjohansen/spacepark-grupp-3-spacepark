using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;


namespace SpacePort.Services.Repositories
{
    public class DriverRepository : Repository, IDriverRepository
    {
        public DriverRepository(DataContext context, ILogger<DriverRepository> logger) : base(context, logger)
        {
        }
        public virtual async Task<Driver[]> GetAll()
        {
            _logger.LogInformation($"Getting all drivers");
            IQueryable<Driver> query = _context.Drivers;
            return await query.ToArrayAsync();
        }

        public virtual async Task<Driver> GetDriverById(int id)
        {
            _logger.LogInformation($"Getting driver by id: {id}");
            IQueryable<Driver> query = _context.Drivers.Where(x => x.DriverId == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
