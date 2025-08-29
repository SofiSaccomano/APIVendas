
using APIVendas.Data;
using APIVendas.Models;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly SistemaVendasDbContext _dbContext;

        public UsuariosRepositorio(SistemaVendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsuariosModel> Adicionar(UsuariosModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuariosModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UsuariosModel> Atualizar(UsuariosModel usuario, int id)
        {
            UsuariosModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário do Id: {id} não encontrado.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.DataNascimento = usuario.DataNascimento;

            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<UsuariosModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuariosModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
    }
}
