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
		public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
		public DbSet<Wishlist> Wishlists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure Login entity
			modelBuilder.Entity<Login>(entity =>
			{
				// Each Login is linked to a User via UserId (foreign key)
				entity.HasOne<User>()                  // Define the relationship
					.WithMany()                      // A User can have many Logins
					.HasForeignKey(l => l.UserId);   // Specify the foreign key in Login
			});

			// Configure PurchaseHistory entity
			modelBuilder.Entity<PurchaseHistory>(entity =>
			{
				// Each PurchaseHistory is linked to a User via UserId (foreign key)
				entity.HasOne<User>()                  // Define the relationship
					.WithMany()                      // A User can have many PurchaseHistories
					.HasForeignKey(ph => ph.UserId); // Specify the foreign key in PurchaseHistory

				// Each PurchaseHistory is linked to an Item via ItemId (foreign key)
				entity.HasOne<Item>()                  // Define the relationship
					.WithMany()                      // An Item can appear in many PurchaseHistories
					.HasForeignKey(ph => ph.ItemId); // Specify the foreign key in PurchaseHistory
			});

			// Configure Wishlist entity
			modelBuilder.Entity<Wishlist>(entity =>
			{
				// Each Wishlist is linked to a User via UserId (foreign key)
				entity.HasOne<User>()                  // Define the relationship
					.WithMany()                      // A User can have many Wishlists
					.HasForeignKey(w => w.UserId);   // Specify the foreign key in Wishlist

				// Each Wishlist item is linked to an Item via ItemId (foreign key)
				entity.HasOne<Item>()                  // Define the relationship
					.WithMany()                      // An Item can appear in many Wishlists
					.HasForeignKey(w => w.ItemId);   // Specify the foreign key in Wishlist
			});
		}
	}
}