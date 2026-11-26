using Hotelaria.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Ports.Entrada
{
    public interface IQuartoService
    {
        Task<QuartoResponse> CriarAsync(CriarQuartoRequest request, CancellationToken ct = default);
        Task<QuartoResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<QuartoResponse>> ListarAsync(CancellationToken ct = default);
        Task<QuartoResponse> AtualizarAsync(Guid id, AtualizarQuartoRequest request, CancellationToken ct = default);
        Task<QuartoResponse> AtivarAsync(Guid id, CancellationToken ct = default);
        Task<QuartoResponse> DesativarAsync(Guid id, CancellationToken ct = default);
        Task RemoverAsync(Guid id, CancellationToken ct = default);
    }
}
