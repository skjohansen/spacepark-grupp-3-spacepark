using Microsoft.Extensions.Logging;
using SpacePort.Models;
using SpacePort.Services.Interfaces;
using System.Threading.Tasks;

namespace SpacePort.Services.Repositories
{
    public class Repository : IRepository
    {
        protected readonly DataContext _context;
        protected readonly ILogger<Repository> _logger;

        public Repository(DataContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public virtual void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding entity of type {entity.GetType()}");
            _context.Add(entity);
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting entity of type {entity.GetType()}");
            _context.Remove(entity);
        }

        public async virtual Task<bool> Save()
        {
            _logger.LogInformation($"Saving changes");
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating entity of type {entity.GetType()}");
            _context.Update(entity);
        }
    }
}
