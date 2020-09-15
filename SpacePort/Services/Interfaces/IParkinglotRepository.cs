using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Services
{
    public interface IParkinglotRepository
    {
        Task<Parkinglot[]> GetAll();
    }
}
