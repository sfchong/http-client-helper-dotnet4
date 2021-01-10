using System.Net;

namespace HttpClientExecutor
{
    public interface IHttpClientResponse<T>
    {
        HttpStatusCode StatusCode { get; set; }

        T Data { get; set; }

        string Description { get; set; }
    }
}
