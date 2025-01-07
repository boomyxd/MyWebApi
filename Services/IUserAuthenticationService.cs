using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Services
{
	public interface IUserAuthenticationService
	{
		Task<string> Login(string email, string password);
		Task<User> SignUp(string firstName, string lastName, string email, string password);
		Task<string> GenerateRefreshToken();        // Fix dette når du kommer hjem
													//Task<User> ValidateRefreshToken(string refreshToken);
	}
}