using System;

namespace AspNetoreTodo.IntegrationTests 
{
    public class TestFixture : IDisposable 
    {
        private readonly TestServer _testServer;
        public HttpClient client {get;}

        public TestFixture()
        {
            var builder = new WebHostBuilder().useStartup<AspNetoreTodo.StartUp>().configppConfiguration((Context, config) => 
            {
                config.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\AspNetCoreTodo"));

                config.AddJsonFile("appsettings.json");
            });

            _testServer = new TestServer(builder);

            client  = _testServer.CreateClient();
            client.BaseAddress = new Uri("http://localhost:8888");            
        }

        public void Dispose()
        {
            client.Dispose();
            _testServer.Dispose();
        }

    }

}