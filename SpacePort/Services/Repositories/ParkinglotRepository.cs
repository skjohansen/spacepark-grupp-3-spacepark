using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PakingAPI.Services
{
    public class ParkinglotRepository : IParkinglotRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ParkinglotRepository> _logger;
        public ParkinglotRepository(DataContext context, ILogger<ParkinglotRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<Parkinglot[]> GetAll()
        {
            _logger.LogInformation($"Getting all parkinglots");
            IQueryable<Parkinglot> query = _context.Parkinglots;
            return await query.ToArrayAsync();
        }
    }
}
