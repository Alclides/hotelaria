using Hotelaria.Application.Common;
using Hotelaria.Application.DTOs;
using Hotelaria.Application.Ports.Entrada;
using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Common;
using Hotelaria.Domain.Enums;
using Hotelaria.Domain.Model;

namespace Hotelaria.Application.Services
{
    public class ReservaService(
        IReservaRepository reservas,
        IHospedeRepository hospedes,
        IQuartoRepository quartos) : IReservaService
    {
        public async Task<ReservaResponse> CriarAsync(CriarReservaRequest request, CancellationToken ct = default)
        {
            // 1. Busca hóspede e quarto pelas portas de saída
            var hospede = await hospedes.ObterPorIdAsync(request.HospedeId, ct)
                ?? throw new NaoEncontradoException("Hóspede", request.HospedeId);

            var quarto = await quartos.ObterPorIdAsync(request.QuartoId, ct)
                ?? throw new NaoEncontradoException("Quarto", request.QuartoId);

            // 2. Regra que depende do banco: o quarto não pode estar ocupado no período
            var reservasDoQuarto = await reservas.ListarPorQuartoAsync(quarto.Id, ct);
            if (reservasDoQuarto.Any(r => r.ConflitaCom(request.DataEntrada, request.DataSaida)))
                throw new DomainException("O quarto já está reservado neste período.");

            // 3. O domínio valida o resto (datas, capacidade, quarto ativo) e calcula o valor
            var reserva = new Reserva(hospede, quarto, request.DataEntrada, request.DataSaida, request.QuantidadeHospedes);

            await reservas.AdicionarAsync(reserva, ct);
            return ReservaResponse.De(reserva);
        }

        public async Task<ReservaResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            var reserva = await ObterOuFalharAsync(id, ct);
            return ReservaResponse.De(reserva);
        }

        public async Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await reservas.ListarAsync(ct);
            return lista.Select(ReservaResponse.De).ToList();
        }

        public async Task<ReservaResponse> FazerCheckInAsync(Guid id, CancellationToken ct = default)
        {
            var reserva = await ObterOuFalharAsync(id, ct);
            reserva.FazerCheckIn();

            await reservas.AtualizarAsync(reserva, ct);
            return ReservaResponse.De(reserva);
        }

        public async Task<ReservaResponse> FazerCheckOutAsync(Guid id, CancellationToken ct = default)
        {
            var reserva = await ObterOuFalharAsync(id, ct);
            reserva.FazerCheckOut();

            await reservas.AtualizarAsync(reserva, ct);
            return ReservaResponse.De(reserva);
        }

        public async Task<ReservaResponse> CancelarAsync(Guid id, CancellationToken ct = default)
        {
            var reserva = await ObterOuFalharAsync(id, ct);
            reserva.Cancelar();

            await reservas.AtualizarAsync(reserva, ct);
            return ReservaResponse.De(reserva);
        }

        public async Task RemoverAsync(Guid id, CancellationToken ct = default)
        {
            var reserva = await ObterOuFalharAsync(id, ct);

            if (reserva.Status == StatusDaReserva.EmAndamento)
                throw new DomainException("Não é possível excluir uma reserva com hóspede no quarto. Faça o check-out.");

            await reservas.RemoverAsync(reserva, ct);
        }

        private async Task<Reserva> ObterOuFalharAsync(Guid id, CancellationToken ct) =>
            await reservas.ObterPorIdAsync(id, ct)
            ?? throw new NaoEncontradoException("Reserva", id);
    }
}
