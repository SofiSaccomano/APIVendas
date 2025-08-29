using APIVendas.Models;

namespace APIVendas.Repositorios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<List<UsuariosModel>> BuscarTodosUsuarios();
        Task<UsuariosModel> BuscarPorId(int id);
        Task<UsuariosModel> Adicionar(UsuariosModel usuarios);
        Task<UsuariosModel> Atualizar(UsuariosModel usuarios, int id);
        Task<bool> Apagar(int id);
    }
}