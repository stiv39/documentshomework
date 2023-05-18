using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Text.Json;
using Domain.Models;

namespace Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Tags)
                .HasConversion(
                t => JsonSerializer.Serialize(t, new JsonSerializerOptions()),
                t => JsonSerializer.Deserialize<List<string>>(t, new JsonSerializerOptions())
            );

            builder.Property(d => d.Data)
              .HasConversion(
              d => JsonSerializer.Serialize(d, new JsonSerializerOptions()),
              d => JsonSerializer.Deserialize<DocumentData>(d, new JsonSerializerOptions())
          );
        }
    }
}
