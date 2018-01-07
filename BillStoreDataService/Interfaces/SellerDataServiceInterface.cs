using BillStoreService.Model;
using System.Threading.Tasks;

namespace BillStoreDataService.DataService.Interfaces
{
    public interface SellerDataServiceInterface
    {
        void AddSeller(Seller newSeller);

        Task<Seller> UpdateSeller(Seller sellerInfo);

        Task<Seller> GetSeller(string sellerCountry, string userPhone);
    }
}
