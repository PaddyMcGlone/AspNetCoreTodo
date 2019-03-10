using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetoreTodo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixture>
    {
        //private readonly HttpClient _client;

        public TodoRouteShould(TestFixture fixture)
        {
          //  _client = fixture.client;
        }

        //[Fact]
        // public async task ChallengeAnonymousUser()
        // {
        //     // Arrange
        //     var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

        //     // Act : Request the Todo route
        //     var response = await _client.SendAsync(request);

        //     // Assert : the user is presented with login page
        //     Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);           
        // }
    }
}