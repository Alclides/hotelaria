using System;
using System.Collections.Generic;
using System.Text;

using Hotelaria.Domain.Model;

namespace Hotelaria.Application.DTOs
{
    public record CriarHospedeRequest(string Nome, string Cpf, string Email, string Telefone);

    public record AtualizarHospedeRequest(string Nome, string Email, string Telefone);

    public record HospedeResponse(Guid Id, string Nome, string Cpf, string Email, string Telefone)
    {
        public static HospedeResponse De(Hospede h) =>
            new(h.Id, h.Nome, h.Cpf, h.Email, h.Telefone);
    }
}
