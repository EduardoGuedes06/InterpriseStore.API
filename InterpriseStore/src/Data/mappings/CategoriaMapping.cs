using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Business.Models;

namespace Data.mappings
{

    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
        {
            public void Configure(EntityTypeBuilder<Categoria> builder)
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(200)");


            // 1 : N => Fornecedor : Produtos
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

                builder.ToTable("Categoria");
            }
        }
}
