using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}