using Hotelaria.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Hotelaria.Domain.Model
{
    public class Hospede
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Telefone { get; private set; } = string.Empty;


        private Hospede () { }

        public Hospede(string nome, string cpf, string email, string telefone)
        {
            Cpf = ValidarCpf(cpf);
            Atualizar(nome, email, telefone);
        }


        public void Atualizar (string nome, string email, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length < 3)
            {
                throw new DomainException("Nome do hóspede deve ter no mínimo 3 caracteres.");
            }

            if (!MailAddress.TryCreate(email, out _))
            {
                throw new DomainException("Endereço de email inválido.");
            }

            if(string.IsNullOrWhiteSpace(telefone))
            {
                throw new DomainException("Telefone do hóspede não pode ser vazio.");
            }

            Nome = nome.Trim();
            Email = email.Trim().ToLowerInvariant();
            Telefone = telefone.Trim();
           
        }

        private static string ValidarCpf(string cpf)
        {
            var digitos = new string((cpf ?? string.Empty).Where(char.IsDigit).ToArray());

            if(digitos.Length != 11)
            {
                throw new DomainException("CPF inválido. Deve conter 11 dígitos.");
            }
            return digitos;
        }
    }
}
