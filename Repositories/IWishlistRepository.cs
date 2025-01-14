using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
	public interface IWishlistRepository : IBaseRepository<Wishlist>
	{
		Task<bool> ExistsAsync(Guid userId, Guid itemId);
		Task<Wishlist> GetWishlistItemAsync(Guid userId, Guid itemId);
		Task<IEnumerable<Item>> GetUserWishlistAsync(Guid userId);
	}
}