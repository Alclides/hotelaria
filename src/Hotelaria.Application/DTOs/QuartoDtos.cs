using Hotelaria.Domain.Enums;
using Hotelaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.DTOs
{
    public record CriarQuartoRequest(int Numero, TipoQuarto Tipo, int Capacidade, decimal ValorDiaria);

    public record AtualizarQuartoRequest(TipoQuarto Tipo, int Capacidade, decimal ValorDiaria);

    public record QuartoResponse(Guid Id, int Numero, TipoQuarto Tipo, int Capacidade, decimal ValorDiaria, bool Ativo)
    {
        public static QuartoResponse De(Quarto q) =>
            new(q.Id, q.Numero, q.Tipo, q.Capacidade, q.ValorDiaria, q.Ativo);
    }
}
