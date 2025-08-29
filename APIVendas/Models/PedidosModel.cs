using APIVendas.Enums;

namespace APIVendas.Models
{
    public class PedidosModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string EnderecoEntrega { get; set; }
        public StatusPedido Status { get; set; }
        public string MetodoPagamento { get; set; }

        public UsuariosModel Usuario { get; set; }
        public ICollection<PedidosProdutosModel> PedidosProdutos { get; set; }
    }
}
