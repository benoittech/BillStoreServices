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
           invoice.InvoiceNumber = new Guid().ToString();
           await  invoiceDataService.SaveInvoice(invoice);
        }

        public async Task<List<Invoice>> GetUserInvoices(DateTime time, string userPhoneNumber)
        {
            var invoice =  InvoiceStub.GetInvoice();
            await invoiceDataService.SaveInvoice(invoice);

            return await invoiceDataService.GetAllInvoiceForUser(time, userPhoneNumber);
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice)
        {
            await invoiceDataService.SaveInvoice(invoice);
            return await invoiceDataService.GetInvoiceDetails(invoice.InvoiceNumber, invoice.PhoneNumber);
        }

        public async Task DeleteInvoice(string phoneNumber, string id)
        {
            await invoiceDataService.DeleteInvoice(phoneNumber, id);           
        }

        public async Task<Invoice> GetInvoiceByIdAndPhone(string phoneNumber, string id)
        {
            return await invoiceDataService.GetInvoiceDetails(phoneNumber, id);
        }

    }
}
