using HubPagamento.ApiExterna.API.DataContracs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    [Serializable]
    public class AddCardDTO
    {
        public AddCardDTO(CustomerDTO customer, IEnumerable<CardDTO> cards)
        {
            Customer = customer;
            Cards = cards;
        }

        [JsonPropertyName("cliente")]
        public CustomerDTO Customer { get; set; }

        [JsonPropertyName("cartoes")]
        public IEnumerable<CardDTO> Cards { get; set; }
    }
}
