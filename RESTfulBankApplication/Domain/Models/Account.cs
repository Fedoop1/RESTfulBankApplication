using System.Collections.Generic;
using System.Linq;

namespace RESTfulBankApplication.Domain.Models
{
    public static class AccountExtenstion
    {
        public enum Gender
        {
            Unspecified,
            Male,
            Female,
        }
        public enum AccountType
        {
            Classic,
            Loan,
        }
    }

    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double MoneyAmount { get; set; }
        public double TransactionAmount => this.Bills.Select(b => b.BillSum).Sum();
        public AccountExtenstion.AccountType AccountType { get; set; }

        public virtual AccountProfile AccountProfile { get; set; }
        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
        public virtual ICollection<FixedDeposit> FixedDeposits { get; set; } = new HashSet<FixedDeposit>();
    }
}
