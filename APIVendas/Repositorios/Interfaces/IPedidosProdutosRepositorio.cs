using APIVendas.Models;

namespace APIVendas.Repositorios.Interfaces
{
    public interface IPedidosProdutosRepositorio
    {
        Task<List<PedidosProdutosModel>> BuscarTodosPedidosProdutos();
        Task<PedidosProdutosModel> BuscarPorId(int id);
        Task<PedidosProdutosModel> Adicionar(PedidosProdutosModel pedidosProdutos);
        Task<PedidosProdutosModel> Atualizar(PedidosProdutosModel pedidosProdutos, int id);
        Task<bool> Apagar(int id);
    }
}
