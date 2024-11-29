using Microsoft.EntityFrameworkCore;
using NET1041_ChuaDeThiMau.Models;

namespace NET1041_ChuaDeThiMau.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Pets);
        }
    }
}
