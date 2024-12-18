﻿using HubPagamento.ApiExterna.Service.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonPropertyName("bandeira")]
        public string FlagBrand { get; set; }

        /// <summary>
        /// Nome como está escrito no cartão
        /// </summary>
        /// <example>
        /// John Doe
        /// </example>
        public string CardHolder { get; set; }

        /// <summary>
        /// Numero como está escrito no cartão. Sem simbolos ou espaços.
        /// </summary>
        /// <example>
        /// 4548812049400004
        /// </example>
        public string CardNumber { get; set; }

        /// <summary>
        /// Codigo de segurança do cartão.
        /// </summary>
        /// <example>
        /// 123
        /// </example>
        public string CardSecurityCode { get; set; }

        /// <summary>
        /// Data de expiração do cartão.
        /// </summary>
        /// <example>
        /// 202012
        /// </example>
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Objeto que representa a entidade Fatura
        /// </summary>
        [JsonPropertyName("fatura")]
        public BillDTO Bill { get; set; }

        /// <summary>
        /// Informe "true" para consulta de antifraude. 
        /// </summary>
        /// <example>
        /// false
        /// </example>
        [JsonPropertyName("fraud")]
        public bool Fraud { get; set; }
        /// <summary>
        /// Dados para analise de antifraude
        /// </summary>
        [JsonPropertyName("antifraude")]
        public AntiFraudDTO? AntiFraud { get; set; }
    }
}
