using System.Net;

namespace ShakespearePokemons.Contracts.Response
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ErrorResponse(HttpStatusCode code, string message)
        {
            Code = (int)code;
            Message = message;
        }
    }
}
