using Hotelaria.Application.DTOs;

namespace Hotelaria.Application.Ports.Entrada
{
    public interface IHospedeService
    {
        Task<HospedeResponse> CriarAsync(CriarHospedeRequest request, CancellationToken ct = default);
        Task<HospedeResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<HospedeResponse>> ListarAsync(CancellationToken ct = default);
        Task<HospedeResponse> AtualizarAsync(Guid id, AtualizarHospedeRequest request, CancellationToken ct = default);
        Task RemoverAsync(Guid id, CancellationToken ct = default);
    }
}
