using System;
using static RESTfulBankApplication.Domain.Models.AccountExtenstion;

namespace RESTfulBankApplication.Domain.Models
{
    public class AccountProfile
    {
        public int ProfileId { get; set; }
        public int AccountId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public virtual Account Account { get; set; }
    }
}
