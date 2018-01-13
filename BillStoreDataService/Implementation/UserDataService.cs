using BillStoreDataService.DataService;
using BillStoreDataService.DataService.Interfaces;
using BillStoreService.Model;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Implementation
{
    public class UserDataService : UserDataServiceInterface
    {
        private readonly CloudStorageService _cloudStorageService;       
        private const string USERTABLENAME = "users";

        public UserDataService(string connectionString)
        {
            _cloudStorageService = new CloudStorageService(connectionString);
        }

        public async Task<UserInfo> Adduser(UserInfo user)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(USERTABLENAME);
            await table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(user);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);

            TableOperation retrieveOperation = TableOperation.Retrieve<UserInfo>(user.PhoneNumber, user.CountryCode);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as UserInfo;
        }

        public async Task DeleteUser(string countryCode, string phoneNumber)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(USERTABLENAME);
            TableOperation retrieveOperation = TableOperation.Retrieve<Invoice>(countryCode, phoneNumber);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            var deleteEntity = (UserInfo)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                await table.ExecuteAsync(deleteOperation);
            }
        }

        public async Task<UserInfo> GetUser(string phoneNumber, string countryCode)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(USERTABLENAME);
            TableOperation retrieveOperation = TableOperation.Retrieve<UserInfo>(countryCode, phoneNumber);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as UserInfo;
        }

        public async  Task<UserInfo> UpdateUser(UserInfo user)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(USERTABLENAME);
            await table.CreateIfNotExistsAsync();

            TableOperation updateOperation = TableOperation.InsertOrReplace(user);

            // Execute the insert operation.
            await table.ExecuteAsync(updateOperation);

            return await GetUser(user.PhoneNumber, user.CountryCode);
        }
    }
}
