using FluentValidation;
using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Validators
{
    public class TokenizeCardValidator : AbstractValidator<TokenizeCardCommand>
    {
        public TokenizeCardValidator()
        {
            RuleFor(t => t.Partner.ToUpper()).NotEmpty().Equal("CLARO").WithMessage("O parceiro deve ser preenchido como CLARO");
            RuleFor(t => t.Month).NotEmpty();
            RuleFor(t => t.Year).NotEmpty();
            RuleFor(t => t.Pan).NotEmpty().CreditCard();
        }
    }
}
