using System.ComponentModel.DataAnnotations;
using CreditManagement.Models.Entities;

namespace CreditManagement.Models
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public List<Account> Accounts { get; set; }
        public List<CreditScore> CreditScores { get; set; }
        public List<Loan> Loans { get; set; }
    }
}

