using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class CardM4UBemodiDTO
    {
        public CardM4UBemodiDTO()
        {

        }

        public CardM4UBemodiDTO(string pan, string month, string year, string partner)
        {
            Pan = pan;
            Month = month;
            Year = year;
            Partner = partner;
        }

        /// <summary>
        /// Campo do Número do cartão
        /// </summary>
        public string Pan { get; set; }

        /// <summary>
        /// Campo do Mês de vencimento do cartão
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Campo do Ano de vencimento do cartão
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Campo para informar o parceiro da M4U. Obs.: Nosso caso sempre será preenchido como CLARO
        /// </summary>
        public string Partner { get; set; }
    }
}
