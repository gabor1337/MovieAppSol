using System.Net.Http;
using System.Text;

namespace MovieApp.MVVM.Model
{
    class ApiHandler
    {
        static readonly HttpClient client = new HttpClient();
        public string? Url { get; set; }
        public string? Body { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public HttpMethod? Method { get; set; }

        public async Task<HttpResponseMessage> SendRequest()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(Method, Url);

                if (Method == HttpMethod.Post)
                {
                    StringContent content = new StringContent(Body, Encoding.UTF8, "application/json");
                    request.Content = content;
                }

                if (Headers?.Count > 0)
                {
                    foreach (var header in Headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await client.SendAsync(request);

                return response;


            }
            catch (Exception ex)
            {
                throw new Exception($"Request failed: {ex.Message}");
            }
        }

    }
}
