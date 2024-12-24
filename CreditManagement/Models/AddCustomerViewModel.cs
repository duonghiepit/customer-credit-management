using System.ComponentModel.DataAnnotations;
using CreditManagement.Models.Entities;

namespace CreditManagement.Models
{
    public class AddCustomerViewModel
    {
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
    }
}
