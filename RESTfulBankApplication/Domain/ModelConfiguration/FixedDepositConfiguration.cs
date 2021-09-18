using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain.ModelConfiguration
{
    public class FixedDepositConfiguration : IEntityTypeConfiguration<FixedDeposit>
    {
        public void Configure(EntityTypeBuilder<FixedDeposit> builder)
        {
            builder.ToTable("FixedDeposit");

            builder.HasKey(x => x.DepositId);
        }
    }
}
