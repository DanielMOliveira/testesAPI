using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.IoC.Configuration.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
