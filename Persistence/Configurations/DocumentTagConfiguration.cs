using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DocumentTagConfiguration : IEntityTypeConfiguration<DocumentTag>
    {
        public void Configure(EntityTypeBuilder<DocumentTag> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(t => t.Document)
                .WithMany(d => d.Tags)
                .HasForeignKey(t => t.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
