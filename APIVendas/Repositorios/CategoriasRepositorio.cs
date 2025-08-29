using APIVendas.Data;
using APIVendas.Models;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Repositorios
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private readonly SistemaVendasDbContext _dbContext;

        public CategoriasRepositorio(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoriasModel> Adicionar(CategoriasModel categoria)
        {
            await _dbContext.Categorias.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();

            return categoria;
        }

        public async Task<bool> Apagar(int id)
        {
            CategoriasModel categoriaPorId = await BuscarPorId(id);

            if (categoriaPorId == null)
            {
                throw new Exception("Categoria não encontrada.");
            }

            _dbContext.Categorias.Remove(categoriaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CategoriasModel> Atualizar(CategoriasModel categoria, int id)
        {
            CategoriasModel categoriaPorId = await BuscarPorId(id);

            if (categoriaPorId == null)
            {
                throw new Exception($"Categoria do Id: {id} não encontrada.");
            }

            categoriaPorId.Nome = categoria.Nome;
            categoriaPorId.Status = categoria.Status;

            await _dbContext.SaveChangesAsync();

            return categoriaPorId;
        }

        public async Task<CategoriasModel> BuscarPorId(int id)
        {
            return await _dbContext.Categorias
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CategoriasModel>> BuscarTodasCategorias()
        {
            return await _dbContext.Categorias.ToListAsync();
        }
    }
}
