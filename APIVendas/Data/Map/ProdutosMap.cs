using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APIVendas.Models;


public class ProdutosMap : IEntityTypeConfiguration<ProdutosModel>
{
    public void Configure(EntityTypeBuilder<ProdutosModel> builder)
    {
        builder.HasKey(x => x.Id); // Chave primária

        builder.Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Descricao)
               .HasMaxLength(500); // Opcional, mas bom definir um limite

        builder.Property(x => x.PrecoUnitario)
               .IsRequired()
               .HasColumnType("decimal(18,2)"); // Define decimal com precisão para valores monetários
    }
}