using APIVendas.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APIVendas.Models;

namespace APIVendas.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuariosModel>
    {
        public void Configure(EntityTypeBuilder<UsuariosModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(180);
            builder.Property(x => x.DataNascimento).IsRequired();
        }
    }
}