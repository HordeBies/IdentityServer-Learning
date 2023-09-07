using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;

namespace Movies.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the discovery document");
            }

            var accessToken = await httpContextAccessor.HttpContext?.GetUserAccessTokenAsync() ?? throw new Exception("Access token not found");

            var userInfoResponse = await idpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            });
            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the user info");
            }

            //var userInfoDictionary = new Dictionary<string, string>();
            //foreach (var claim in userInfoResponse.Claims)
            //{
            //    userInfoDictionary.Add(claim.Type, claim.Value);
            //}
            var dict = userInfoResponse.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
            dict["accessToken"] = accessToken;
            dict["refreshToken"] = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            dict["id_token"] = await httpContextAccessor.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            return new(dict);
        }
    }
}
