using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

		public UsersController(IUserRepository repository, IUserAuthenticationService userAuthenticationService) : base(repository)
		{
			_userAuthenticationService = userAuthenticationService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			var token = await _userAuthenticationService.Authenticate(request.Email, request.Password);
			if (token == null)
			{
				return Unauthorized();
			}
			return Ok(new { token });
		}

		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] SignupRequest request)
		{
			try
			{
				var user = await _userAuthenticationService.SignUp(request.Name, request.Email, request.Password);
				return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}
	}
}