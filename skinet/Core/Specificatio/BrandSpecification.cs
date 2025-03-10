using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specificatio;

public class BrandSpecification : BaseSpecification<Product, string>
{
  public BrandSpecification()
  {
    AddSelect(x => x.Brand);
    ApplyDistinct();

  }
}
