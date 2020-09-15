using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    interface IDriverRepository
    {
        Task<Driver[]> GetAll();
    }
}
