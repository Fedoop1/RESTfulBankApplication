using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain.ModelConfiguration
{
    public class AccountProfileConfiguration : IEntityTypeConfiguration<AccountProfile>
    {
        public void Configure(EntityTypeBuilder<AccountProfile> builder)
        {
            builder.ToTable("AccountProfile");

            builder.HasKey(x => x.ProfileId);

            builder.Property(x => x.DateOfBirth)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Gender)
                .HasDefaultValue(AccountExtenstion.Gender.Unspecified);
        }
    }
}
