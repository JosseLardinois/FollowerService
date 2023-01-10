using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;


namespace FollowerService.UnitTest
{
    public class APIIntegrationTests {


        private readonly HttpClient httpClient;

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


        //Since logging is done via hostedUI, authorized is not testable
    }
}
