using System;

namespace Core.Specificatio;

public class ProductSpecParam
{
     private const int MaxPageSize = 50;
     public int PageIndex { get; set; } = 1;
 
     private int _pageSize = 20;
     public int PageSize
     {
         get => _pageSize;
         set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
     }
  private List<string> _brands = [];
  public List<string> Brand
  {
    get => _brands;
    set 
    {
      _brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
       }
  }
  private List<string> _types = [];
  public List<string> Type
  {
    get => _types;
    set 
    {
      _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
       }
  }
  public string? Sort {get; set;}
  private string? _search;
     public string Search
     {
         get => _search ?? "";
         set => _search = value.ToLower();
     }
     
}
