using System.ComponentModel.DataAnnotations;

namespace RESTfulBankApplication.Domain.Models
{
    public class Bill
    {
        public int BillId { get; set; }

        [Range(1, double.MaxValue)]
        public double BillSum { get; set; }
        public int PayeeId { get; set; }
        public int SenderId { get; set; }
        public virtual Payee Payee { get; set; }
        public virtual Account Sender { get; set; }
    }
}
