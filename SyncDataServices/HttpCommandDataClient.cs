using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{

    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

        }
        public async Task SendPlatformToCommand(PlateformReadDtos platformread)
        {
            var httpContent = new StringContent(
               JsonSerializer.Serialize(platformread),
               Encoding.UTF8, "application/json"
                    );
            Console.WriteLine(_configuration["CommandService"]);
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("-->Sync post to command service was OK! <--");
            }
            else
            {
                Console.WriteLine("-->Sync post to command service not was OK! <--");
            }
        }

    }
}