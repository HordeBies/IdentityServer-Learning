using IdentityModel.Client;
using Movies.Client.Models;
using System.Text.Json;

namespace Movies.Client.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var client = httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/movies")
            {
                Content = JsonContent.Create(movie)
            };

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonSerializer.Deserialize<Movie>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return movieResponse; // ?? throw error
        }

        public async Task DeleteMovie(int id)
        {
            var client = httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/movies/{id}");

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Movie> GetMovie(int id)
        {
            var client = httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/movies/{id}");

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<Movie>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return movie; // ?? throw error
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var client = httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<IEnumerable<Movie>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return movies ?? new List<Movie>();

            // --Hard way--
            //TokenResponse token;
            //var is4client = new HttpClient();
            //var apiClientCredentials = new ClientCredentialsTokenRequest
            //{
            //    Address = "https://localhost:7002/connect/token",
            //    ClientId = "MovieClient",
            //    ClientSecret = "secret",
            //    Scope = "MovieAPI"
            //};
            //var disco = await is4client.GetDiscoveryDocumentAsync("https://localhost:7002");
            //if (disco.IsError)
            //{
            //    throw new Exception(disco.Error);
            //}
            //token = await is4client.RequestClientCredentialsTokenAsync(apiClientCredentials);

            //using var client = new HttpClient();
            //client.SetBearerToken(token.AccessToken ?? throw new Exception("Bearer token is null"));
            //var response = await client.GetAsync("https://localhost:7001/api/movies");
            //response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //var movies = JsonSerializer.Deserialize<IEnumerable<Movie>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //return movies ?? new List<Movie>();
        }

        public async Task UpdateMovie(Movie movie)
        {
            var client = httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/movies/{movie.Id}")
            {
                Content = JsonContent.Create(movie)
            };

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
