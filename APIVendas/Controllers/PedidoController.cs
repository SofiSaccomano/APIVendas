using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVendas.Data;
using APIVendas.Models;


[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly SistemaVendasDbContext _context;

    public PedidoController(SistemaVendasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Usuario)
            .Include(p => p.PedidosProdutos)
            .ToListAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPedido(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Usuario)
            .Include(p => p.PedidosProdutos)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> PostPedido([FromBody] PedidosModel pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPedido(int id, [FromBody] PedidosModel pedido)
    {
        if (id != pedido.Id) return BadRequest();

        _context.Entry(pedido).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}