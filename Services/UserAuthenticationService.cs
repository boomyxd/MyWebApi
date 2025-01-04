using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services
{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;

		public UserAuthenticationService(IUserRepository userRepository, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_configuration = configuration;
		}

		public async Task<string> Authenticate(string email, string password)
		{
			var user = await _userRepository.GetUserByEmailAsync(email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			{
				return null;
			}

			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new System.Security.Claims.ClaimsIdentity(new[]
				{
					new System.Security.Claims.Claim("id", user.Id.ToString()),
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public async Task<User> SignUp(string name, string email, string password)
		{
			var existingUser = await _userRepository.GetUserByEmailAsync(email);
			if (existingUser != null)
			{
				throw new Exception("User already exists.");
			}

			var user = new User
			{
				Name = name,
				Email = email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
			};

			await _userRepository.AddAsync(user);
			return user;
		}
	}
}