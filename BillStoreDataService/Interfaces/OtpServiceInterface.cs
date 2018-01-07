using BillStoreService.Model;
using System.Threading.Tasks;

namespace BillStoreDataService.DataService.Interfaces
{
    public interface OtpServiceInterface
    {
        Task<string> RequestOtp(string phoneNumber);

        Task<bool> ValidateUserOtp(string phoneNumber, string otp);
    }
}
