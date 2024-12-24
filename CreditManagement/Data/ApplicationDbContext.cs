using Microsoft.EntityFrameworkCore;
using CreditManagement.Models.Entities;

namespace CreditManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor nhận các tùy chọn cấu hình DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet cho các model
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CreditScore> CreditScores { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // Cấu hình bổ sung khi tạo model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình ràng buộc và index
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.IdentificationNumber)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();

            // Cấu hình quan hệ giữa các bảng
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade Delete

            modelBuilder.Entity<CreditScore>()
                .HasOne(cs => cs.Customer)
                .WithMany(c => c.CreditScores)
                .HasForeignKey(cs => cs.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade Delete

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.Loans)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade Delete

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict Delete

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Loan)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LoanId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict Delete

            // Cấu hình giá trị mặc định cho các trường ngày
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Account>()
                .Property(a => a.OpenedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<CreditScore>()
                .Property(cs => cs.AssessmentDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionDate)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        // Phương thức để kiểm tra và áp dụng các migration
        public void ApplyMigrations()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
    }
}
