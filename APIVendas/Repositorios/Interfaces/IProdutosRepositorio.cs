using APIVendas.Models;

namespace APIVendas.Repositorios.Interfaces
{
    public interface IProdutosRepositorio
    {
        Task<List<ProdutosModel>> BuscarTodosProdutos();
        Task<ProdutosModel> BuscarPorId(int id);
        Task<ProdutosModel> Adicionar(ProdutosModel produtos);
        Task<ProdutosModel> Atualizar(ProdutosModel produtos, int id);
        Task<bool> Apagar(int id);
    }
}
