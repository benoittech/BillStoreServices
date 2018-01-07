using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace BillStoreService.Model
{
    public class UserInfo : TableEntity
    {
        public UserInfo(string phoneNumber, string countryCode): base(countryCode, phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            this.CountryCode = countryCode;
        }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String CountryCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public UserType UserType { get; set; }
    }
}
