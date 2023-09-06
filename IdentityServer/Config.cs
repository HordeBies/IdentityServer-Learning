using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //new Client
                //{
                //    ClientId="MovieClient",
                //    AllowedGrantTypes=GrantTypes.ClientCredentials,
                //    ClientSecrets={new Secret("secret".Sha256())},
                //    AllowedScopes={"MovieAPI"}
                //},
                new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent =false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:7003/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7003/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "MovieAPI"
                    },
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("MovieAPI", "Movie API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource("roles","Your role(s)", new List<string>(){ "role" })
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "61029AAE-DACB-42FE-A466-3B045B6AFF51",
                    Username = "bies",
                    Password = "bies",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Mehmet"),
                        new Claim(JwtClaimTypes.FamilyName, "Demirci")
                    }
                }
            };
    }
}
