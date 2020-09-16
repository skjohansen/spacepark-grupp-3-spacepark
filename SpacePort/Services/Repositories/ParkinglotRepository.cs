using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using SpacePort.Services.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Services.Repositories
{
    public class ParkinglotRepository : Repository, IParkinglotRepository
    {
        public ParkinglotRepository(DataContext context, ILogger<ParkinglotRepository> logger) : base(context, logger)
        {
        }

        public virtual async Task<Parkinglot[]> GetAll()
        {
            _logger.LogInformation("Getting all Parkinglots");
            IQueryable<Parkinglot> query = _context.Parkinglots;
            return await query.ToArrayAsync();
        }
    }
}
