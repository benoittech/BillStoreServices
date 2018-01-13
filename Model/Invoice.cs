using Microsoft.WindowsAzure.Storage.Table;

namespace BillStoreService.Model
{
    public class Invoice : TableEntity
    {
        public Invoice(string phoneNumber, string invoiceNumber) 
        {
            this.RowKey = this.InvoiceNumber = invoiceNumber;
            this.PartitionKey = PhoneNumber = phoneNumber;
        }

        
        public Invoice() { }

        public long BillId { get; set; }

        public string CountryCode { get; set; }

        public string PhoneNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public long SellerId { get; set; }

        public string Address { get; set; }

        public string SellerName { get; set; }        

        // PaymentMode ModeOfPayment { get; set; }

        public string SellerGstNumber { get; set; }

        public double TotalGstAmount { get; set; }

        public string CashierName { get; set; }

        public string CashierId { get; set; }

        public string DiscountCuponCode { get; set; }

        public int DiscountPercent { get; set; }

        public double TotalDisCount { get; set; }

        public string BillItems { get; set; }

        public double BillAmount { get; set; }

        public double ChangeReturned { get; set; }

    }
}
