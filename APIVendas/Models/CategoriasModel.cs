using APIVendas.Enums;

namespace APIVendas.Models
{
    public class CategoriasModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public StatusCategoria Status { get; set; }
    }
}
