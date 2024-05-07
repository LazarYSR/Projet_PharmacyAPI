using Api.Dtos;
using Api.Enums;
using Api.Models;
using Api.Repos;
using APi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj_APIPharmacy.Data;

namespace Api.Controllers
{
    [Route("User")]
    [ApiController]

    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly PharmacyDbContext _dbContext;

        public UserController(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/User
        [HttpGet]
        [Admin]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Admin]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpGet("{role}")]
        [Admin]  
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(UserRole role)
        {
            IEnumerable<User> users;

            switch (role)
            {
                case UserRole.Client:
                    users = await _dbContext.Users.Where(u => u.Role == UserRole.Client).ToListAsync();
                    break;
                case UserRole.Deliverer:
                    users = await _dbContext.Users.Where(u => u.Role == UserRole.Deliverer).ToListAsync();
                    break;
                case UserRole.Pharmacist:
                    users = await _dbContext.Users.Where(u => u.Role == UserRole.Pharmacist).ToListAsync();
                    break;
                default:
                    return BadRequest("Invalid role specified.");
            }

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        [Admin]
        public async Task<IActionResult> CreateUser(DRegister register)
        {
            var user = new User
            {
                Name = register.Name,
                Phone = register.Phone,
                Email = register.Email,
                Address = register.Address,
                Username = register.Username,
                Role = register.Role,
                PharmacyId = register.PharmacyId,
                CostPerKM = register.CostPerKM
            };
            var result = await _userRepo.SaveAsync(user);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

         // PUT: api/User/5
        [HttpPut("{id}")]
        [Admin]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Admin]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(Guid id)
        {
            return _dbContext.Users.Any(e => e.Id == id);
        }


        [HttpPost("{id}/Activate")]
        [Admin]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            
            if (user.IsActive) // Vérifiez si l'utilisateur est déjà activé
            {
                return BadRequest("L'utilisateur est déjà activé.");
            }

            user.IsActive = true;   // Activez l'utilisateur 

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Ok("Compte activé avec succès.");
        }

        [HttpGet("UnactivatedAccounts")]
        [Admin]
        public async Task<ActionResult<IEnumerable<User>>> GetUnactivatedAccounts()
        {
            var unactivatedAccounts = await _dbContext.Users.Where(u => !u.IsActive).ToListAsync();

            if (unactivatedAccounts == null || !unactivatedAccounts.Any())
            {
                return NotFound("Aucun compte utilisateur non activé trouvé.");
            }

            return Ok(unactivatedAccounts);
        }

    }
}

/*[Authorize] 
public class UserController : Controller
{
    private readonly IUserRepo _userRepo;

    public UserController(IUserRepo userRepo)
    {
        _userRepo = userRepo;
<<<<<<< HEAD
    }

    [HttpGet("{id}")]
    [Admin]
    public async Task<IActionResult> GetById(Guid id)
    {
        var users = await _userRepo.GetUserByIdAsync(id);
        if (users == null)
        {
            return NotFound();
        }
        return Ok(users);
    }
=======
    }*/


/* [HttpGet("{role}")]
 [Admin]
 public async Task<IActionResult> Get(UserRole role)
 {
     var users = await _userRepo.AllAsync(role);

<<<<<<< HEAD
            return CreatedAtAction(nameof (GetById), new { id = user.Id }, user);
        }
=======
     return Ok(users);
 }
>>>>>>> a730b5c73918bc10d205dde2f80119db6dfe952d

 [HttpPost]
 [Admin]
 public async Task<IActionResult> createUser(DRegister register)
 {
     var user = new User
     {
         Name = register.Name,
         Phone = register.Phone,
         Email = register.Email,
         Address = register.Address,
         Username = register.Username,
         Role = register.Role,
         PharmacyId = register.PharmacyId,
         CostPerKM = register.CostPerKM
     };
      var result = await _userRepo.SaveAsync(user);
         if (!result)
         {
             return BadRequest();
         }

         return CreatedAtAction(nameof (Get(UserRole)), new { id = user.Id }, user);
     }

     // PUT: api/User/{id}
     [HttpPut("{id}")]
     [Admin]
     public async Task<IActionResult> UpdateUser(Guid id, DRegister register)
     {
         var existingUser = await _userRepo.GetUserByIdAsync(id);
         if (existingUser == null)
         {
             return NotFound();
         }

         // Mettre à jour les champs nécessaires
         existingUser.Name = register.Name;
         existingUser.Phone = register.Phone;
         existingUser.Email = register.Email;
         existingUser.Address = register.Address;
         existingUser.Username = register.Username;
         existingUser.Role = register.Role;
         existingUser.PharmacyId = register.PharmacyId;
         existingUser.CostPerKM = register.CostPerKM;

         var result = await _userRepo.UpdateAsync(existingUser);
         if (!result)
         {
             return BadRequest();
         }

         return NoContent();
     }

     // DELETE: api/User/{id}
     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteUser(Guid id)
     {
         var existingUser = await _userRepo.GetUserByIdAsync(id);
         if (existingUser == null)
         {
             return NotFound();
         }

         var result = await _userRepo.DeleteAsync(existingUser);
         if (!result)
         {
             return BadRequest();
         }

         return NoContent();
     }
 }
*/