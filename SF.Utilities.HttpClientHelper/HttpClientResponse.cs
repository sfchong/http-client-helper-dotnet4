using System.Net;

namespace HttpClientExecutor
{
    public class HttpClientResponse<T> : IHttpClientResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }

        public string Description { get; set; }
    }
}
