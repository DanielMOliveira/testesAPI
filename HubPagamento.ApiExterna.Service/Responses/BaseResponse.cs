using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {

        }

        protected BaseResponse(object result)
        {
            Result = result;
        }

        public object Result { get; set; }
        public bool IsSucess { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
    }
}
