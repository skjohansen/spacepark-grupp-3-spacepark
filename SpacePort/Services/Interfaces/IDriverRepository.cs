using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    interface IDriverRepository : IRepository
    {
        Task<Driver[]> GetAll();
    }
}
