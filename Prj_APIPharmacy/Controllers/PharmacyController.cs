using Api.Dtos;
using Api.Models;
using APi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj_APIPharmacy.Data;

namespace Api.Controllers
{
    [Route("Pharmacy")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly PharmacyDbContext db;

        public PharmacyController(PharmacyDbContext db) { this.db = db; }

        [HttpGet]
        [Pharmacy]
        [Admin]

        public async Task<List<Pharmacy>> GetAllPharmacies() 
        {
            return await this.db.Pharmacies.ToListAsync();
        }
        //[HttpPost]
        //public async Task<ActionResult<Pharmacy>> AjouterPharmacy(DPharmacy pharmacy)
        //{
        //    var p = await db.Pharmacies.FirstOrDefaultAsync(pr => pr.Name == pharmacy.Name);
        //    if(p == null)
        //    {
        //        var Phar = new Pharmacy {
        //            Name = pharmacy.Name,
        //            Address = pharmacy.Address,
        //            Email = pharmacy.Email,
        //            Latitude = pharmacy.Latitude,
        //            Longitude = pharmacy.Longitude           
        //        };
        //        db.Pharmacies.Add(Phar);
        //        await db.SaveChangesAsync();
        //        return CreatedAtAction(nameof(GetByName), new { Name = pharmacy.Name},pharmacy);
        //    }


            
        //    return Conflict($" La Pharmacie Avec cet Nom {pharmacy.Name} Deja Connue ");
        //}
        [HttpGet("{nom}" )]
        public async Task<ActionResult<Pharmacy>> GetByName(string nom)
        {
            Pharmacy p = await db.Pharmacies.FirstOrDefaultAsync(pr => pr.Name == nom);
            if (p != null)
            {
                return Ok(p);
            }
            return NotFound($"La Pharmacie Avec cet Nom {nom} N'Existe Pas !");
        }
        [HttpPut("{Id}")]
          public async Task<IActionResult> Modifier(Guid Id,DPharmacy pharma)
        {
            Pharmacy p = await db.Pharmacies.FirstOrDefaultAsync(pr => pr.Id == Id);
            
            if (p == null)
            {
                return NotFound();
            }
            if (p.Name == pharma.Name)
            { return Conflict(); }

          
            p.Name = pharma.Name;
            p.Address = pharma.Address;
            p.Phone = pharma.Phone;
            p.Longitude = pharma.Longitude;
            p.Latitude = pharma.Latitude;
            p.Email = pharma.Email;

            await db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{nom}")]
        public async Task<ActionResult<Pharmacy>> DeleteByName(string nom)
        {
            Pharmacy p = await db.Pharmacies.FirstOrDefaultAsync(pr => pr.Name == nom);
            if (p != null)
            {
                db.Pharmacies.Remove(p);
                await db.SaveChangesAsync();
                return NoContent();
            }
            return NotFound($"La Pharmacie Avec cet Nom {nom} N'Existe Pas ");
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Pharmacy>> Delete(Guid Id)
        {
            Pharmacy p = await db.Pharmacies.FirstOrDefaultAsync(pr => pr.Id == Id);
            if (p != null)
            {
                db.Pharmacies.Remove(p);
                await db.SaveChangesAsync();
                return NoContent();
            }
            return NotFound($"La Pharmacie Avec cet Id {Id} N'Existe Pas ");
        }
        
        [HttpDelete("{Id}")]
        [Admin]
        public async Task<ActionResult<Pharmacy>> DeleteBiId(Guid Id)
        {
            // Recherchez le PharmacyProduct par son identifiant
            var pharmacyProduct = await db.PharmacyProducts.FindAsync(Id);
            if (pharmacyProduct == null)
            {
                return NotFound($"La Pharmacie Avec cet Id {Id} N'Existe Pas !");
            }

            try
            {
                var idPharmacyProducts = db.PharmacyProducts.Where(ip => ip.Id == Id);
                Pharmacy p = await db.Pharmacies.FindAsync(Id);
                db.PharmacyProducts.RemoveRange(idPharmacyProducts);
                db.Pharmacies.Remove(p);
              
                await db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"Une erreur s'est produite lors de la suppression des IdPharmacyProduct: {ex.Message}");
            }
     
        }
    }
}
