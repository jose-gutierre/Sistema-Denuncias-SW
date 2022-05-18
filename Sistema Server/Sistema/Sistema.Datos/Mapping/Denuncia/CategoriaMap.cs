using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Denuncia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Denuncia
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categoria")
              .HasKey(c => c.iddenuncia);
            builder.Property(c => c.nombre)
                .HasMaxLength(50);
            builder.Property(c => c.apellido)
                .HasMaxLength(256);
            builder.Property(c => c.cargo)
              .HasMaxLength(256);
            builder.Property(c => c.entidad)
             .HasMaxLength(256);
            builder.Property(c => c.fecha)
             .HasMaxLength(256);
            builder.Property(c => c.hora)
             .HasMaxLength(256);
            builder.Property(c => c.lugar)
             .HasMaxLength(256);
            builder.Property(c => c.descripcion)
             .HasMaxLength(256);

        }
    }
}
