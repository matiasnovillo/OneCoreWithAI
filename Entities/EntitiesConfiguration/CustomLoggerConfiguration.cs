using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OneCore.Entities.EntitiesConfiguration
{
    public class CustomLoggerConfiguration : IEntityTypeConfiguration<CustomLogger>
    {
        public void Configure(EntityTypeBuilder<CustomLogger> entity)
        {
            //CustomLoggerId (clave principal autoincremental)
            entity.HasKey(e => e.CustomLoggerId);
            entity.Property(e => e.CustomLoggerId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InteractionType)
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .IsRequired(false);

            entity.Property(e => e.DateTime)
                .HasColumnType("datetime");

            //ForeignKey
            entity.Property(e => e.UserId)
                .IsRequired();
            entity.HasOne(e => e.User)
                .WithMany(e => e.lstCustomLogger)
                .HasForeignKey(e => e.UserId);
        }
    }
}