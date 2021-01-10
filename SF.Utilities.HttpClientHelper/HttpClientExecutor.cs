using HttpClientExecutor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SF.Utilities.HttpClientHelper
{
    public class HttpClientExecutor<T>
    {
        private static HttpClient _client;

        private HttpContent _httpContent;
        private string _mediaType = string.Empty;

        static HttpClientExecutor()
        {
            _client = new HttpClient();
        }

        public HttpClientExecutor(string mediaType = "application/json", long? responseBufferSize = null)
        {
            if (responseBufferSize.HasValue)
                _client.MaxResponseContentBufferSize = responseBufferSize.Value;

            this._mediaType = mediaType;
        }

        public async Task<IHttpClientResponse<T>> PostAsync(string url, List<KeyValuePair<string, string>> param)
        {
            string message = string.Empty;
            string content = string.Empty;

            _httpContent = new FormUrlEncodedContent(param);
            var response = await _client.PostAsync(url, _httpContent);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                message = response.Content.ReadAsStringAsync().Result;
            }

            return new HttpClientResponse<T>()
            {
                StatusCode = response.StatusCode,
                Data = JsonConvert.DeserializeObject<T>(content),
                Description = message
            };
        }

        public async Task<IHttpClientResponse<T>> PostAsync<Request>(string url, Request entity, string accessToken = null)
        {
            string message = string.Empty;
            string content = string.Empty;
            var response = new HttpResponseMessage();

            _httpContent = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, _mediaType);

            if (string.IsNullOrEmpty(accessToken))
            {
                response = await _client.PostAsync(url, _httpContent);
            }
            else
            {
                response = await _client.PostAsJsonAsync(
                    url, 
                    _httpContent, 
                    x => x.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken));
            }
            
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }

            return new HttpClientResponse<T>()
            {
                StatusCode = response.StatusCode,
                Data = JsonConvert.DeserializeObject<T>(content),
                Description = message
            };
        }

        public async Task<IHttpClientResponse<T>> GetAsync(string url, string accessToken = null)
        {
            string message = string.Empty;
            string content = string.Empty;
            var response = new HttpResponseMessage();

            if (string.IsNullOrEmpty(accessToken))
            {
                response = await _client.GetAsync(url);
            }
            else
            {
                response = await _client.GetAsync(
                    url, 
                    x => x.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken));
            }
            
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }

            return new HttpClientResponse<T>()
            {
                StatusCode = response.StatusCode,
                Data = JsonConvert.DeserializeObject<T>(content),
                Description = message
            };
        }

        public async Task<IHttpClientResponse<T>> PutAsync(string url, string accessToken, T entity)
        {
            string message = string.Empty;
            string content = string.Empty;
            var response = new HttpResponseMessage();

            _httpContent = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, _mediaType);

            if (string.IsNullOrEmpty(accessToken))
            {
                response = await _client.PutAsync(url, _httpContent);
            }
            else
            {
                response = await _client.PutAsync(
                    url, 
                    _httpContent, 
                    x => x.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken));
            }
            
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }

            return new HttpClientResponse<T>()
            {
                StatusCode = response.StatusCode,
                Data = JsonConvert.DeserializeObject<T>(content),
                Description = message
            };
        }

        public async Task<IHttpClientResponse<T>> Delete(string url, string accessToken)
        {
            string message = string.Empty;
            string content = string.Empty;
            var response = new HttpResponseMessage();

            if (string.IsNullOrEmpty(accessToken))
            {
                response = await _client.DeleteAsync(url);
            }
            else
            {
                response = await _client.DeleteAsync(
                    url, 
                    x => x.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken));
            }
            
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }

            return new HttpClientResponse<T>()
            {
                StatusCode = response.StatusCode,
                Data = JsonConvert.DeserializeObject<T>(content),
                Description = message
            };
        }
    }
}
