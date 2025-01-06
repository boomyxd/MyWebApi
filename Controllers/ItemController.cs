using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Repositories;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

public class ItemController : BaseEntityController<Item>
{
    
    
    public ItemController(IBaseRepository<Item> repository) : base(repository)
    {
        
    }
    
}