using APIVendas.Models;

namespace APIVendas.Repositorios.Interfaces
{
    public interface IPedidosRepositorio
    {
        Task<List<PedidosModel>> BuscarTodosPedidos();
        Task<PedidosModel> BuscarPorId(int id);
        Task<PedidosModel> Adicionar(PedidosModel pedidos);
        Task<PedidosModel> Atualizar(PedidosModel pedidos, int id);
        Task<bool> Apagar(int id);
    }
}
