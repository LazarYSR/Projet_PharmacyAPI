using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APi.Filters;
using Prj_APIPharmacy.Data;

namespace Api.Controllers
{
    [Route("Product")]
    [ApiController]
    [Admin]
    [Pharmacy]
    public class ProductController : ControllerBase
    {
        private readonly PharmacyDbContext _dbContext;

        public ProductController(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Product
        [HttpGet]
        
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            if (products == null || !products.Any())
            {
                return NotFound("");
            }
            List<Product> Prds = await _dbContext.Products.ToListAsync();
            return Ok(Prds);
        }
      
        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
      
        private bool ProductExists(Guid id)
        {
            return _dbContext.Products.Any(e => e.Id == id);
        }

        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(Guid CategoryId)
        {
            var products = await _dbContext.Products.Where(p => p.CategoryId == CategoryId).ToListAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }
    }

}




   /* //  GET: api/Product
      [HttpGet]
       public async Task<IActionResult> GetAllProducts()
       {
          var products = await _productRepo.GetAllProductsAsync();
         return Ok(products);
      }

//        GET: api/Product/{id}
        [HttpGet("{id}")]
       public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
           if (product == null)
          {
              return NotFound();
          }
          return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
       {
            var result = await _productRepo.SaveAsync(product);
           if (!result)
          {
               return BadRequest();
           }

           return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
       }

       // PUT: api/Product/{id}
        [HttpPut("{id}")]
       public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            var existingProduct = await _productRepo.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Mettre à jour les champs nécessaires
           existingProduct.Name = product.Name;
          existingProduct.Description = product.Description;
           existingProduct.Image = product.Image;
          existingProduct.Barcode = product.Barcode;
           existingProduct.CategoryId = product.CategoryId;

           var result = await _productRepo.UpdateAsync(existingProduct);
           if (!result)
            {
               return BadRequest();
            }

           return NoContent();
        }

      // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var existingProduct = await _productRepo.GetProductByIdAsync(id);
           if (existingProduct == null)
           {
               return NotFound();
         }

        var result = await _productRepo.DeleteAsync(existingProduct);
           if (!result)
           {
              return BadRequest();
          }

           return NoContent();
     }

    */