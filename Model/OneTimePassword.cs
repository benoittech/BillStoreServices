using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillStoreService.Model
{
    public class OneTimePassword : TableEntity
    {
        public OneTimePassword(string phoneNumber, string otp) : base("OTP", phoneNumber + DateTime.Now.ToString("YYYY-MM-DD"))
        {
            this.PhoneNumber = phoneNumber;
            this.OTPassword = otp;
            this.ExpiryTime = DateTime.Now.AddMinutes(5);
        }

        public OneTimePassword() { }

        public string PhoneNumber { get; set; }

        public string OTPassword { get; set; }

        public DateTime ExpiryTime;

        public string GeneratedDate
        {
            get { return DateTime.Now.ToString("YYYY-MM-DD"); }
        }
    }
}
