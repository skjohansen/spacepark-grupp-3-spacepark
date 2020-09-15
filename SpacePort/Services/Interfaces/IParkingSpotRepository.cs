using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    interface IParkingspotRepository
    {
        Task<Parkingspot[]> GetAll();
    }
}

