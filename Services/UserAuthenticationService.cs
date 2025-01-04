using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

		public async Task<string> Login(string email, string password)
		{
			var user = await _userRepository.GetUserByEmailAsync(email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			{
				throw new UnauthorizedAccessException("Invalid email or password.");
			}

			return GenerateJwtToken(user);
		}

		public async Task<User> SignUp(string firstName, string lastName, string email, string password)
		{
			var existingUser = await _userRepository.GetUserByEmailAsync(email);
			if (existingUser != null)
			{
				throw new Exception("User already exists.");
			}

			var user = new User
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
			};

			await _userRepository.AddAsync(user);
			return user;
		}

		private string GenerateJwtToken(User user)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}