using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs;
using MyWebApi.Models;
using MyWebApi.Repositories;
using MyWebApi.Services;

namespace MyWebApi.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UsersController : BaseEntityController<User>
	{
		private readonly IUserAuthenticationService _userAuthenticationService;
		private readonly IUserRepository _repository;

		public UsersController(IUserRepository repository, IUserAuthenticationService userAuthenticationService) : base(repository)
		{
			_userAuthenticationService = userAuthenticationService;
			_repository = repository;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserForLoginDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var token = await _userAuthenticationService.Login(dto.Email, dto.Password);

				if (string.IsNullOrEmpty(token))
				{
					return Unauthorized(new { message = "Invalid email or password" });
				}

				var user = await _repository.GetUserByEmailAsync(dto.Email);

				var refreshToken = await _userAuthenticationService.GenerateRefreshToken();
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
				
				await _repository.UpdateAsync(user);
				
				return Ok(new { message = "User successfully logged in" });
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}

		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] UserForRegistrationDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			try
			{
				var user = await _userAuthenticationService.SignUp(dto.FirstName, dto.LastName, dto.Email, dto.Password);
				
				var responseDto = new UserForResponseDto
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email
				};
				
				return CreatedAtAction(nameof(GetById), new { id = user.Id }, responseDto);
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}
		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh(TokenRefreshRequestDto dto)
		{
			// Continue here
			
			var user = await 
		}
	}
}