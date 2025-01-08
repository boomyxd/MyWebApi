using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime Expires { get; set; }
		public bool IsRevoked { get; set; }
		public Guid UserId { get; set; } // Foreign key
		public virtual User User { get; set; } // Navigation property

	}
}