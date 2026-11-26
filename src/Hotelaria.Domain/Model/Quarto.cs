using Hotelaria.Domain.Common;
using Hotelaria.Domain.Enums;

namespace Hotelaria.Domain.Model
{
    public class  Quarto
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int Numero { get; private set; }
        public TipoQuarto Tipo { get; private set; }
        public int Capacidade { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public bool Ativo { get; private set; } = true;


        private Quarto() { }

        public Quarto(int numero, TipoQuarto tipo, int capacidade, decimal precoDiaria)
        {
            if (numero <= 0)
                throw new DomainException("O número do quarto deve ser maior que zero.");

            Numero = numero;
            Atualizar(tipo, capacidade, precoDiaria);
        }

        public void Atualizar(TipoQuarto tipo, int capacidade, decimal valorDiaria)
        {
            
            if (!Enum.IsDefined(tipo))
            {
                throw new DomainException("Tipo de quarto invalido!");

            }

            if (capacidade is < 1 or > 10)
            {
                throw new DomainException("Capacidade do quarto deve ser entre 1 e 10!");
            }

            if (valorDiaria <= 0)
            {
                throw new DomainException("Valor da diaria deve ser maior que 0!");
            }

            Tipo = tipo;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
    }
}
