using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIVendas.Data;
using APIVendas.Models;


[ApiController]
[Route("api/[controller]")]
public class PedidoProdutoController : ControllerBase
{
    private readonly SistemaVendasDbContext _context;

    public PedidoProdutoController(SistemaVendasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidosProdutos()
    {
        var itens = await _context.PedidosProdutos.ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _context.PedidosProdutos.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> PostItem([FromBody] PedidosProdutosModel item)
    {
        _context.PedidosProdutos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(int id, [FromBody] PedidosProdutosModel item)
    {
        if (id != item.Id) return BadRequest();

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _context.PedidosProdutos.FindAsync(id);
        if (item == null) return NotFound();

        _context.PedidosProdutos.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
