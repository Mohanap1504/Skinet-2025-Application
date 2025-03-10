using System;
using System.Threading.Tasks.Dataflow;
using Core.Entities;
using Core.Interfaces;
using Core.Specificatio;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IGenericRepository<Product> repo) : ControllerBase
{

  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(string? brand, string? type, string? sort)
  {
    var spec = new ProductSpecification(brand, type, sort);
    var products = await repo.ListAsync(spec);

    return Ok(products);
  }

  [HttpGet("{id:int}")] //api/products/id
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await repo.GetByIdAsync(id);
    if (product == null) return NotFound();

    return product;
  }
  
  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct( Product product)
  {
    repo.Add(product);
    if( await repo.SaveAllAync())
    {
      return CreatedAtAction("GetProduct", new {id = product.Id}, product);
    }

    return BadRequest("Problem creating product");
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> updateproduct(int id, Product product)
  {
     if(product.Id != id || !ProductExists(id)) return BadRequest("Cannot update this product");

     repo.Update(product);

     if(await repo.SaveAllAync())
     {
       return NoContent();
     }
      return BadRequest("Problem Updating the product");
  }
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> deleteProduct(int id)
  {
    var product = await repo.GetByIdAsync(id);
    if(product == null) return NotFound();

    repo.Remove(product);

    if(await repo.SaveAllAync())
     {
       return NoContent();
     }
      return BadRequest("Problem Deleting the product");
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  {
    var spec = new BrandSpecification();
    return Ok(await repo.ListAsync(spec));
  }

[HttpGet("types")]
public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
{
   var spec = new TypeSpecification();
   return Ok(await repo.ListAsync(spec));
}
  private bool ProductExists(int id)
  {
    return repo.Exists(id);
  }
}
