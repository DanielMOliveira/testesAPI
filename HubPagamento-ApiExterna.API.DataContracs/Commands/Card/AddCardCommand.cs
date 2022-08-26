using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.API.Application
{
    public class AddCardCommand : IRequest<AddCardResponse>
    {
        [Required]
        [JsonPropertyName("cliente")]
        public CustomerDTO Customer { get; set; }

        [Required]
        [JsonPropertyName("cartoes")]
        public IEnumerable<CardDTO> Cards { get; set; }
    }
}
