using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVendas.Data;
using APIVendas.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly SistemaVendasDbContext _context;

    public ProdutoController(SistemaVendasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdutos()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> PostProduto([FromBody] ProdutosModel produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, [FromBody] ProdutosModel produto)
    {
        if (id != produto.Id) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
