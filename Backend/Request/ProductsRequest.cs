namespace HAScraper.Infrastructure;

public class ProductsRequest
{
    private required int Id {get;set;}
    private required string Url {get;set;}
    private required int Price {get;set;}
    private string cpu {get;set;}
    private int memory {get;set;}
    private string ddr {get;set;}
    private string drive {get;set;}
    private required string bargain {get;set;}
    private required bool frozen {get;set;}
}