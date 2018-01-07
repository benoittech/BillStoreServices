using System;
using System.Collections.Generic;
using System.Text;

namespace BillStoreService.Model
{
    public class BillItemCollection
    {
        private List<BillingItems> Items { get; }

        public BillItemCollection(List<BillingItems> items)
        {
            Items = items;
        }
    }
}
