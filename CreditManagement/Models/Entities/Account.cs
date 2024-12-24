using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManagement.Models.Entities
{
    // Model Tài Khoản
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public AccountStatus Status { get; set; }

        public DateTime OpenedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }

    // Enum Loại Tài Khoản
    public enum AccountType
    {
        Checking,
        Savings,
        Investment
    }

    // Enum Trạng Thái Tài Khoản
    public enum AccountStatus
    {
        Active,
        Inactive,
        Suspended,
        Closed
    }
}