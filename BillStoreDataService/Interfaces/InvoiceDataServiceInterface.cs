using BillStoreService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillStoreDataService.DataService.Interfaces
{
    public interface InvoiceDataServiceInterface
    {
        Task SaveInvoice(Invoice invoice);

        Task<List<Invoice>> GetAllInvoiceForUser(DateTime time, string userPhoneNumber);

        Task DeleteInvoice(string userPhone, string invoiceNumber);
    }
}
