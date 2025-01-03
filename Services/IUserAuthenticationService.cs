using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public interface IUserAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
    }
}