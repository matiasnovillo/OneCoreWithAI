using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneCore.Entities.EntitiesConfiguration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> entity)
        {
            //BillId (clave principal autoincremental)
            entity.HasKey(e => e.BillId);
            entity.Property(e => e.BillId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FileName)
                .IsRequired(false)
                .HasMaxLength(200);

            entity.Property(e => e.FileURL)
                .IsRequired(false);

            entity.Property(e => e.ClientName)
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(e => e.ClientAddress)
                .IsRequired(false)
                .HasMaxLength(2000);

            entity.Property(e => e.ProviderName)
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(e => e.ProviderAddress)
                .IsRequired(false)
                .HasMaxLength(2000);

            entity.Property(e => e.BillingNumber)
                .IsRequired(false)
                .HasMaxLength(50);

            entity.Property(e => e.BillingDate)
                .IsRequired(false)
                .HasMaxLength(20);

            entity.Property(e => e.BillingTotal)
                .IsRequired(false)
                .HasMaxLength(50);

            //ForeignKey
            entity.Property(e => e.UserId)
                .IsRequired();
        }
    }
}
