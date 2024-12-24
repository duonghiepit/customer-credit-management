using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManagement.Models.Entities
{
    // Model Khoản Vay
    public class Loan
    {
        [Key]
        public Guid LoanId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        [Required]
        public LoanType LoanType { get; set; }

        [Required]
        public LoanStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RemainingBalance { get; set; }

        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }

    // Enum Loại Khoản Vay
    public enum LoanType
    {
        Personal,
        Mortgage,
        Business,
        Auto
    }

    // Enum Trạng Thái Khoản Vay
    public enum LoanStatus
    {
        Pending,
        Approved,
        Active,
        Rejected,
        Completed,
        Defaulted
    }
}