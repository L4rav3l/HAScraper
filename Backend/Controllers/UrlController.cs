using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using HAScraper.Infrastructure;
using HAScraper.Request;

namespace HAScraper.Controllers;

[ApiController]

public class UrlController : ControllerBase
{
    private readonly Postgresql _postgresql;

    public UrlController(Postgresql postgresql)
    {
        _postgresql = postgresql;
    }

    [HttpPost("api/url")]
    public async Task<IActionResult> Url(UrlRequest request)
    {
        string url = request.Url;

        System.IO.File.WriteAllText("url.txt", url);

        return Ok();
    }
}