using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Controllers
{
	[ApiController]
	[Route("api/purchases")]
	public class PurchaseHistoryController : ControllerBase
	{
		private readonly IBaseRepository<PurchaseHistory> _purchaseHistoryRepository;
		private readonly IBaseRepository<Item> _itemRepository;
		
		public PurchaseHistoryController(IBaseRepository<PurchaseHistory> purchaseHistoryRepository, IBaseRepository<Item> itemRepository)
		{
			_purchaseHistoryRepository = purchaseHistoryRepository;
			_itemRepository = itemRepository;
			
		}
		
		[HttpPost("purchase")]
		public async Task<IActionResult> Purchase([FromBody] PurchaseHistory purchaseHistory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			var item = await _itemRepository.GetByIdAsync(purchaseHistory.ItemId);
			if (item == null)
			{
				return NotFound();
			}
			
			var purchase = new PurchaseHistory
			{
				UserId = purchaseHistory.UserId,
				ItemId = purchaseHistory.ItemId,
				PurchaseDate = DateTime.Now
			};
			
			await _purchaseHistoryRepository.AddAsync(purchaseHistory);
			return Ok(purchase);
		}
	}
}