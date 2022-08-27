using FluentValidation;
using HubPagamento.ApiExterna.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Validators
{
    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidator()
        {
            RuleFor(c => c.CustomerIdentity).NotEmpty();
            RuleFor(c => c.Document).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
