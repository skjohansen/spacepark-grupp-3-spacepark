using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using SpacePort.Services.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Services.Repositories
{
    public class ReceiptRepository : Repository, IReceiptRepository
    {
        public ReceiptRepository(DataContext context, ILogger<ReceiptRepository> logger) : base(context, logger)
        {

        }

        public virtual async Task<Receipt[]> GetAll()
        {
            _logger.LogInformation("Getting all Receipts");
            IQueryable<Receipt> query = _context.Receipts;
            return await query.ToArrayAsync();
        }

        public virtual async Task<Receipt> GetReceiptByDriverId(int driverId)
        {
            _logger.LogInformation($"gettting receipt by driver id {driverId}");
            IQueryable<Receipt> query = _context.Receipts.Include(x => x.Driver)
                .Where(x => x.Driver.DriverId == driverId);

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<Receipt> GetReceiptById(int id)
        {
            _logger.LogInformation($"Getting receipt by id: {id}");
            IQueryable<Receipt> query = _context.Receipts.Include(x=> x.Parkingspot).Where(x => x.ReceiptId == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
