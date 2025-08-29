using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using APIVendas.Enums;
using APIVendas.Models;

public class CategoriasMap : IEntityTypeConfiguration<CategoriasModel>
{
    public void Configure(EntityTypeBuilder<CategoriasModel> builder)
    {
        builder.HasKey(x => x.Id); // Chave primária

        builder.Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Status)
               .IsRequired()
               .HasConversion<string>() // Salva o enum como string no banco
               .HasMaxLength(50);
    }
}