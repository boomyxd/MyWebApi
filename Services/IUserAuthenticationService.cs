using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Enums;
using MyWebApi.Models;

namespace MyWebApi.Services
{
	public interface IUserAuthenticationService
	{
		Task<string> Login(string email, string password);
		Task<User> SignUp(string firstName, string lastName, string email, string password, UserRole role);
		Task<string> GenerateRefreshToken();
		Task<(string AccessToken, string RefreshToken)> RefreshToken(string refreshToken);
	}
}