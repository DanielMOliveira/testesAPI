using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class MerchantMiscDataAntiFraudDTO
    {
        /// <summary>
        /// Dados de MDD - ID
        /// </summary>
        /// <example>1</example>
        public string MddFieldId { get; set; }

        /// <summary>
        /// Dados de MDD - Valor
        /// </summary>
        /// <example>1970-10-01</example>
        public string MddFieldValue { get; set; }
    }
}
