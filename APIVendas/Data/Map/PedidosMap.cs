using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using APIVendas.Enums;
using APIVendas.Models;

public class PedidosMap : IEntityTypeConfiguration<PedidosModel>
{
    public void Configure(EntityTypeBuilder<PedidosModel> builder)
    {
        builder.HasKey(x => x.Id); // Chave primária

        builder.Property(x => x.UsuarioId)
               .IsRequired();

        builder.Property(x => x.EnderecoEntrega)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.Status)
               .IsRequired()
               .HasConversion<string>() // Armazena o enum como string no banco
               .HasMaxLength(50);

        builder.Property(x => x.MetodoPagamento)
               .IsRequired()
               .HasMaxLength(100);

        // Relacionamentos
        builder.HasOne(x => x.Usuario) // Pedido tem um usuário
               .WithMany()
               .HasForeignKey(x => x.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PedidosProdutos) // Pedido tem muitos produtos
               .WithOne()
               .HasForeignKey(x => x.PedidoId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}