using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExecutor
{
    public static class HttpClientExtension
    {
        public static Task<HttpResponseMessage> GetAsync(
            this HttpClient httpClient, 
            string uri, 
            Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync(
            this HttpClient httpClient, 
            string uri, 
            HttpContent content, 
            Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static Task<HttpResponseMessage> PutAsync(
            this HttpClient httpClient, 
            string uri, 
            HttpContent content, 
            Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = content
            };

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static Task<HttpResponseMessage> DeleteAsync(
            this HttpClient httpClient, 
            string uri, 
            Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }
    }
}
