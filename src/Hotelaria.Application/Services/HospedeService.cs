using Hotelaria.Application.Common;
using Hotelaria.Application.DTOs;
using Hotelaria.Application.Ports.Entrada;
using Hotelaria.Application.Ports.Saida;
using Hotelaria.Domain.Common;
using Hotelaria.Domain.Model;

namespace Hotelaria.Application.Services
{
    public class HospedeService(
        IHospedeRepository hospedes,
        IReservaRepository reservas) : IHospedeService
    {
        public async Task<HospedeResponse> CriarAsync(CriarHospedeRequest request, CancellationToken ct = default)
        {
            // 1. O domínio valida os dados (nome, e-mail, CPF...)
            var hospede = new Hospede(request.Nome, request.Cpf, request.Email, request.Telefone);

            // 2. Regra que depende do banco: CPF não pode repetir
            if (await hospedes.ExisteCpfAsync(hospede.Cpf, ct))
                throw new DomainException("Já existe um hóspede cadastrado com este CPF.");

            // 3. Salva pela porta de saída
            await hospedes.AdicionarAsync(hospede, ct);
            return HospedeResponse.De(hospede);
        }

        public async Task<HospedeResponse> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        {
            var hospede = await ObterOuFalharAsync(id, ct);
            return HospedeResponse.De(hospede);
        }

        public async Task<IReadOnlyList<HospedeResponse>> ListarAsync(CancellationToken ct = default)
        {
            var lista = await hospedes.ListarAsync(ct);
            return lista.Select(HospedeResponse.De).ToList();
        }

        public async Task<HospedeResponse> AtualizarAsync(Guid id, AtualizarHospedeRequest request, CancellationToken ct = default)
        {
            var hospede = await ObterOuFalharAsync(id, ct);

            // Campo que não veio (null) mantém o valor atual
            hospede.Atualizar(
                request.Nome ?? hospede.Nome,
                request.Email ?? hospede.Email,
                request.Telefone ?? hospede.Telefone);

            await hospedes.AtualizarAsync(hospede, ct);
            return HospedeResponse.De(hospede);
        }

        public async Task RemoverAsync(Guid id, CancellationToken ct = default)
        {
            var hospede = await ObterOuFalharAsync(id, ct);

            var reservasDoHospede = await reservas.ListarPorHospedeAsync(id, ct);
            if (reservasDoHospede.Count > 0)
                throw new DomainException("Não é possível excluir um hóspede que possui reservas.");

            await hospedes.RemoverAsync(hospede, ct);
        }

        private async Task<Hospede> ObterOuFalharAsync(Guid id, CancellationToken ct) =>
            await hospedes.ObterPorIdAsync(id, ct)
            ?? throw new NaoEncontradoException("Hóspede", id);
    }
}
