using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
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

		public async Task<User> GetByRefreshTokenAsync(string refreshToken)
		{
			if (string.IsNullOrWhiteSpace(refreshToken))
				throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));

			return await _context.Users
				.Include(u => u.RefreshTokens)
				.SingleOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));
		}
		public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
		{
			await _context.RefreshTokens.AddAsync(refreshToken);
			await _context.SaveChangesAsync();
		}

		public async Task RevokeRefreshTokenAsync(string refreshToken)
		{
			var token = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);

			if (token != null)
			{
				token.IsRevoked = true;
				_context.RefreshTokens.Update(token);
				await _context.SaveChangesAsync();
			}
		}
	}
}