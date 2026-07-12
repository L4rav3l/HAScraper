using AngleSharp.Html.Parser;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Mscc.GenerativeAI;
using Newtonsoft.Json;
using Npgsql;

namespace HAScraper.Infrastructure;

public class Scanner : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Postgresql _postgresql;
    private readonly string _geminiApi;

    public Scanner(IHttpClientFactory httpClientFactory, Postgresql postgresql)
    {
        _postgresql = postgresql;
        _httpClientFactory = httpClientFactory;
        _geminiApi = Environment.GetEnvironmentVariable("API_TOKEN");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var googleAI = new GoogleAI(_geminiApi);
        var model = googleAI.GenerativeModel("gemini-3.1-flash-lite");
    
        while(!stoppingToken.IsCancellationRequested)
        {
            var currentLinks = new HashSet<string>();

            try
            {
                using HttpClient client = _httpClientFactory.CreateClient("ScraperClient");

                int offset = 0;

                while(true)
                {
                    string rawUrl = File.ReadAllText("url.txt");
                    string url = $"{rawUrl}?offset={offset}";
                    string html = await client.GetStringAsync(url, stoppingToken);

                    var parser = new HtmlParser();
                    var document = parser.ParseDocument(html);

                    var products = document.QuerySelectorAll(".uad-col.uad-col-title h1 a");

                    foreach(var product in products)
                    {
                        currentLinks.Add(product.GetAttribute("href"));
                    }

                    var button = document.QuerySelector("a[rel='next']");

                    if(button == null)
                    {
                        break;
                    } else {
                        offset+=100;
                    }
                }

                foreach(var link in currentLinks)
                {
                    try
                    {
                        string productHtml = await client.GetStringAsync(link, stoppingToken);

                        var productParser = new HtmlParser();
                        var productDocument = productParser.ParseDocument(productHtml);

                        var price = productDocument.QuerySelector(".text-center.text-md-left");
                        var description = productDocument.QuerySelector(".mb-3.rtif-content");
                        var title = productDocument.QuerySelector(".uad-content-block");
                        var frozen = productDocument.QuerySelectorAll(".fa.fa-snowflake.fa-fw");
                        
                        string priceRaw = Regex.Replace(price.TextContent, @"[^\d]", "");
                        string titleText = title.TextContent.Trim();
                        string descriptionText = description.TextContent.Trim();
                        bool frozenStatus = false;

                        if(frozen.Length > 0)
                        {
                            frozenStatus = true;
                        } else {
                            frozenStatus = false;
                        }

                        await using(var conn = await _postgresql.GetOpenConnectionAsync())
                        {
                            await using(var check = new NpgsqlCommand("UPDATE products SET frozen = @frozen WHERE url = @url RETURNING id", conn))
                            {
                                check.Parameters.AddWithValue("url", link);
                                check.Parameters.AddWithValue("frozen", frozenStatus);

                                await using(var reader = await check.ExecuteReaderAsync())
                                {
                                    if(await reader.ReadAsync())
                                    {
                                        Console.WriteLine("DUPE!");
                                        continue;
                                    } else {
                                        Console.WriteLine("NEW!");
                                    }
                                }
                            }
                        }

                        string rawPrompt = File.ReadAllText("prompt.txt");

                        string prompt = $"{rawPrompt} PRICE: {priceRaw} Ft, Title: {titleText}, Descriptions: {descriptionText}";
                        var response = await model.GenerateContent(prompt);
                        string answer = response.Text;

                        Console.WriteLine(answer);

                        dynamic datas = Newtonsoft.Json.JsonConvert.DeserializeObject(answer);

                        using(var conn = await _postgresql.GetOpenConnectionAsync())
                        {
                            await using(var insert = new NpgsqlCommand("INSERT INTO products (url, price, cpu, memory, ddr, drive, extras, bargain, frozen) VALUES (@url, @price, @cpu, @memory, @ddr, @drive, @extras, @bargain, @frozen)", conn))
                            {
                                insert.Parameters.AddWithValue("url", link);
                                insert.Parameters.AddWithValue("price", Convert.ToInt32(priceRaw));
                                insert.Parameters.AddWithValue("cpu", datas?.cpu?.ToString() ?? (object)DBNull.Value);
                                insert.Parameters.AddWithValue("memory", datas?.memory != null ? Convert.ToInt32(datas.memory) : DBNull.Value);
                                insert.Parameters.AddWithValue("ddr", datas?.ddr?.ToString() ?? (object)DBNull.Value);
                                insert.Parameters.AddWithValue("drive", datas?.drive?.ToString() ?? (object)DBNull.Value);
                                insert.Parameters.AddWithValue("extras", datas?.extras?.ToString() ?? (object)DBNull.Value);
                                insert.Parameters.AddWithValue("bargain", datas?.bargain?.ToString() ?? (object)DBNull.Value);
                                insert.Parameters.AddWithValue("frozen", frozenStatus);

                                await insert.ExecuteNonQueryAsync();
                            }

                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Hiba a link feldolgozása során ({link}): {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
            }

            catch(Exception ex)
            {

            }
        }
    }
}