using System;
using System.Net;

namespace ShakespearePokemons.Commons
{

    public class SimpleHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public int StatusCodeValue => (int)StatusCode;

        public SimpleHttpResponseException(HttpStatusCode statusCode, string content) : base(content)
        {
            StatusCode = statusCode;
        }

        public SimpleHttpResponseException(HttpStatusCode statusCode, string content, Exception innerException) : base(content, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
