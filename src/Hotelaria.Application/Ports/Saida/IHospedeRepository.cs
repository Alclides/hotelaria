using Hotelaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Ports.Saida
{
    public interface IHospedeRepositorycs
    {
        Task<Hospede?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Hospede>> ListarAsync(CancellationToken ct = default);
        Task<bool> ExisteCpfAsync(string cpf, CancellationToken ct = default);


        Task AdicionarAsync(Hospede hospede, CancellationToken ct = default);
        Task AtualizarAsync(Hospede hospede, CancellationToken ct = default);
        Task RemoverAsync(Hospede hospede, CancellationToken ct = default);
    }
}
