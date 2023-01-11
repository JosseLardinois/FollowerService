using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.CognitoIdentityProvider;
using Amazon;
using Amazon.Extensions.CognitoAuthentication;
using System.Net.Http;

namespace FollowerService.UnitTest
{
    public class APIIntegrationTests {


        private readonly HttpClient httpClient;

        private async Task<string> GetAccessToken()
        {
            Amazon.CognitoIdentityProvider.AmazonCognitoIdentityProviderClient provider = new Amazon.CognitoIdentityProvider.AmazonCognitoIdentityProviderClient(
     new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.EUWest1);
            CognitoUserPool userPool = new CognitoUserPool("eu-west-1_QRNd3f8Cu", "6uad7uthfhv3a7p9aihdbjl3tt", provider);
            CognitoUser user = new CognitoUser("test", "6uad7uthfhv3a7p9aihdbjl3tt", userPool, provider);
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = "Test123!"
            };

            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            //var accessToken = authResponse.AuthenticationResult.AccessToken;
            return user.SessionTokens.AccessToken;
        }
        
        public APIIntegrationTests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                    });
                });

            httpClient = application.CreateClient();

        }


        [Theory]
        [InlineData]
        public async Task UnauthorizedGet()
        {
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "follower");
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Theory]
        [InlineData]
        public async Task UnauthorizedPost()
        {
            // Act
            var request = new HttpRequestMessage(HttpMethod.Post, "/follower/Create");
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData]
        public async Task UnauthorizedDelete()
        {
            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "/follower/Delete");
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Theory]
        [InlineData]
        public async Task AuthorizedGet()
        {

            var accesstoken = await GetAccessToken();
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "follower");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData]
        public async Task AuthorizedPost()
        {
            var accesstoken = await GetAccessToken();

            var url = "follower/Create";
            var parameters = new Dictionary<string, string> { { "UserId", "73dbbbde-de8d-4c69-9842-3bf5b7381592" }, { "FollowerId", "73dbbbde-de8d-4c69-9842-3bf5b7381592" } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", accesstoken);

            var response = await httpClient.PostAsync(url, encodedContent).ConfigureAwait(false);
            // Act
          

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
