
using APIVendas.Data;
using APIVendas.Models;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Repositorios
{
    public class PedidosProdutosRepositorio : IPedidosProdutosRepositorio
    {
        private readonly SistemaVendasDbContext _dbContext;

        public PedidosProdutosRepositorio(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PedidosProdutosModel> Adicionar(PedidosProdutosModel pedidoProduto)
        {
            await _dbContext.PedidosProdutos.AddAsync(pedidoProduto);
            await _dbContext.SaveChangesAsync();

            return pedidoProduto;
        }

        public async Task<bool> Apagar(int id)
        {
            PedidosProdutosModel pedidoProdutoPorId = await BuscarPorId(id);

            if (pedidoProdutoPorId == null)
            {
                throw new Exception("PedidoProduto não encontrado.");
            }

            _dbContext.PedidosProdutos.Remove(pedidoProdutoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PedidosProdutosModel> Atualizar(PedidosProdutosModel pedidoProduto, int id)
        {
            PedidosProdutosModel pedidoProdutoPorId = await BuscarPorId(id);

            if (pedidoProdutoPorId == null)
            {
                throw new Exception($"PedidoProduto do Id: {id} não encontrado.");
            }

            pedidoProdutoPorId.ProdutoId = pedidoProduto.ProdutoId;
            pedidoProdutoPorId.PedidoId = pedidoProduto.PedidoId;
            pedidoProdutoPorId.Quantidade = pedidoProduto.Quantidade;
            pedidoProdutoPorId.PrecoUnitario = pedidoProduto.PrecoUnitario;

            await _dbContext.SaveChangesAsync();

            return pedidoProdutoPorId;
        }

        public async Task<PedidosProdutosModel> BuscarPorId(int id)
        {
            return await _dbContext.PedidosProdutos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PedidosProdutosModel>> BuscarTodosPedidosProdutos()
        {
            return await _dbContext.PedidosProdutos.ToListAsync();
        }
    }
}
