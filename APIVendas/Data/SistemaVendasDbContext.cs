
using APIVendas.Data.Map;
using APIVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace APIVendas.Data
{
    public class SistemaVendasDbContext : DbContext
    {
        public SistemaVendasDbContext(DbContextOptions<SistemaVendasDbContext> options)
            : base(options)
        {
        }

        // DbSets das entidades
        public DbSet<UsuariosModel> Usuarios { get; set; }
        public DbSet<ProdutosModel> Produtos { get; set; }
        public DbSet<PedidosModel> Pedidos { get; set; }
        public DbSet<PedidosProdutosModel> PedidosProdutos { get; set; }
        public DbSet<CategoriasModel> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProdutosMap());
            modelBuilder.ApplyConfiguration(new PedidosMap());
            modelBuilder.ApplyConfiguration(new PedidosProdutosMap());
            modelBuilder.ApplyConfiguration(new CategoriasMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

