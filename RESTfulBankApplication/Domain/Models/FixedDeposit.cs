using System.ComponentModel.DataAnnotations;

namespace RESTfulBankApplication.Domain.Models
{
    public class FixedDeposit
    {
        public int DepositId { get; set; }
        public int AccountId { get; set; }
        
        [Range(1, double.MaxValue)]
        public double DepositRate { get; set; }

        [Range(1, double.MaxValue)]
        public double DepositAmount { get; set; }

        public virtual Account Account { get; set; }
    }
}
