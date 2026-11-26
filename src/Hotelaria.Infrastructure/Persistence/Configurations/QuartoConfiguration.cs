using Hotelaria.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelaria.Infrastructure.Persistence.Configurations
{
    public class QuartoConfiguration : IEntityTypeConfiguration<Quarto>
    {
        public void Configure(EntityTypeBuilder<Quarto> builder)
        {
            builder.ToTable("Quartos");

            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedNever();

            builder.Property(q => q.Numero).IsRequired();
            builder.Property(q => q.Capacidade).IsRequired();
            builder.Property(q => q.Ativo).IsRequired();
            builder.Property(q => q.Tipo).HasConversion<string>().HasMaxLength(20);

            builder.Property(q => q.ValorDiaria).HasPrecision(10, 2);

            builder.HasIndex(q => q.Numero).IsUnique();
        }
    }
}
