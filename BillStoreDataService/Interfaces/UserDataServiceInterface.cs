using BillStoreService.Model;
using System.Threading.Tasks;

namespace BillStoreDataService.DataService.Interfaces
{
    public interface UserDataServiceInterface
    {
        Task<UserInfo> GetUser(string phoneNumber, string countryCode);

        void DeleteUser(UserInfo user);

        Task<UserInfo> Adduser(UserInfo user);

        Task<UserInfo> UpdateUser(UserInfo user);
    }
}
