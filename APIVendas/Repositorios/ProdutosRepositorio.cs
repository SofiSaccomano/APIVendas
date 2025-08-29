
using APIVendas.Data;
using APIVendas.Models;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Repositorios
{
    public class ProdutosRepositorio : IProdutosRepositorio
    {
        private readonly SistemaVendasDbContext _dbContext;

        public ProdutosRepositorio(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProdutosModel> Adicionar(ProdutosModel produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> Apagar(int id)
        {
            ProdutosModel produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            _dbContext.Produtos.Remove(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ProdutosModel> Atualizar(ProdutosModel produto, int id)
        {
            ProdutosModel produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto do Id: {id} não encontrado.");
            }

            produtoPorId.Nome = produto.Nome;
            produtoPorId.Descricao = produto.Descricao;
            produtoPorId.PrecoUnitario = produto.PrecoUnitario;

            await _dbContext.SaveChangesAsync();

            return produtoPorId;
        }

        public async Task<ProdutosModel> BuscarPorId(int id)
        {
            return await _dbContext.Produtos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProdutosModel>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }
    }
}
