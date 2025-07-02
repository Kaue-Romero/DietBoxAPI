using System.Net.Http;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using DietBoxAPI;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        var loginData = new
        {
            Username = "nutri",
            Password = "nutri123"
        };

        var response = await _client.PostAsJsonAsync("/api/Auth/login", loginData);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        Assert.False(string.IsNullOrWhiteSpace(content.Token));
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var loginData = new
        {
            Username = "nutri",
            Password = "nutri1222223" 
        };

        var response = await _client.PostAsJsonAsync("/api/Auth/login", loginData);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetPatients_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/Patient");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetPatients_WithValidToken_ReturnsOk()
    {
        var loginData = new
        {
            Username = "nutri",
            Password = "nutri123"
        };

        var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", loginData);
        loginResponse.EnsureSuccessStatusCode();

        var loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Patient");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginContent!.Token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetPatients_WithInvalidToken_ReturnsUnauthorized()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Patient");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "invalid-token");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }



    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
