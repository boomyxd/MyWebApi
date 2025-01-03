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
		}
	}
}