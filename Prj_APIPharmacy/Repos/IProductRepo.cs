using Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repos
{
    public interface IProductRepo
    {
        Task<bool> SaveAsync(Product product);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
    }
}
