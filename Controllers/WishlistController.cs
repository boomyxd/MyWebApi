using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Controllers
{
	[ApiController]
	[Route("api/wishlist")]
	public class WishlistController : BaseEntityController<Wishlist>
	{
		private readonly IWishlistRepository _wishlistRepository;
		private readonly IBaseRepository<Item> _itemRepository;
		
		public WishlistController(IWishlistRepository wishlistRepository, IBaseRepository<Item> itemRepository) : base(wishlistRepository)
		{
			_wishlistRepository = wishlistRepository;
			_itemRepository = itemRepository;
		}
		
		[HttpPost("add")]
		public async Task<IActionResult> AddToWishlist([FromBody] WishlistItemDto dto)
		{
			var userId = GetUserIdFromToken();
			
			var item = await _itemRepository.GetByIdAsync(dto.ItemId);
			if (item == null)
			{
				return NotFound(new { message = "Item not found" });
			}
			
			if (await _wishlistRepository.ExistsAsync(userId, dto.ItemId))
			{
				return BadRequest(new { message = "Item already in wishlist" });
			}
			
			var wishlistItem = new Wishlist
			{
				UserId = userId,
				ItemId = dto.ItemId
			};
			
			await _wishlistRepository.AddAsync(wishlistItem);
			return Ok(new { message = "Item added to wishlist" });
		}
		
		[HttpDelete("remove/{itemId}")]
		public async Task<IActionResult> RemoveFromWishlist(Guid itemId)
		{
			var userId = GetUserIdFromToken();
			
			var wishlistItem = await _wishlistRepository.GetWishlistItemAsync(userId, itemId);
			if (wishlistItem == null)
			{
				return NotFound(new { message = "Item not found in wishlist" });
			}
			
			await _wishlistRepository.DeleteAsync(wishlistItem.Id);
			return Ok(new { message = "Item removed from wishlist" });
		}
		
		[HttpGet]
		public async Task<IActionResult> GetWishlist()
		{
			var userId = GetUserIdFromToken();

			var wishlist = await _wishlistRepository.GetUserWishlistAsync(userId);
			return Ok(wishlist);
		}
		
		private Guid GetUserIdFromToken()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userIdClaim))
			{
				throw new UnauthorizedAccessException("User not found in token");
			}
			return Guid.Parse(userIdClaim);
		}
	}
}