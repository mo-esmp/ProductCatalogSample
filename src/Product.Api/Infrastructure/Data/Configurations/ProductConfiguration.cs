using Catalog.Api.Domain.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Infrastructure.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .OwnsOne(s => s.Code)
                .Property(b => b.Value)
                .HasColumnName("Code")
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            builder
                .OwnsOne(s => s.Name)
                .Property(b => b.Value)
                .HasColumnName("Name")
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            builder
                .OwnsOne(x => x.Price,
                    p => p.OwnsOne(c => c.Currency));
        }
    }
}