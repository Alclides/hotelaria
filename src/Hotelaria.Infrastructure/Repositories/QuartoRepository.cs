using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Model;
using Hotelaria.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelaria.Infrastructure.Repositories
{
    public class QuartoRepository(HotelariaDbContext db) : IQuartoRepository
    {
        public Task<Quarto?> ObterPorIdAsync(Guid id, CancellationToken ct = default) =>
            db.Quartos.FirstOrDefaultAsync(q => q.Id == id, ct);

        public async Task<IReadOnlyList<Quarto>> ListarAsync(CancellationToken ct = default) =>
            await db.Quartos.AsNoTracking().OrderBy(q => q.Numero).ToListAsync(ct);

        public Task<bool> ExisteNumeroAsync(int numero, CancellationToken ct = default) =>
            db.Quartos.AnyAsync(q => q.Numero == numero, ct);

        public async Task AdicionarAsync(Quarto quarto, CancellationToken ct = default)
        {
            db.Quartos.Add(quarto);
            await db.SaveChangesAsync(ct);
        }

        public async Task AtualizarAsync(Quarto quarto, CancellationToken ct = default)
        {
            db.Quartos.Update(quarto);
            await db.SaveChangesAsync(ct);
        }

        public async Task RemoverAsync(Quarto quarto, CancellationToken ct = default)
        {
            db.Quartos.Remove(quarto);
            await db.SaveChangesAsync(ct);
        }
    }
}
