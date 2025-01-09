using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Enums;

namespace MyWebApi.Models
{
	public class User : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		
		// Roles
		public UserRole Role { get; set; } = UserRole.Customer;
		
		// Navigation properties
		public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
		public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
		public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
		public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
	}
}