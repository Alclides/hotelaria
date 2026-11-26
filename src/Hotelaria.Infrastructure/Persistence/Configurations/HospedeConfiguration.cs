using Hotelaria.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelaria.Infrastructure.Persistence.Configurations
{
    public class HospedeConfiguration : IEntityTypeConfiguration<Hospede>
    {
        public void Configure(EntityTypeBuilder<Hospede> builder)
        {
            builder.ToTable("Hospedes");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedNever(); 

            builder.Property(h => h.Nome).IsRequired().HasMaxLength(150);
            builder.Property(h => h.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(h => h.Email).IsRequired().HasMaxLength(150);
            builder.Property(h => h.Telefone).IsRequired().HasMaxLength(20);
            builder.HasIndex(h => h.Cpf).IsUnique();
        }
    }
}
