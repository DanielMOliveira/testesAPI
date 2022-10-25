using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class BillResponse
    {
        /// <summary>
        /// Número de identificação único do portador.
        /// </summary>
        /// <example>
        /// 12332423
        /// </example> 
        [JsonPropertyName("customerIdentity")]
        public string CustomerIdentity { get; set; }

        /// <summary>
        /// Nome do portador.
        /// </summary>
        /// <example>
        /// John Doe
        /// </example> 
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        /// <summary>
        /// Endereço do portador.
        /// </summary>
        /// <example>
        /// Av. República do Brasil, 1988
        /// </example> 
        [JsonPropertyName("endereco")]
        public string Address { get; set; }

        /// <summary>
        /// Complemento do portador.
        /// </summary>
        /// <example>
        /// 08 Andar
        /// </example> 
        [JsonPropertyName("complemento")]
        public string Complement { get; set; }

        /// <summary>
        /// Cidade do portador.
        /// </summary>
        /// <example>
        /// São Paulo
        /// </example> 
        [JsonPropertyName("cidade")]
        public string City { get; set; }

        /// <summary>
        /// Estado do portador.
        /// </summary>
        /// <example>
        /// SP
        /// </example> 
        [JsonPropertyName("estado")]
        public string State { get; set; }

        /// <summary>
        /// CEP, código postal do portador.
        /// </summary>
        /// <example>
        /// 08742000
        /// </example> 
        [JsonPropertyName("codigoPostal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// País do portador.
        /// </summary>
        /// <example>
        /// BR
        /// </example> 
        [JsonPropertyName("pais")]
        public string Country { get; set; }

        /// <summary>
        /// Telefone do portador.
        /// </summary>
        /// <example>
        /// 998712121
        /// </example> 
        [JsonPropertyName("telefone")]
        public string Phone { get; set; }

        /// <summary>
        /// E-mail do portador.
        /// </summary>
        /// <example>
        /// meucliente@site.com.br
        /// </example> 
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
