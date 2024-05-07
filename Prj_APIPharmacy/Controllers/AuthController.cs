using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Dtos;
using Api.Models;
using Api.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[Route("Auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserRepo _userRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _config;

    public AuthController(IUserRepo userRepo, IPasswordHasher<User> passwordHasher, IConfiguration config)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(DLogin login)
    {
        var user = await _userRepo.FindByUserNameOrEmailAsync(login.UsernameOrEmail);

        if (user == null)
        {
            return NotFound();
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized();
        }

        return Ok(new
        {
            Token = GenerateToken(user)
        });
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var signedCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new ("aud", _config["Jwt:Audience"]!),

            new ("Id", user.Id.ToString()),
            
            new ("Name", user.Name),
            new ("Phone", user.Phone),
            new ("Email", user.Email),
            new ("Address", user.Address),

            new ("Username", user.Username ?? ""),

            new ("PharmacyId", user.PharmacyId.ToString() ?? ""),
            new ("CostPerKM", user.CostPerKM.ToString() ?? ""),
            
            new ("Role", user.Role.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signedCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(DRegister register)
    {
        // TODO: Validate Role
        // TODO: Validate Fields by roles

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

        var result = _passwordHasher.HashPassword(user, register.Password);

        user.Password = result;

        if (!await _userRepo.SaveAsync(user)) return BadRequest();

        return Ok(new
        {
            User = user,
            Token = GenerateToken(user)
        });
    }
}