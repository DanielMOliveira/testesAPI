using HubPagamento.ApiExterna.Service.Responses;
using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.Service.Response
{
    public class AddCardResponse : BaseResponse
    {
        public AddCardResponse()
        {

        }

        public AddCardResponse(WalletResponse? wallet)
        {
            Wallet = wallet;
        }

        [JsonPropertyName("carteira")]
        public WalletResponse? Wallet { get; set; }
    }
}
