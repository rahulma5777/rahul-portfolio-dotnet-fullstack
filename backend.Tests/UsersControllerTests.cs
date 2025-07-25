using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NetFullStack.API.Tests
{
    /// <summary>
    /// Basic integration tests for the Users controller.  These tests use the
    /// WebApplicationFactory to spin up the API in memory and perform HTTP
    /// requests against it.  They illustrate how to test controllers end‑to‑end.
    /// </summary>
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetUsers_ReturnsSuccessAndList()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/users");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var users = await response.Content.ReadFromJsonAsync<dynamic>();
            Assert.NotNull(users);
        }
    }
}