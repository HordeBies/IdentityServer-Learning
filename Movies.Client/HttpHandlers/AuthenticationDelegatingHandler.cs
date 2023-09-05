using IdentityModel.Client;

namespace Movies.Client.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientCredentialsTokenRequest clientCredentialsTokenRequest;

        public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest clientCredentialsTokenRequest)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.clientCredentialsTokenRequest = clientCredentialsTokenRequest ?? throw new ArgumentNullException(nameof(clientCredentialsTokenRequest));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("IDPClient");
            
            var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
            if (token.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }
            
            request.SetBearerToken(token.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
