using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var converter = new EnumToStringConverter<Models.OrderStatus>();

            modelBuilder
                .Entity<Order>()
                .Property(e => e.Status)
                .HasConversion(converter);

            modelBuilder
                .Entity<OrderService>()
                .HasKey(os => new { os.ServiceId, os.OrderId });

            //modelBuilder
            //    .Entity<OrderService>()
            //    .HasOne(os => os.Order)
            //    .WithMany(s => s.OrderServices)
            //    .HasForeignKey(os => os.OrderId);

            //modelBuilder
            //    .Entity<OrderService>()
            //    .HasOne(os => os.Service)
            //    .WithMany(s => s.OrderServices)
            //    .HasForeignKey(os => os.ServiceId);
        }
    }
}
