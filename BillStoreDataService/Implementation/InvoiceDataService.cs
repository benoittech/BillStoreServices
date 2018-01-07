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
    public class InvoiceDataService : InvoiceDataServiceInterface
    {
        private readonly CloudStorageService _cloudStorageService;
        private const string _INVOICETABLENAME = "InvoiceTable";
        
        public InvoiceDataService(string connectionString)
        {
            this._cloudStorageService = new CloudStorageService(connectionString);
        }

        public async Task DeleteInvoice(string userPhone, string invoiceNumber)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(_INVOICETABLENAME);
            TableOperation retrieveOperation = TableOperation.Retrieve<Invoice>(userPhone, invoiceNumber);

             var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            var deleteEntity = (Invoice)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                await table.ExecuteAsync(deleteOperation);                
            }
        }

        public async Task<List<Invoice>> GetAllInvoiceForUser(DateTime time, string userPhoneNumber)
        {
            var invoices = new List<Invoice>();
            var invoiceFilter = new TableQuery<Invoice>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userPhoneNumber),           
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("TimeStamp", QueryComparisons.GreaterThanOrEqual, time)));

            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(_INVOICETABLENAME);
            TableContinuationToken tableContinuationToken = null;

            do
            {
                var queryResponse = await table.ExecuteQuerySegmentedAsync<Invoice>(invoiceFilter, tableContinuationToken);
                tableContinuationToken = queryResponse.ContinuationToken;
                invoices.AddRange(queryResponse.Results);
            }
            while (tableContinuationToken != null);
            return invoices;
        }

        public async Task SaveInvoice(Invoice invoice)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(_INVOICETABLENAME);
            table.CreateIfNotExistsAsync().Wait();

            TableOperation insertOperation = TableOperation.Insert(invoice);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
        }

        public async Task<Invoice> GetInvoiceDetails(string InvoiceId, string userPhone)
        {
            var tableClient = _cloudStorageService.GetStorageTableClient();
            var table = tableClient.GetTableReference(_INVOICETABLENAME);
            TableOperation retrieveOperation = TableOperation.Retrieve<Invoice>(userPhone, InvoiceId);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as Invoice;
        }       
    }
}
