using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Database
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Login> Logins { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
		public DbSet<Wishlist> Wishlists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure Login entity
			modelBuilder.Entity<Login>(entity =>
			{
				entity.HasOne(l => l.User)                  // Use navigation property
					.WithMany(u => u.Logins)              // A User can have many Logins
					.HasForeignKey(l => l.UserId)         // Foreign key in Login
					.OnDelete(DeleteBehavior.Cascade);    // Cascade delete
			});

			modelBuilder.Entity<User>()
				.HasMany(u => u.RefreshTokens)
				.WithOne(rt => rt.User)
				.HasForeignKey(rt => rt.UserId);

			// Configure PurchaseHistory entity
			modelBuilder.Entity<PurchaseHistory>(entity =>
			{
				entity.HasOne(ph => ph.User)               // Use navigation property
					.WithMany(u => u.PurchaseHistories)  // A User can have many PurchaseHistories
					.HasForeignKey(ph => ph.UserId)      // Foreign key in PurchaseHistory
					.OnDelete(DeleteBehavior.Cascade);   // Cascade delete

				entity.HasOne(ph => ph.Item)               // Use navigation property
					.WithMany(i => i.PurchaseHistories)  // An Item can appear in many PurchaseHistories
					.HasForeignKey(ph => ph.ItemId)      // Foreign key in PurchaseHistory
					.OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Item if referenced
			});

			// Configure Wishlist entity
			modelBuilder.Entity<Wishlist>(entity =>
			{
				entity.HasOne(w => w.User)                // Use navigation property
					.WithMany(u => u.Wishlists)         // A User can have many Wishlists
					.HasForeignKey(w => w.UserId)       // Foreign key in Wishlist
					.OnDelete(DeleteBehavior.Cascade);  // Cascade delete

				entity.HasOne(w => w.Item)                // Use navigation property
					.WithMany(i => i.Wishlists)         // An Item can appear in many Wishlists
					.HasForeignKey(w => w.ItemId)       // Foreign key in Wishlist
					.OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Item if referenced
			});
		}

	}
}