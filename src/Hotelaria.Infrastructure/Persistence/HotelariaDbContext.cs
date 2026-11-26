using Hotelaria.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelaria.Infrastructure.Persistence
{
    public class HotelariaDbContext(DbContextOptions<HotelariaDbContext> options) : DbContext(options)
    {
        public DbSet<Hospede> Hospedes => Set<Hospede>();
        public DbSet<Quarto> Quartos => Set<Quarto>();
        public DbSet<Reserva> Reservas => Set<Reserva>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelariaDbContext).Assembly);
        }
    }
}
