using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PakingAPI.Services
{
    public class ReceiptRepository : IReceiptRepository
    {
        public ReceiptRepository(DataContext context, ILogger<ReceiptRepository> logger)
        {

        }

        public virtual async Task<Receipt> GetReceiptById(int id)
        {
            _logger.LogInformation($"Getting receipt by id: {id}");
            IQueryable<Receipt> query = _context.Receipts.Where(x => x.ReceiptId == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
