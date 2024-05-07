using Api.Models;
using APi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj_APIPharmacy.Data;

namespace Api.Controllers
{
    [Route("CategoryController")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly PharmacyDbContext _DbContext;
        public CategoryController(PharmacyDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        [Admin]
        [Pharmacy]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (_DbContext.Categories == null)
            {
                return NotFound();
            }
            return await _DbContext.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        [Admin] // Contrôle d'accès pour le rôle "Admin"
        [Pharmacy] // Contrôle d'accès pour le rôle "Pharmacie"
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_DbContext.Categories == null)
            {
                return NotFound();
            }

            var category = await _DbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        [Admin] 
        [Pharmacy] 
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _DbContext.Categories.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{Id}")]
        [Admin] 
        public async Task<ActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();

            }
            _DbContext.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                await _DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;

                }
            }
            return Ok();
        }
        private bool CategoryAvailable(Guid id)
        {
            return (_DbContext.Categories?.Any(x => x.Id == id)).GetValueOrDefault();
        }

      
        }
    }
