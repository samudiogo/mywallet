using System.Net;
using System.Net.Http;

namespace MyWallet.WebApi.Utils
{
    public class ResponseMessageUtils
    {
        public static HttpResponseMessage CustomMessage(HttpRequestMessage request, HttpStatusCode statusCode, string message)
        {
            return request.CreateResponse(statusCode, new
            {
                statusCode,
                mensagem = message
            });
        }
    }
}