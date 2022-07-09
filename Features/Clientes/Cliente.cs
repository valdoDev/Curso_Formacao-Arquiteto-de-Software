using Features.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Clientes
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Cliente()
        {

        }

        public Cliente(Guid id, string nome, string sobrenome, DateTime dataNascimento, string email, bool ativo, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
        }   

        public string NomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        public bool EhEspecial()
        {
            return DataCadastro < DateTime.Now.AddYears(-3) && Ativo;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ClienteValidacao().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class ClienteValidacao : AbstractValidator<Cliente>
    {
        public ClienteValidacao()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Preencha o nome! Por favor")
                .Length(2, 150).WithMessage("O nome deve conter entre 2 e 150 caracteres!");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Preencha o sobrenome! Por favor")
                .Length(2, 150).WithMessage("O sobrenome deve conter entre 2 e 150 caracteres!");


            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .Must(HaveMinimunAge)
                .WithMessage("O cliente deve ter 18 anos ou mais");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimunAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
