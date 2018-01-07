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
        public async Task<IEnumerable<Invoice>> GetInvoice(DateTime dateTime, string phoneNumber)
        {
            return await invoiceService.GetUserInvoice(dateTime, phoneNumber);
        }        
        
        // POST: api/Invoice
        [HttpPost]
        public async Task Post([FromBody]Invoice value)
        {
            await invoiceService.SaveInvoice(value);
        }
        
        // PUT: api/Invoice/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
