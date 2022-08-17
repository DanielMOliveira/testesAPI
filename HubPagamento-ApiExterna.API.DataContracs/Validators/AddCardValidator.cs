using FluentValidation;
using HubPagamento.ApiExterna.API.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Validators
{
    public class AddCardValidator : AbstractValidator<AddCardCommand>
    {
        public AddCardValidator()
        {
            RuleFor(a => a.Cards).NotEmpty();
            RuleFor(a => a.Customer).NotEmpty();
        }
    }
}
