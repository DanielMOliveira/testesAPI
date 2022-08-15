using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class WalletCardResponse
    {
        [JsonPropertyName("card")]
        public CardResponse Card { get; set; }

        [JsonPropertyName("fatura")]
        public BillResponse Bill { get; set; }

        [JsonPropertyName("integracaoBemobiM4U")]
        public IReadOnlyCollection<BemobiM4UIntegrationResponse> Integration { get; set; }
    }
}
