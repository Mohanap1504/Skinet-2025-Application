using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IRepositoryProduct
{
  Task<IReadOnlyList<Product>> GetProductAsync(string? brand, string? type, string? sort);
  Task<Product?> GetProductByIdAsync(int id);
  Task<IReadOnlyList<string>> GetBrandsAsync();
  Task<IReadOnlyList<string>> GetTypesAsync();
  void AddProduct(Product product);
  void UpdateProduct(Product product);
  void DeleteProduct(Product product);
  bool ProductExsists(int id);
  Task<bool> SaveChnagesAsync();

}
