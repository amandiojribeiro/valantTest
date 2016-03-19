namespace ValantTest.Client.Sdk.Helper
{
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public abstract class ApiClient
    {
        private readonly Uri baseUri;

        private readonly HttpClient httpClient;

        protected ApiClient()
        {
            var uriFromConfig = ConfigurationManager.AppSettings["apiEndpoint"];

            if (string.IsNullOrWhiteSpace(uriFromConfig))
            {
                throw new ArgumentNullException("baseUri");
            }

            this.baseUri = new Uri(uriFromConfig);

            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = this.baseUri;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> Post<T>(string path, T value)
        {
            var response = await this.httpClient.PostAsJsonAsync<T>(path, value);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        protected async Task<T> Get<T>(string path)
        {
            var response = await this.httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }
    }
}
