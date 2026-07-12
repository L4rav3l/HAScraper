using Microsoft.AspNetCore.Mvc;
using HAScraper.Infrastructure;
using Npgsql;

namespace HAScraper.Controllers;

[ApiController]

public class ProductsController : ControllerBase
{
    private readonly Postgresql _postgresql;

    public ProductsController(Postgresql postgresql)
    {
        _postgresql = postgresql;
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> productRead()
    {
        
    }
}