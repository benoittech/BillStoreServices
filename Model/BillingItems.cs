using System;
using System.Collections.Generic;
using System.Text;

namespace BillStoreService.Model
{
    public class BillingItems
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Discount { get; set; }
        public double TaxPercent { get; set; }
        public double TaxAmount { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
    }
}
