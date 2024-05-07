using Api.Dtos;
using Api.Models;
using APi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj_APIPharmacy.Data;


namespace Api.Controllers
{
    [Route("CommandeProduct")]
    [ApiController]
    public class CommandeProductController : ControllerBase
    {
        private readonly PharmacyDbContext db;

        public CommandeProductController(PharmacyDbContext db) { this.db = db; }

        [HttpGet]
        public async Task<List<CommandeProduct>> GetAllCommandeProduct()
        {
            return await this.db.OrderProducts.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<CommandeProduct>> AjouterCommandeProduct(DCommandeProduct ComPr)
        {
            var p = await db.OrderProducts.FirstOrDefaultAsync(pr => pr.PharmacyProductId == ComPr.PharmacyProductId && pr.CommandeId == ComPr.CommandeId);

            if (p == null)
            {
                var COMPhar = new CommandeProduct
                {
                    PharmacyProductId = ComPr.PharmacyProductId,
                    CommandeId = ComPr.CommandeId,
                    Quantity = ComPr.Quantity
                };

                db.OrderProducts.Add(COMPhar);
                await db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCommandeProductById), new { Id = COMPhar.Id }, COMPhar);
            }



            return Conflict($" La Commande Products Avec cet Id {p.Id} Deja Connue ");
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CommandeProduct>> GetCommandeProductById(Guid Id)
        {
            CommandeProduct p = await db.OrderProducts.FirstOrDefaultAsync(cmp => cmp.Id == Id);
            if (p != null)
            {
                return Ok(p);
            }
            return NotFound($"La Commande Products Avec cet Id {Id} N'Existe Pas !");
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> ModifierCommandeyProduct(Guid Id, DCommandeProduct ComProd)
        {
           CommandeProduct p = await db.OrderProducts.FirstOrDefaultAsync(pr => pr.Id == Id);

            if (p == null)
            {
                return NotFound();
            }
            if (p.PharmacyProductId != ComProd.PharmacyProductId && p.CommandeId != ComProd.CommandeId)
            { return Conflict(); }


            var CMP = new CommandeProduct
            {

                PharmacyProductId = ComProd.PharmacyProductId,
                CommandeId = ComProd.CommandeId,
                Quantity = ComProd.Quantity
            };

            await db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<CommandeProduct>> DeleteById(Guid Id)
        {
            CommandeProduct p = await db.OrderProducts.FirstOrDefaultAsync(pr => pr.Id == Id);
            if (p != null)
            {
                db.OrderProducts.Remove(p);
                await db.SaveChangesAsync();
                return NoContent();
            }
            return NotFound($"La Commande Product Avec cet Id {Id} N'Existe Pas ");
        }
        [HttpDelete("{IdPharmacieProduct}/{IdCommande}")]
        public async Task<ActionResult<CommandeProduct>> DeleteByCommandeProduct(Guid IdPharmacieProduct, Guid IdCommande)
        {
            CommandeProduct p = await db.OrderProducts.FirstOrDefaultAsync(pr => pr.PharmacyProductId == IdPharmacieProduct && pr.CommandeId == IdCommande);
            if (p != null)
            {
                db.OrderProducts.Remove(p);
                await db.SaveChangesAsync();
                return NoContent();
            }
            return NotFound($"La Commande Product Avec cet Id Pharmacy Product {IdPharmacieProduct} et Id Commande {IdCommande} N'Existe Pas ");
        }
    }
}
