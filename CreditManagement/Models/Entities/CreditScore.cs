using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManagement.Models.Entities
{
    // Model Điểm Tín Dụng
    public class CreditScore
    {
        [Key]
        public Guid CreditScoreId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal RiskFactor { get; set; }

        [Required]
        public DateTime AssessmentDate { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string CreditHistory { get; set; }

        // Navigation Properties
        public virtual Customer Customer { get; set; }
    }
}