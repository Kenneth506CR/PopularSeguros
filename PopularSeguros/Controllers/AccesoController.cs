using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PopularSeguros.DTOs;
using PopularSeguros.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly DbPolizasContext _context;

    public AuthController(IConfiguration configuration, DbPolizasContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        // Busca el usuario en la base de datos
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Usuario1 == loginDto.Usuario && u.Contrasena == loginDto.Contrasena);

        if (usuario == null)
        {
            return Unauthorized("Usuario o contraseña incorrectos.");
        }

        // Genera el token JWT
        var token = GenerateJwtToken(usuario.Usuario1);
        return Ok(new { Token = token });
    }


    private string GenerateJwtToken(string username)
    {
        var keyString = _configuration["Jwt:Key"];

        if (string.IsNullOrEmpty(keyString))
        {
            throw new ArgumentNullException("Jwt:Key", "El valor de Jwt:Key no está configurado en appsettings.json.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
