using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using APIVendas.Data;
using APIVendas.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly SistemaVendasDbContext _dbContext;

        public ContaController(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UsuariosModel usuario)
        {
            // 🔹 Forma estática (teste rápido)
            if (usuario.Nome == "admin" && usuario.Senha == "1234")
            {
                var token = GerarToken(usuario);
                return Ok(new { token });
            }


            //var usuarioExistente = _dbContext.Usuarios
            //    .FirstOrDefault(u => u.Nome == login.Nome && u.Senha == login.Senha);

            //if (usuarioExistente != null)
            //{
            //    var token = GerarToken(login);
            //    return Ok(new { token });
            //}

            return Unauthorized(new { mensagem = "Credenciais inválidas. Verifique e tente novamente." });
        }

        private string GerarToken(UsuariosModel usuario)
        {
            string chaveSecreta = "minha-chave-super-secreta-apivendas"; // ⚠️ ideal mover para appsettings.json

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("UsuarioSistemaVendas", "true")
            };

            var token = new JwtSecurityToken(
                issuer: "APIVendas",
                audience: "APIVendas",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
