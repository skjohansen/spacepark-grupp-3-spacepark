using Microsoft.Extensions.Logging;

namespace PakingAPI.Services
{
    public class ParkinglotRepository
    {
        private readonly ILogger<ParkinglotRepository> _logger;
        public ParkinglotRepository(ILogger<ParkinglotRepository> logger)
        {
            _logger = logger;
        }
    }
}
