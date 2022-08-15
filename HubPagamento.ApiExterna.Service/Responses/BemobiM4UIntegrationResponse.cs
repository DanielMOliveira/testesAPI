using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class BemobiM4UIntegrationResponse
    {
        [JsonPropertyName("card")]
        public CardM4UIntegrationResponse Card { get; set; }

        [JsonPropertyName("result")]
        public M4UIntegrationResult Result { get; set; }
    }
}
