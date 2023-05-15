using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyApi.Authorization.Roles;
using MyApi.Authorization.Users;
using MyApi.MultiTenancy;
using MyApi.Customers;
using MyApi.Products;

namespace MyApi.EntityFrameworkCore
{
    public class MyApiDbContext : AbpZeroDbContext<Tenant, Role, User, MyApiDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Product { get; set; }
        
        public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasMany(x => x.Produtos).WithOne(x => x.Cliente).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasOne(x => x.Cliente).WithMany(x => x.Produtos).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
