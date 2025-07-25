using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NetFullStack.API.Controllers;
using Xunit;

namespace NetFullStack.API.Tests
{
    /// <summary>
    /// Integration tests for the TaskItems controller.  These tests exercise
    /// both authenticated and unauthenticated requests to ensure that the
    /// authorization middleware and CRUD endpoints behave as expected.
    /// </summary>
    public class TaskItemsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TaskItemsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Helper that requests a JWT for a seeded user.  The default seed
        /// creates users like "Alice Johnson" without passwords.  The
        /// AuthController issues a token when provided with a valid username.
        /// </summary>
        private async Task<string?> GetJwtTokenAsync(string username = "Alice Johnson")
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/api/auth/login", new { username });
            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return payload?.Token;
        }

        private class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
        }

        [Fact]
        public async Task GetTaskItems_UnauthorizedWithoutToken()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/taskitems");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetTaskItems_ReturnsSuccessWithToken()
        {
            // Arrange
            var token = await GetJwtTokenAsync();
            Assert.False(string.IsNullOrEmpty(token));
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.GetAsync("/api/taskitems");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var tasks = await response.Content.ReadFromJsonAsync<dynamic>();
            Assert.NotNull(tasks);
        }
    }
}