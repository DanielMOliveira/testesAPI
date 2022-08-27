using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class CardResponse
    {
        /// <summary>
        /// Token do cartão
        /// </summary>
        /// <example>3c3d59a7-8469-4b5c-9171-061064e625b2</example>
        [JsonPropertyName("token")]
        public string Token { get; set; }


        /// <summary>
        /// Bandeira do Cartão
        /// </summary>
        /// <example>
        /// Mastercard
        /// </example>
        [JsonPropertyName("bandeira")]
        public string FlagBrand { get; set; }

        /// <summary>
        /// Nome como está escrito no cartão
        /// </summary>
        /// <example>
        /// John Doe
        /// </example>
        [JsonPropertyName("cardHolder")]
        public string CardHolder { get; set; }

        /// <summary>
        /// Numero como está escrito no cartão. Sem simbolos ou espaços.
        /// </summary>
        /// <example>
        /// 4548812049400004
        /// </example>
        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// Codigo de segurança do cartão.
        /// </summary>
        /// <example>
        /// 123
        /// </example>
        [JsonPropertyName("cardSecurityCode")]
        public string CardSecurityCode { get; set; }

        /// <summary>
        /// Data de expiração do cartão.
        /// </summary>
        /// <example>
        /// 202012
        /// </example>
        [JsonPropertyName("cardExpirationDate")]
        public string CardExpirationDate { get; set; }
    }
}
