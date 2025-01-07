using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Database;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{

		private readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;

		public BaseRepository(AppDbContext context)
		{
			this._context = context;
			this._dbSet = context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

		public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<Item>> SearchByNameAsync(string name)
		{
			return await _context.Items
				.Where(i => EF.Functions.Like(i.Name, $"%{name}%"))
				.ToListAsync();
		}

	}
}