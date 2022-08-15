using HubPagamento.ApiExterna.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Response
{
    public class AddCardResponse
    {
        [JsonPropertyName("carteira")]
        public WalletResponse Wallet { get; set; }
    }
}
