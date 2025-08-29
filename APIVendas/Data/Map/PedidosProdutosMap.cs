using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APIVendas.Models;

public class PedidosProdutosMap : IEntityTypeConfiguration<PedidosProdutosModel>
{
    public void Configure(EntityTypeBuilder<PedidosProdutosModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProdutoId)
               .IsRequired();

        builder.Property(x => x.PedidoId)
               .IsRequired();

        builder.Property(x => x.Quantidade)
               .IsRequired();

        builder.Property(x => x.PrecoUnitario)
               .IsRequired()
               .HasColumnType("decimal(18,2)"); // Preço preciso


        builder.HasOne<ProdutosModel>()  // Cada item tem um produto
               .WithMany()
               .HasForeignKey(x => x.ProdutoId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<PedidosModel>()   // Cada item pertence a um pedido
               .WithMany()
               .HasForeignKey(x => x.PedidoId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}