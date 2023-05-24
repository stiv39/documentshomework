using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Configurations
{
    public class DocumentDataConfiguration : IEntityTypeConfiguration<DocumentData>
    {
        public void Configure(EntityTypeBuilder<DocumentData> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired();

            builder.HasOne(d => d.Document)
                .WithOne(d => d.Data)
                .HasForeignKey<DocumentData>(d => d.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
