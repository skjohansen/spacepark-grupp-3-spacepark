using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IParkinglotRepository : IRepository
    {
        Task<Parkinglot[]> GetAll();
        Task<Parkingspot> GetparkinglotById(int ParkinglotId);
    }
}
