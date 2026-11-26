using Hotelaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Ports.Saida
{
    public interface IQuartoRepository
    {
        Task<Quarto?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Quarto>> ListarAsync(CancellationToken ct = default);
        Task<bool> ExisteNumeroAsync(int numero, CancellationToken ct = default);

        Task AdicionarAsync(Quarto quarto, CancellationToken ct = default);
        Task AtualizarAsync(Quarto quarto, CancellationToken ct = default);
        Task RemoverAsync(Quarto quarto, CancellationToken ct = default);
    }
}
