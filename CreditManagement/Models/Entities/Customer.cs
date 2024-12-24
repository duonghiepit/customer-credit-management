using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManagement.Models.Entities
{
    // Model Khách Hàng
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CreditScore> CreditScores { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }

    // Enum Loại Khách Hàng
    public enum CustomerType
    {
        Individual,
        Business,
        Corporate
    }
}