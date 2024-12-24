using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManagement.Models.Entities
{
    // Model Giao Dịch
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [ForeignKey("Account")]
        public Guid? AccountId { get; set; }

        [ForeignKey("Loan")]
        public Guid? LoanId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation Properties
        public virtual Account Account { get; set; }
        public virtual Loan Loan { get; set; }
    }

    // Enum Loại Giao Dịch
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        LoanPayment,
        LoanDisbursement,
        Fee
    }
}