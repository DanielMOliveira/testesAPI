using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class WalletResponse
    {
        [JsonPropertyName("carteiraId")]
        public int ID { get; set; }

        [JsonPropertyName("cliente")]
        public CustomerResponse Customer { get; set; }

        [JsonPropertyName("cartoes")]
        public IReadOnlyCollection<WalletCardResponse> Cards { get; set; }

    }
}
