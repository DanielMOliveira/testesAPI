using HubPagamento.ApiExterna.Service.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Requests
{
    public class CardDTO
    {
        /// <summary>
        /// Bandeira do Cartão
        /// </summary>
        /// <example>
        /// Mastercard
        /// </example>
        [Required]
        public string FlagBrand { get; set; }

        /// <summary>
        /// Nome como está escrito no cartão
        /// </summary>
        /// <example>
        /// John Doe
        /// </example>
        [Required]
        public string CardHolder { get; set; }

        /// <summary>
        /// Numero como está escrito no cartão. Sem simbolos ou espaços.
        /// </summary>
        /// <example>
        /// 4548812049400004
        /// </example>
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Codigo de segurança do cartão.
        /// </summary>
        /// <example>
        /// 123
        /// </example>
        [Required]
        public string CardSecurityCode { get; set; }

        /// <summary>
        /// Data de expiração do cartão.
        /// </summary>
        /// <example>
        /// 202012
        /// </example>
        [Required]
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Objeto que representa a entidade Fatura
        /// </summary>
        public BillDTO Bill;

        /// <summary>
        /// Informe "true" para consulta de antifraude. 
        /// </summary>
        /// <example>
        /// false
        /// </example>
        [Required]
        public bool Fraud { get; set; }
        /// <summary>
        /// Dados para analise de antifraude
        /// </summary>
        public AntiFraudDTO? AntiFraud { get; set; }
    }
}
