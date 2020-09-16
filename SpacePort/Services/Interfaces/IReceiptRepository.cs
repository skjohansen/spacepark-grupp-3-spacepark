using SpacePort.Models;
using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IReceiptRepository : IRepository
    {
        Task<Receipt[]> GetAll();
        Task<Receipt> GetReceiptById(int ReceiptId);
    }
}
