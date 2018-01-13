using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillStoreDataService.DataService.Interfaces;
using BillStoreService.Model;
using DataService.Implementation;
using DataService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Services.BusinessServices
{
    public class InvoiceService
    {
        private readonly InvoiceDataServiceInterface invoiceDataService;

        public InvoiceService(IConfiguration configuration)
        {
            invoiceDataService = new InvoiceDataService(configuration["ConnectionStrings:paybillstore_AzureStorageConnectionString"]);
        }

        public async Task SaveInvoice(Invoice invoice)
        {
           await  invoiceDataService.SaveInvoice(invoice);
        }

        public async Task<List<Invoice>> GetUserInvoice(DateTime time, string userPhoneNumber)
        {
            var invoice =  InvoiceStub.GetInvoice();
            await invoiceDataService.SaveInvoice(invoice);

            return await invoiceDataService.GetAllInvoiceForUser(time, userPhoneNumber);
        }



    }
}
