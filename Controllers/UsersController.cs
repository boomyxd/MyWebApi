using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.CustomAttributes;
using MyWebApi.DTOs;
using MyWebApi.Enums;
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
				var refreshTokenEntity = new RefreshToken
				{
					Token = refreshToken,
					Expires = DateTime.UtcNow.AddDays(7),
					UserId = user.Id,
				};
				user.RefreshTokens.Add(refreshTokenEntity);
				
				await _repository.UpdateAsync(user);
				
				return Ok(new
				{
					AccessToken = token,
					RefreshToken = refreshToken
				});
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
				if (dto.Role == default)
				{
					dto.Role = UserRole.Customer;
				}
				var user = await _userAuthenticationService.SignUp(dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.Role);
				
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
			
			if (!ModelState.IsValid) return BadRequest(ModelState);

			try
			{
				var tokens = await _userAuthenticationService.RefreshToken(dto.RefreshToken);

				if (string.IsNullOrEmpty(tokens.AccessToken) || string.IsNullOrEmpty(tokens.RefreshToken))
				{
					return Unauthorized(new { message = "Invalid refresh token" });
				}

				return Ok(new
				{
					AccessToken = tokens.AccessToken,
					RefreshToken = tokens.RefreshToken
				});
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}
		[AdminAuthorize]
		[HttpGet("admin-data")]
		public IActionResult GetData()
		{
			return Ok(new { message = "Du er admin!" });
		}
	}
}