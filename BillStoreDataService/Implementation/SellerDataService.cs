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
    public class SellerDataService : SellerDataServiceInterface
    {
        private readonly CloudStorageService _cloudStorageService;
        private const string SELLERTABLENAME = "Seller";

        public SellerDataService(string connectionString)
        {
             _cloudStorageService = new CloudStorageService(connectionString);
        }

        public async void AddSeller(Seller newSeller)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(SELLERTABLENAME);
            await table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(newSeller);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
        }

        public async Task<Seller> GetSeller(string sellerCountry, string userPhone )
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(SELLERTABLENAME);
            TableOperation retrieveOperation = TableOperation.Retrieve<Seller>(sellerCountry, userPhone);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as Seller;
        }

        public async Task<Seller> UpdateSeller(Seller sellerInfo)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(SELLERTABLENAME);
            await table.CreateIfNotExistsAsync();

            TableOperation updateOperation = TableOperation.InsertOrReplace(sellerInfo);

            // Execute the insert operation.
            await table.ExecuteAsync(updateOperation);

            return await GetSeller(sellerInfo.SellerCountry, sellerInfo.PhoneNumber);
        }
    }
}
