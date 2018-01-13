using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillStoreService.Model
{
    public class Seller : TableEntity
    {
        public Seller(string sellerCountry, string phoneNumber) :base(sellerCountry, phoneNumber)
        {
            this.SellerCountry = this.PartitionKey = sellerCountry;
            this.PhoneNumber = this.RowKey = phoneNumber;
        }

        public string SellerName { get; set; }

        public string SellerCategory { get; set; }

        public string SellerId { get; set; }      

        public string SellerGstNumber { get; set; }   
        
        public string SellerAddress { get; set; }

        public string SellerCountry { get; set; }

        public string SellerCity { get; set; }

        public string SellerPinCode { get; set; }

        public string PhoneNumber { get; set; }

    }
}
