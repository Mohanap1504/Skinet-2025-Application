using System;
using Core.Entities;

namespace Core.Specificatio;

public class TypeSpecification : BaseSpecification<Product, string>
{
public TypeSpecification()
{
    AddSelect(x => x.Type);
    ApplyDistinct();
}
}
