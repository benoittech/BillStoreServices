using BillStoreDataService.DataService.Interfaces;
using BillStoreService.Model;
using DataService.Implementation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.BusinessServices
{
    public class UserService
    {
        private readonly UserDataServiceInterface userRepository;

        public UserService(IConfiguration configuration)
        {
            userRepository = new UserDataService(configuration["ConnectionStrings:paybillstore_AzureStorageConnectionString"]);
        }

        public async Task<UserInfo> Adduser(UserInfo user)
        {
            return await userRepository.Adduser(user);
        }

        public async Task DeleteUser(string countryCode, string phoneNumber)
        {
            await userRepository.DeleteUser(countryCode, phoneNumber);
        }

        public async Task<UserInfo> GetUser(string phoneNumber, string countryCode)
        {
            return await userRepository.GetUser(phoneNumber, countryCode);
        }

        public async Task<UserInfo> UpdateUser(UserInfo user)
        {
            return await userRepository.UpdateUser(user);
        }
    }
}
