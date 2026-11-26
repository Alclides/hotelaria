using Hotelaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Ports.Saida
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Reserva>> ListarAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Reserva>> ListarPorQuartoAsync(Guid quartoId, CancellationToken ct = default);
        Task<IReadOnlyList<Reserva>> ListarPorHospedeAsync(Guid hospedeId, CancellationToken ct = default);

        Task AdicionarAsync(Reserva reserva, CancellationToken ct = default);
        Task AtualizarAsync(Reserva reserva, CancellationToken ct = default);
        Task RemoverAsync(Reserva reserva, CancellationToken ct = default);
    }
}
