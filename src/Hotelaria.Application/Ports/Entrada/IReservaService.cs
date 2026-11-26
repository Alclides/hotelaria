using Hotelaria.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Ports.Entrada
{
    public interface IReservaService
    {
        Task<ReservaResponse> CriarAsync(CriarReservaRequest request, CancellationToken ct = default);
        Task<ReservaResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken ct = default);
        Task<ReservaResponse> FazerCheckInAsync(Guid id, CancellationToken ct = default);
        Task<ReservaResponse> FazerCheckOutAsync(Guid id, CancellationToken ct = default);
        Task<ReservaResponse> CancelarAsync(Guid id, CancellationToken ct = default);
        Task RemoverAsync(Guid id, CancellationToken ct = default);
    }
}
