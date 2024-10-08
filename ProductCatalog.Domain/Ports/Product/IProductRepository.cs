﻿using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports
{
    public interface IProductRepository
    {
        Task<(IEnumerable<ProductModel>, int TotalCount)> GetAllAsync(int? pageNumber, int? pageSize);
        Task<ProductModel> GetByIdAsync(string id);
        Task<bool> ProductExist(string id);
        Task<ProductModel> AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(string id);
    }
}
