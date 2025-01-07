using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
	public interface IBaseRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(Guid id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(Guid id);
		Task<IEnumerable<Item>> SearchByNameAsync(string name);
	}
}