using Microsoft.Extensions.Logging;

namespace PakingAPI.Services
{
    public class ReceiptRepository
    {
        private readonly ILogger<ReceiptRepository> _logger;
        public ReceiptRepository(ILogger<ReceiptRepository> logger)
        {
            _logger = logger;
        }
    }
}
