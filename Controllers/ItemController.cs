using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs;
using MyWebApi.Models;
using MyWebApi.Repositories;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController : BaseEntityController<Item>
{
	private readonly IBaseRepository<Item> _repository;
	public ItemController(IBaseRepository<Item> repository) : base(repository)
	{
		_repository = repository;
	}

	[HttpPost("additem")]
	public async Task<ActionResult> Create([FromBody] ItemForCreationDto dto)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var item = new Item
		{
			Name = dto.Name,
			Price = dto.Price,
		};
		
		await _repository.AddAsync(item);
		return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
	}
	[HttpGet("{id:guid}")]
	public async Task<ActionResult> GetById(Guid id)
	{
		var item = await _repository.GetByIdAsync(id);
		
		if (item == null) return NotFound();
		
		return Ok(item);
	}

	[HttpGet("getall")]
	public async Task<ActionResult> GetAll()
	{
		var items = await _repository.GetAllAsync();
		
		if (items == null || !items.Any()) return NoContent();
		
		return Ok(items);
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchItems([FromQuery] string name)
	{
		if (string.IsNullOrWhiteSpace(name)) return BadRequest();
		
		var items = await _repository.SearchByNameAsync(name);
		
		return Ok(items);
	}
	
	
	
	
}