using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Model;
using Hotelaria.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelaria.Infrastructure.Repositories
{
    public class HospedeRepository(HotelariaDbContext db) : IHospedeRepository
    {
        public Task<Hospede?> ObterPorIdAsync(Guid id, CancellationToken ct = default) =>
            db.Hospedes.FirstOrDefaultAsync(h => h.Id == id, ct);

        public async Task<IReadOnlyList<Hospede>> ListarAsync(CancellationToken ct = default) =>
            await db.Hospedes.AsNoTracking().OrderBy(h => h.Nome).ToListAsync(ct);

        public Task<bool> ExisteCpfAsync(string cpf, CancellationToken ct = default) =>
            db.Hospedes.AnyAsync(h => h.Cpf == cpf, ct);

        public async Task AdicionarAsync(Hospede hospede, CancellationToken ct = default)
        {
            db.Hospedes.Add(hospede);
            await db.SaveChangesAsync(ct);
        }

        public async Task AtualizarAsync(Hospede hospede, CancellationToken ct = default)
        {
            db.Hospedes.Update(hospede);
            await db.SaveChangesAsync(ct);
        }

        public async Task RemoverAsync(Hospede hospede, CancellationToken ct = default)
        {
            db.Hospedes.Remove(hospede);
            await db.SaveChangesAsync(ct);
        }
    }
}
