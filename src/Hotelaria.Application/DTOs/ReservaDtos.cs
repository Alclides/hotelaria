using Hotelaria.Domain.Enums;
using Hotelaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.DTOs
{
    public record CriarReservaRequest(
        Guid HospedeId,
        Guid QuartoId,
        DateOnly DataEntrada,
        DateOnly DataSaida,
        int QuantidadeHospedes);

    public record ReservaResponse(
        Guid Id,
        Guid HospedeId,
        Guid QuartoId,
        DateOnly DataEntrada,
        DateOnly DataSaida,
        int Noites,
        int QuantidadeHospedes,
        decimal ValorTotal,
        StatusDaReserva Status)
    {
        public static ReservaResponse De(Reserva r) =>
            new(r.Id, r.HospedeId, r.QuartoId, r.DataEntrada, r.DataSaida,
                r.Noites, r.QuantidadeHospedes, r.ValorTotal, r.Status);
    }
}
