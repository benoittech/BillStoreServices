using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillStoreService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.BusinessServices;

namespace Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Invoice")]
    public class InvoiceController : Controller
    {
        private readonly InvoiceService invoiceService;

        public InvoiceController(IConfiguration configuration)
        {
            invoiceService = new InvoiceService(configuration);
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IEnumerable<Invoice>> GetInvoices(DateTime dateTime, string phoneNumber)
        {
            return await invoiceService.GetUserInvoices(dateTime, phoneNumber);
        }

        [HttpGet("{phoneNumber}/{id}")]
        public async Task<Invoice> GetInvoice(string id, string phoneNumber)
        {
            return await invoiceService.GetInvoiceByIdAndPhone(phoneNumber, id);
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task Post([FromBody]Invoice invoice)
        {           
            await invoiceService.SaveInvoice(invoice);
        }
        
        // PUT: api/Invoice/5
        [HttpPut]
        public async Task<Invoice> Put([FromBody]Invoice invoice)
        {
            return await invoiceService.UpdateInvoice(invoice);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{phoneNumber}/{id}")]
        public async Task Delete(string id, string phoneNumber)
        {
            await invoiceService.DeleteInvoice(phoneNumber, id);
        }
    }
}
