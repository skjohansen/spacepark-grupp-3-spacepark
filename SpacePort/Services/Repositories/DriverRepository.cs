using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpacePort.Services.Repositories
{
    public class DriverRepository
    {
        private readonly ILogger<DriverRepository> _logger;
        public DriverRepository(ILogger<DriverRepository> logger)
        {
            _logger = logger;
        }
    }
}
