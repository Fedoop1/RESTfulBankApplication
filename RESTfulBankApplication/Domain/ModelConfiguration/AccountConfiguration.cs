using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain.ModelConfiguration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder
                .Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();

            builder.Ignore(x => x.TransactionAmount);

            builder.Property(x => x.MoneyAmount);//.HasColumnType("float");

            builder.Property(x => x.AccountType)
                .HasDefaultValue(AccountExtenstion.AccountType.Classic);

            builder
                .HasOne(x => x.AccountProfile)
                .WithOne(x => x.Account)
                .HasForeignKey<AccountProfile>(x => x.AccountId);
        }
    }
}
