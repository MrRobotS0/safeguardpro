using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apisafeguardpro.Context;
using apisafeguardpro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace apisafeguardpro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public UsuarioController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
    {
        var colaborador = await _context.Colaboradors.FirstOrDefaultAsync(e => e.Email == model.Email && e.Cpf == model.Cpf);
        if (colaborador is null)
        {
            return BadRequest("Colaborador não cadastrado.");
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Cpf = model.Cpf!
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest("Não foi possível criar o usuário.");
        }

        await _userManager.AddToRoleAsync(user, "Basic");
        var roles = await _userManager.GetRolesAsync(user);

        return BuildToken(model, roles);
    }

    [HttpGet("Check")]
    public async Task<ActionResult<string>> CheckUser([FromQuery] string cpf, [FromQuery] string email)
    {
        var jaCadastrado = await _context.Users.AnyAsync(u => u.Email == email && u.Cpf == cpf);
        if (jaCadastrado)
        {
            return BadRequest("Usuário já cadastrado.");
        }

        var colaboradorExiste = await _context.Colaboradors.AnyAsync(e => e.Email == email && e.Cpf == cpf);
        if (!colaboradorExiste)
        {
            return BadRequest("Colaborador não cadastrado.");
        }

        return "OK";
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return BadRequest("Login inválido.");
        }

        var user = await _userManager.FindByNameAsync(userInfo.Email);
        var roles = await _userManager.GetRolesAsync(user!);

        return BuildToken(userInfo, roles);
    }

    private UserToken BuildToken(UserInfo userInfo, IList<string> userRoles)
    {
        var idCol = 0;
        var colaborador = _context.Colaboradors.FirstOrDefault(c => c.Email == userInfo.Email);
        if (colaborador is not null)
        {
            idCol = colaborador.ColaboradorCod;
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);

        var token = new JwtSecurityToken(claims: claims, expires: expiration, signingCredentials: creds);

        return new UserToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Roles = userRoles,
            IdCol = idCol
        };
    }
}
