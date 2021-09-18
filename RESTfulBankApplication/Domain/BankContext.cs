using Microsoft.EntityFrameworkCore;
using RESTfulBankApplication.Domain.ModelConfiguration;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain
{
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountProfile> AccountProfiles { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<FixedDeposit> FixedDeposits { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AccountConfiguration())
                .ApplyConfiguration(new AccountProfileConfiguration())
                .ApplyConfiguration(new BillConfiguration())
                .ApplyConfiguration(new FixedDepositConfiguration())
                .ApplyConfiguration(new PayeeConfiguration());
        }
    }
}
