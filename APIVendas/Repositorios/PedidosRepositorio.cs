
using APIVendas.Data;
using APIVendas.Models;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Repositorios
{
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly SistemaVendasDbContext _dbContext;

        public PedidosRepositorio(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PedidosModel> Adicionar(PedidosModel pedido)
        {
            await _dbContext.Pedidos.AddAsync(pedido);
            await _dbContext.SaveChangesAsync();

            return pedido;
        }

        public async Task<bool> Apagar(int id)
        {
            PedidosModel pedidoPorId = await BuscarPorId(id);

            if (pedidoPorId == null)
            {
                throw new Exception("Pedido não encontrado.");
            }

            _dbContext.Pedidos.Remove(pedidoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PedidosModel> Atualizar(PedidosModel pedido, int id)
        {
            PedidosModel pedidoPorId = await BuscarPorId(id);

            if (pedidoPorId == null)
            {
                throw new Exception($"Pedido do Id: {id} não encontrado.");
            }

            pedidoPorId.UsuarioId = pedido.UsuarioId;
            pedidoPorId.EnderecoEntrega = pedido.EnderecoEntrega;
            pedidoPorId.Status = pedido.Status;
            pedidoPorId.MetodoPagamento = pedido.MetodoPagamento;

            await _dbContext.SaveChangesAsync();

            return pedidoPorId;
        }

        public async Task<PedidosModel> BuscarPorId(int id)
        {
            return await _dbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidosProdutos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PedidosModel>> BuscarTodosPedidos()
        {
            return await _dbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidosProdutos)
                .ToListAsync();
        }
    }
}
