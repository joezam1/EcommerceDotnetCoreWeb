using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.DataAccess.EFContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>{
                entity.HasKey(b=>new{b.CategoryId});
            });


            modelBuilder.Entity<Product>(entity =>{
                entity.HasKey(s=>new{s.ProductId});
                entity.Property(p=>p.Price).HasColumnType("decimal(18,4)");
                
            } );

            modelBuilder.Entity<ProductSize>(entity => {
                entity.HasKey(s=> new{s.ProductSizeId});
                entity.HasOne(ss=>ss.Product)
                      .WithMany(s=>s.ProductSizes)
                      .HasForeignKey(ss=>ss.ProductId);

                entity.HasOne(ss=>ss.Size)
                      .WithMany(s=>s.ProductSizes)
                      .HasForeignKey(ss=>ss.SizeId);

                     
                });
            
            modelBuilder.Entity<Size>(entity =>{
                entity.HasKey(s=>new{s.SizeId});

            } );

            modelBuilder.Entity<Status>(entity =>{
                entity.HasKey(s=>new{s.StatusId});

            } );
           

            
        }
    }
}