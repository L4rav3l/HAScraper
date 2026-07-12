namespace HAScraper.Request;

public class ProductsRequest
{
    public required int Id {get;set;}
    public required string Url {get;set;}
    public required int Price {get;set;}
    public string? Cpu {get;set;}
    public int? Memory {get;set;}
    public string? Ddr {get;set;}
    public string? Drive {get;set;}
    public required string Bargain {get;set;}
    public required bool Frozen {get;set;}
}