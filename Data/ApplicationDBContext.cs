using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>(
                entity =>
                {
                    entity.HasIndex(u => u.NormalizedEmail)
                          .HasDatabaseName("EmailIndex")
                          .IsUnique();
                }
            );

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLongDateString(),
                },
                new IdentityRole
                {
                    Id = "8d241795-7429-4b6f-8702-862d22a57861",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLongDateString(),
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.UserId, p.StockId }));
            builder.Entity<Portfolio>()
                    .HasOne(u => u.User)
                    .WithMany(u => u.Portfolios)
                    .HasForeignKey(u => u.UserId);

            builder.Entity<Portfolio>()
                    .HasOne(u => u.Stock)
                    .WithMany(u => u.Portfolios)
                    .HasForeignKey(u => u.StockId);
        }
    }
}