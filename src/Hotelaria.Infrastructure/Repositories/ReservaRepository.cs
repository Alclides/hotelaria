using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Model;
using Hotelaria.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelaria.Infrastructure.Repositories
{
    public class ReservaRepository(HotelariaDbContext db) : IReservaRepository
    {
        public Task<Reserva?> ObterPorIdAsync(Guid id, CancellationToken ct = default) =>
            db.Reservas.FirstOrDefaultAsync(r => r.Id == id, ct);

        public async Task<IReadOnlyList<Reserva>> ListarAsync(CancellationToken ct = default) =>
            await db.Reservas.AsNoTracking().OrderBy(r => r.DataEntrada).ToListAsync(ct);

        public async Task<IReadOnlyList<Reserva>> ListarPorQuartoAsync(Guid quartoId, CancellationToken ct = default) =>
            await db.Reservas.AsNoTracking().Where(r => r.QuartoId == quartoId).ToListAsync(ct);

        public async Task<IReadOnlyList<Reserva>> ListarPorHospedeAsync(Guid hospedeId, CancellationToken ct = default) =>
            await db.Reservas.AsNoTracking().Where(r => r.HospedeId == hospedeId).ToListAsync(ct);

        public async Task AdicionarAsync(Reserva reserva, CancellationToken ct = default)
        {
            db.Reservas.Add(reserva);
            await db.SaveChangesAsync(ct);
        }

        public async Task AtualizarAsync(Reserva reserva, CancellationToken ct = default)
        {
            db.Reservas.Update(reserva);
            await db.SaveChangesAsync(ct);
        }

        public async Task RemoverAsync(Reserva reserva, CancellationToken ct = default)
        {
            db.Reservas.Remove(reserva);
            await db.SaveChangesAsync(ct);
        }
    }
}
