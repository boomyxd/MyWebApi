using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MyWebApi.Database;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
	public class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
	{
		private readonly AppDbContext _context;
		public WishlistRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> ExistsAsync(Guid userId, Guid itemId)
		{
			return await _context.Wishlists
				.AnyAsync(w => w.UserId == userId && w.ItemId == itemId);
		}
		        // Find et specifikt ønskelisteelement
        public async Task<Wishlist> GetWishlistItemAsync(Guid userId, Guid itemId)
        {
            return await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ItemId == itemId);
        }

        // Hent hele brugerens ønskeliste
        public async Task<IEnumerable<Item>> GetUserWishlistAsync(Guid userId)
        {
            return await _context.Wishlists
                .Where(w => w.UserId == userId)
                .Select(w => w.Item)
                .ToListAsync();
        }
	}
}