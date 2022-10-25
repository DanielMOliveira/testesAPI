using FluentValidation;
using HubPagamento.ApiExterna.API.DataContracs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Validators
{
    public class CardDTOValidator : AbstractValidator<CardDTO>
    {
        public CardDTOValidator()
        {
            RuleFor(c => c.FlagBrand).NotEmpty();
            RuleFor(c => c.CardHolder).NotEmpty();
            RuleFor(c => c.CardNumber).NotEmpty().CreditCard();
            RuleFor(c => c.CardSecurityCode).NotEmpty();
            RuleFor(c => c.CardExpirationDate).NotEmpty();
            RuleFor(c => c.Fraud).NotEmpty();
        }
    }
}
