using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Services
{
	public interface IUserAuthenticationService
	{
		Task<string> Authenticate(string email, string password);
		Task<User> SignUp(string name, string email, string password);
	}
}