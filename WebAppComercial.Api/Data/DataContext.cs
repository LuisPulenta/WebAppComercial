using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Iva> Ivas { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Storeproduct> Storeproducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Store>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Measure>().HasIndex(c => c.Unit).IsUnique();
            modelBuilder.Entity<Iva>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Concept>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex("Name", "Document").IsUnique();
            modelBuilder.Entity<Supplier>().HasIndex("Name", "Document").IsUnique();
            modelBuilder.Entity<Barcode>().HasIndex(c => c.Code).IsUnique();
            modelBuilder.Entity<Storeproduct>().HasIndex("StoreId", "ProductId").IsUnique();
        }
    }
}
