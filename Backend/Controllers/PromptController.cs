using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using HAScraper.Infrastructure;
using HAScraper.Request;

namespace HAScraper.Controllers;

[ApiController]
public class PromptController : ControllerBase
{
    public readonly Postgresql _postgresql;

    public PromptController(Postgresql postgresql)
    {
        _postgresql = postgresql;
    }

    [HttpPost("api/prompt")]
    public async Task<IActionResult> Prompt(PromptRequest request)
    {
        string prompt = request.Prompt;

        System.IO.File.WriteAllText("prompt.txt", prompt);

        return Ok();
    }

    [HttpGet("api/prompt")]
    public async Task<IActionResult> GetPrompt()
    {
        string prompt = System.IO.File.ReadAllText("prompt.txt");
        return Ok(prompt);
    }
}