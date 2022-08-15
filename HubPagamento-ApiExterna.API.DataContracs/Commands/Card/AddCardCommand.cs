using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HubPagamento.ApiExterna.API.Application
{
    public class AddCardCommand : IRequest<AddCardResponse>
    {
        [Required]
        public CustomerDTO Customer { get; set; }

        [Required]
        public IEnumerable<CardDTO> Cards { get; set; }
    }
}
