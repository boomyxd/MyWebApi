using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
	public class Item : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		
		public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
		public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

	}
}