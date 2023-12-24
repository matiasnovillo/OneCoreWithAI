using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneCore.Entities.EntitiesConfiguration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> entity)
        {
            //DocumentId (clave principal autoincremental)
            entity.HasKey(e => e.DocumentId);
            entity.Property(e => e.DocumentId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FileName)
                .IsRequired(false)
                .HasMaxLength(200);

            entity.Property(e => e.FileURL)
                .IsRequired(false);

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .IsRequired(false);

            entity.Property(e => e.Resume)
                .HasColumnType("text")
                .IsRequired(false);

            entity.Property(e => e.Feeling)
                .HasColumnType("text")
                .IsRequired(false);

            //ForeignKey
            entity.Property(e => e.UserId)
                .IsRequired();
        }
    }
}
