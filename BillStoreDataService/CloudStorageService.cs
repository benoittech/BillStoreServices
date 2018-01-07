using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillStoreDataService.DataService
{
    public class CloudStorageService
    {
        private readonly string ConnectionString;

        public CloudStorageService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(ConnectionString);   
        }
        
        public CloudTableClient GetStorageTableClient()
        {
            var cloudStorageAccount = GetStorageAccount();
            return cloudStorageAccount.CreateCloudTableClient();
        }
    }
}
