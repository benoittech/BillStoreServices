using BillStoreService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public static class InvoiceStub
    {
        private static Random random = new Random();
        private static int invoiceNumber = 1;
        public async static Task<List<Invoice>> GetInvoice()
        {
            int value = random.Next(0, 50);
            long amount = random.Next(0, 999);
            var invoice = new Invoice()
            {
                BillId = value,
                BillAmount = amount,
                CashierId = "12BB",
                CashierName = "dummy",
                //ModeOfPayment = PaymentMode.CreditCard,
                CountryCode = "+91",
                DiscountCuponCode = "Test",
                DiscountPercent = 10,
                InvoiceNumber = invoiceNumber++.ToString(),
                PhoneNumber = "9886160635",
                SellerName = "Dummy Seller",
                BillItems = GetBillItems(),
                SellerGstNumber="abcd1234",
                Address = "dDF"                 
            };

            return new List<Invoice>() { invoice };
        }

        public static string GetBillItems()
        {
            var items = new List<BillingItems>();

            items.Add(new BillingItems()
            {
                ItemCode = random.Next(0, 50).ToString(),
                ItemName = "Item" + random.Next(0, 50).ToString(),
                GrossAmount = 100,
                NetAmount = 98
            });
            items.Add(new BillingItems()
            {
                ItemCode = random.Next(0, 50).ToString(),
                ItemName = "Item" + random.Next(0, 50).ToString(),
                GrossAmount = 100,
                NetAmount = 98
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(items);
        }
    }
}
