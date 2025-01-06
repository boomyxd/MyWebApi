using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BaseEntityController<T> : ControllerBase where T : BaseEntity
	{
		private readonly IBaseRepository<T> _repository;

		public BaseEntityController(IBaseRepository<T> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public virtual async Task<IActionResult> GetAll()
		{
			var entities = await _repository.GetAllAsync();
			return Ok(entities);
		}

		[HttpGet("{id}")]
		public virtual async Task<IActionResult> GetById(Guid id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}
			return Ok(entity);
		}

		[HttpPost]
		public virtual async Task<IActionResult> Create(T entity)
		{
			await _repository.AddAsync(entity);
			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public virtual async Task<IActionResult> Update(Guid id, T entity)
		{
			if (id != entity.Id) return BadRequest();
			await _repository.UpdateAsync(entity);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public virtual async Task<IActionResult> Delete(Guid id)
		{
			await _repository.DeleteAsync(id);
			return NoContent();
		}
	}
}