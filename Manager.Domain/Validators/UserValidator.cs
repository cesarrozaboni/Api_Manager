using FluentValidation;
using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A Entidade não pode ser vazia")
                
                .NotNull()
                .WithMessage("A Entidade não pode ser nula");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo")
                
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio")
                
                .MinimumLength(3)
                .WithMessage("O nome deve ter no minimo 3 caracteres")
                
                .MaximumLength(80)
                .WithMessage("O nome deve ter no maximo 80 caracteres");

            RuleFor(x => x.Password)
              .NotNull()
              .WithMessage("A senha não pode ser nulo")

              .NotEmpty()
              .WithMessage("A senha não pode ser vazio")

              .MinimumLength(6)
              .WithMessage("A senha deve ter no minimo 6 caracteres")

              .MaximumLength(30)
              .WithMessage("A senha deve ter no maximo 30 caracteres");

            RuleFor(x => x.Email)
                  .NotNull()
                .WithMessage("O email não pode ser nulo")

                .NotEmpty()
                .WithMessage("O email não pode ser vazio")

                .MinimumLength(10)
                .WithMessage("O email deve ter no minimo 3 caracteres")

                .MaximumLength(180)
                .WithMessage("O email deve ter no maximo 80 caracteres")
                
                .Matches(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")
                .WithMessage("O Email informado não é valido.");
        }
    }
}
