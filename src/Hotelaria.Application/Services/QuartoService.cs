using Hotelaria.Application.Common;
using Hotelaria.Application.DTOs;
using Hotelaria.Application.Ports.Entrada;
using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Common;
using Hotelaria.Domain.Model;

namespace Hotelaria.Application.Services
{
    public class QuartoService(
        IQuartoRepository quartos,
        IReservaRepository reservas) : IQuartoService
    {
        public async Task<QuartoResponse> CriarAsync(CriarQuartoRequest request, CancellationToken ct = default)
        {
            var quarto = new Quarto(request.Numero, request.Tipo, request.Capacidade, request.ValorDiaria);

            if (await quartos.ExisteNumeroAsync(quarto.Numero, ct))
                throw new DomainException($"Já existe um quarto com o número {quarto.Numero}.");

            await quartos.AdicionarAsync(quarto, ct);
            return QuartoResponse.De(quarto);
        }

        public async Task<QuartoResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            var quarto = await ObterOuFalharAsync(id, ct);
            return QuartoResponse.De(quarto);
        }

        public async Task<IReadOnlyList<QuartoResponse>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await quartos.ListarAsync(ct);
            return lista.Select(QuartoResponse.De).ToList();
        }

        public async Task<QuartoResponse> AtualizarAsync(Guid id, AtualizarQuartoRequest request, CancellationToken ct = default)
        {
            var quarto = await ObterOuFalharAsync(id, ct);
            quarto.Atualizar(request.Tipo, request.Capacidade, request.ValorDiaria);

            await quartos.AtualizarAsync(quarto, ct);
            return QuartoResponse.De(quarto);
        }

        public async Task<QuartoResponse> AtivarAsync(Guid id, CancellationToken ct = default)
        {
            var quarto = await ObterOuFalharAsync(id, ct);
            quarto.Ativar();

            await quartos.AtualizarAsync(quarto, ct);
            return QuartoResponse.De(quarto);
        }

        public async Task<QuartoResponse> DesativarAsync(Guid id, CancellationToken ct = default)
        {
            var quarto = await ObterOuFalharAsync(id, ct);
            quarto.Desativar();

            await quartos.AtualizarAsync(quarto, ct);
            return QuartoResponse.De(quarto);
        }

        public async Task RemoverAsync(Guid id, CancellationToken ct = default)
        {
            var quarto = await ObterOuFalharAsync(id, ct);

            var reservasDoQuarto = await reservas.ListarPorQuartoAsync(id, ct);
            if (reservasDoQuarto.Count > 0)
                throw new DomainException("Não é possível excluir um quarto que possui reservas. Desative-o.");

            await quartos.RemoverAsync(quarto, ct);
        }

        private async Task<Quarto> ObterOuFalharAsync(Guid id, CancellationToken ct) =>
            await quartos.ObterPorIdAsync(id, ct)
            ?? throw new NaoEncontradoException("Quarto", id);
    }
}
