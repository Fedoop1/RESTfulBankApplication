using System.Collections.Generic;

namespace RESTfulBankApplication.Domain.Models
{
    public class Payee
    {
        public int PayeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
