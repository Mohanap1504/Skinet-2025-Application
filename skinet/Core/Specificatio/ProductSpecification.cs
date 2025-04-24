using System;
using Core.Entities;

namespace Core.Specificatio;

public class ProductSpecification : BaseSpecification<Product>
{
  public ProductSpecification(ProductSpecParam productSpecParam) : base(x => 
  (string.IsNullOrEmpty(productSpecParam.Search) || x.Name.ToLower().Contains(productSpecParam.Search)) &&
  (!productSpecParam.Brand.Any() || productSpecParam.Brand.Contains(x.Brand)) && 
  (!productSpecParam.Type.Any() || productSpecParam.Type.Contains(x.Type)))
  { 
    ApplyPaging(productSpecParam.PageSize * (productSpecParam.PageIndex-1), productSpecParam.PageSize);
    switch (productSpecParam.Sort)
    {
      case "priceAsc":
         AddOrderBy(x => x.Price);
         break;
      case "priceDesc":
         AddOrderByDescending(x => x.Price);
         break;   
      default:
         AddOrderBy(x => x.Name);
         break;
    }
  }
}
