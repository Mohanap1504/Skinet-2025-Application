using System;
using System.Threading.Tasks.Dataflow;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IRepositoryProduct repo) : ControllerBase
{

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string? brand, string? type, string? sort)
  {
    return Ok(await repo.GetProductAsync(brand, type, sort));
  }

  [HttpGet("{id:int}")] //api/products/id
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await repo.GetProductByIdAsync(id);
    if (product == null) return NotFound();

    return product;
  }
  
  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct( Product product)
  {
    repo.AddProduct(product);
    if( await repo.SaveChnagesAsync())
    {
      return CreatedAtAction("GetProduct", new {id = product.Id}, product);
    }

    return BadRequest("Problem creating product");
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> updateproduct(int id, Product product)
  {
     if(product.Id != id || !ProductExists(id)) return BadRequest("Cannot update this product");

     repo.UpdateProduct(product);

     if(await repo.SaveChnagesAsync())
     {
       return NoContent();
     }
      return BadRequest("Problem Updating the product");
  }
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> deleteProduct(int id)
  {
    var product = await repo.GetProductByIdAsync(id);
    if(product == null) return NotFound();

    repo.DeleteProduct(product);

    if(await repo.SaveChnagesAsync())
     {
       return NoContent();
     }
      return BadRequest("Problem Deleting the product");
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  {
    return Ok(await repo.GetBrandsAsync());
  }

[HttpGet("types")]
public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
{
  return Ok(await repo.GetTypesAsync());
}
  private bool ProductExists(int id)
  {
    return repo.ProductExsists(id);
  }
}
