using Hotelaria.Domain.Common;
using Hotelaria.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Domain.Model
{
    public class Reserva
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid HospedeId { get; private set; }
        public Guid QuartoId { get; private set; }
        public DateOnly DataEntrada { get; private set; }
        public DateOnly DataSaida { get; private set; }
        public int QuantidadeHospedes { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusDaReserva Status { get; private set; }

    public int Noites => DataSaida.DayNumber - DataEntrada.DayNumber;

        private Reserva() { }

        public Reserva(Hospede hospede, Quarto quarto, DateOnly entrada, DateOnly saida, int quantidadeHospedes)
        {
            ArgumentNullException.ThrowIfNull(hospede);
            ArgumentNullException.ThrowIfNull(quarto);

            if (!quarto.Ativo)
                throw new DomainException("Este quarto está desativado.");

            if (entrada < DateOnly.FromDateTime(DateTime.Today))
                throw new DomainException("A data de entrada não pode estar no passado.");

            if (saida <= entrada)
                throw new DomainException("A data de saída deve ser depois da data de entrada.");

            if (quantidadeHospedes < 1 || quantidadeHospedes > quarto.Capacidade)
                throw new DomainException($"Este quarto aceita de 1 a {quarto.Capacidade} hóspedes.");

            HospedeId = hospede.Id;
            QuartoId = quarto.Id;
            DataEntrada = entrada;
            DataSaida = saida;
            QuantidadeHospedes = quantidadeHospedes;
            ValorTotal = Noites * quarto.ValorDiaria;
            Status = StatusDaReserva.Confirmada;
        }

        // Verifica se esta reserva ocupa o quarto no período informado
        public bool ConflitaCom(DateOnly entrada, DateOnly saida)
        {
            var ocupaQuarto = Status is StatusDaReserva.Confirmada or StatusDaReserva.EmAndamento;
            return ocupaQuarto && entrada < DataSaida && saida > DataEntrada;
        }

        public void FazerCheckIn()
        {
            if (Status != StatusDaReserva.Confirmada)
                throw new DomainException("Só é possível fazer check-in em reservas confirmadas.");

            Status = StatusDaReserva.EmAndamento;
        }

        public void FazerCheckOut()
        {
            if (Status != StatusDaReserva.EmAndamento)
                throw new DomainException("Só é possível fazer check-out após o check-in.");

            Status = StatusDaReserva.Finalizada;
        }

        public void Cancelar()
        {
            if (Status != StatusDaReserva.Confirmada)
                throw new DomainException("Só é possível cancelar reservas confirmadas.");

            Status = StatusDaReserva.Cancelada;
        }
    }

}
