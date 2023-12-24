using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OneCore.Entities.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //UserId (clave principal autoincremental)
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd();
            entity.HasMany(e => e.lstCustomLogger)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId);

            entity.Property(e => e.Email)
                .HasMaxLength(380);

            entity.Property(e => e.Password);
        }
    }
}
