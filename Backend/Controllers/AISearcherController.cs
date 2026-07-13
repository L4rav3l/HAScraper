using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using Newtonsoft.Json;
using Npgsql;
using HAScraper.Infrastructure;
using HAScraper.Request;

namespace HAScraper.Infrastructure;

[ApiController]
public class AiSearcherController : ControllerBase
{
    private readonly Postgresql _postgresql;
    private readonly string _geminiApi;

    public AiSearcherController(Postgresql postgresql)
    {
        _postgresql = postgresql;
        _geminiApi = Environment.GetEnvironmentVariable("API_TOKEN");
    }

    [HttpPost("api/searchwithai")]
    public async Task<IActionResult> AISearcher([FromBody] AiSearcherRequest request)
    {
        List<ProductsRequest> products = new List<ProductsRequest>();

        await using(var conn = await _postgresql.GetOpenConnectionAsync())
        {
            await using
        }

        return Ok();
    }
}