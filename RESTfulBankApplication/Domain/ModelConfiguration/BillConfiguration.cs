using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain.ModelConfiguration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => new {x.PayeeId, x.SenderId});

            builder.ToTable("Bill");

            builder
                .HasOne(x => x.Payee)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.PayeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Sender)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.SenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
