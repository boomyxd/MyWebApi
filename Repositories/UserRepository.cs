using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Database;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}