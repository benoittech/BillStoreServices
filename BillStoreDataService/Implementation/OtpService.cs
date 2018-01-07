using BillStoreDataService.DataService;
using BillStoreDataService.DataService.Interfaces;
using BillStoreService.Model;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Implementation
{
    public class OtpService : OtpServiceInterface
    {
        private readonly CloudStorageService _cloudStorageService;
        private const string OTPTABLENAME= "OtpTable";
        private static Random rng = new Random();

        public OtpService(string connectionString)
        {
            _cloudStorageService = new CloudStorageService(connectionString);
        }

        public async Task<string> RequestOtp(string phoneNumber)
        {
            int value = rng.Next(100, 9999); //1
            string code = value.ToString("0000");

            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(OTPTABLENAME);
            await table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(new OneTimePassword(phoneNumber, code));

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);

            return code;
        }

        public async Task<bool> ValidateUserOtp(string phoneNumber, string otp)
        {
            var oneTimePasswords = new List<OneTimePassword>();
            var otpQuery = new TableQuery<OneTimePassword>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "OTP"),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("TimeStamp", QueryComparisons.GreaterThanOrEqual, DateTime.Now.AddMinutes(-5))));

            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(OTPTABLENAME);
            TableContinuationToken tableContinuationToken = null;

            do
            {
                var queryResponse = await table.ExecuteQuerySegmentedAsync<OneTimePassword>(otpQuery, tableContinuationToken);
                tableContinuationToken = queryResponse.ContinuationToken;
                oneTimePasswords.AddRange(queryResponse.Results);
            }
            while (tableContinuationToken != null);

            var latestPassword = string.Empty;
            if (oneTimePasswords.Count > 0)
            {
                 latestPassword = oneTimePasswords.OrderByDescending(x => x.ExpiryTime).First().OTPassword;
            }

            return latestPassword == otp;
        }
    }
}
