using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelsAndExtensions.Models;

namespace APIWarehouse.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne<Brand>(o => o.Brand)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "",
                    Unit = "",
                    Price = 0.0,
                    Quantity = 0,
                    Active = true,
                    BrandId = 1          
                }
            );
        }
    }
}