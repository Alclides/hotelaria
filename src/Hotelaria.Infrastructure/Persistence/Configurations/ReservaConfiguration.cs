using Hotelaria.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelaria.Infrastructure.Persistence.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder.Property(r => r.DataEntrada).IsRequired();
            builder.Property(r => r.DataSaida).IsRequired();
            builder.Property(r => r.QuantidadeHospedes).IsRequired();
            builder.Property(r => r.ValorTotal).HasPrecision(10, 2);
            builder.Property(r => r.Status).HasConversion<string>().HasMaxLength(20);

            builder.Ignore(r => r.Noites);

            builder.HasOne<Hospede>()
                .WithMany()
                .HasForeignKey(r => r.HospedeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Quarto>()
                .WithMany()
                .HasForeignKey(r => r.QuartoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
