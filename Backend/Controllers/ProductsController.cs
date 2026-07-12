using Microsoft.AspNetCore.Mvc;
using HAScraper.Infrastructure;
using HAScraper.Request;
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

        List<ProductsRequest> products = new List<ProductsRequest>();

        await using(var conn = await _postgresql.GetOpenConnectionAsync())
        {
            await using(var productsQuery = new NpgsqlCommand("SELECT * FROM products", conn))
            {
                await using(var reader = await productsQuery.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var product = new ProductsRequest
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Url = reader.GetString(reader.GetOrdinal("url")),
                            Price = reader.GetInt32(reader.GetOrdinal("price")),
                            Bargain = reader.GetString(reader.GetOrdinal("bargain")),
                            Frozen = reader.GetBoolean(reader.GetOrdinal("frozen")),
                            Cpu = reader.IsDBNull(reader.GetOrdinal("cpu")) ? null : reader.GetString(reader.GetOrdinal("cpu")),
                            Memory = reader.IsDBNull(reader.GetOrdinal("memory")) ? null : reader.GetInt32(reader.GetOrdinal("memory")),
                            Drive = reader.IsDBNull(reader.GetOrdinal("drive")) ? null : reader.GetString(reader.GetOrdinal("drive")),
                        };

                        products.Add(product);
                    }
                }
            }
        }

        return Ok(products);
    }

    [HttpDelete("api/products")]
    public async Task<IActionResult> productDelete()
    {
        await using(var conn = await _postgresql.GetOpenConnectionAsync())
        {
            await using(var delete = new NpgsqlCommand("DELETE FROM products", conn))
            {
                await delete.ExecuteReaderAsync();
            }
        }

        return Ok();
    }
}