using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Branch>().ToTable("Branches");
            modelBuilder.Entity<Company>().ToTable("Companies");



            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .HasDefaultValue("کالا");

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Branch)
                .WithMany(x => x.Products)
                .HasForeignKey(x=>x.BranchId);

  

        }
    }
}
