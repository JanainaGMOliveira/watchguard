using Microsoft.EntityFrameworkCore;
using ModelsAndExtensions.Models;
using APIWarehouse.Context.Configuration;

namespace APIWarehouse.Context
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions options) : base(options){}

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            builder.Entity<Brand>().ToTable("tb_brand");
            builder.Entity<Product>().ToTable("tb_product");

            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    
    }
}