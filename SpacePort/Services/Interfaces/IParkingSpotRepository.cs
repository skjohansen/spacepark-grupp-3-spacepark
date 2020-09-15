using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IParkingspotRepository
    {
        Task<Parkingspot[]> GetAll();
        Task<Parkingspot> GetparkingspotById(int ParkingspotId);
    }
}

