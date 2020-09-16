using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IParkinglotRepository : IRepository
    {
        Task<Parkinglot[]> GetAll();
    }
}
