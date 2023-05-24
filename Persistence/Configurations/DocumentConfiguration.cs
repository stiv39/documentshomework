using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Id);
    
            builder.HasMany(d => d.Tags)
                .WithOne()
                .HasForeignKey(t => t.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Data)
                .WithOne()
                .HasForeignKey<Document>(d => d.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
