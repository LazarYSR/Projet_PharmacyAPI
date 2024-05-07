using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repos.Fake
{
    public class FProductRepo : IProductRepo
    {
        private readonly List<Product> _products = new();

        public FProductRepo()
        {
            for (int i = 0; i < 100; i++)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product {i}",
                    Description = $"Description {i}",
                    Image = $"Image {i}",
                    Barcode = $"Barcode {i}",
                    CategoryId = Guid.NewGuid() // TODO: Populate category ID as needed
                };

                _products.Add(product);
            }
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetProductByIdAsync(Guid id)
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public Task<bool> SaveAsync(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
                _products.Add(product);
            }
            else
            {
                var index = _products.FindIndex(p => p.Id == product.Id);
                if (index == -1) return Task.FromResult(false);

                _products[index] = product;
            }

            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return Task.FromResult(false);
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Image = product.Image;
            existingProduct.Barcode = product.Barcode;
            existingProduct.CategoryId = product.CategoryId;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return Task.FromResult(false);
            }

            _products.Remove(existingProduct);
            return Task.FromResult(true);
        }
    }
}
