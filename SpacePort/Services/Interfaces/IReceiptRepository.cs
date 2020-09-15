using System.Threading.Tasks;

namespace SpacePort.Services.Interfaces
{
    public interface IReceiptRepository
    {
        Task<Receipt> GetReceiptById(int ReceiptId);
    }
}
