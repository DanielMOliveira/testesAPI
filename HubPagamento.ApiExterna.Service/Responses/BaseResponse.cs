using System.Dynamic;
using System.Net;
using System.Text.Json;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {

        }

        public BaseResponse(string json)
        {
            Result = GetObject(json);
        }

        internal ExpandoObject? GetObject(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<ExpandoObject>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ExpandoObject? Result { get; private set; }
        public bool IsSucess { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
    }
}
