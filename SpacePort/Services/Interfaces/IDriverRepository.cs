using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IDriverRepository : IRepository
    {
        Task<Driver[]> GetAll();
    }
}
