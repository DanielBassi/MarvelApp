using Azure;
using MarvelApp.Application.DTOs;
using MarvelApp.Application.Ports;
using MarvelApp.Domain.Ports;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MarvelApp.Infrastructure.Adapters
{
    public class MarvelApiService : IMarvelApiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly string publicKey;
        private readonly string privateKey;
        private readonly string ts;

        public MarvelApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClient = httpClientFactory.CreateClient("marvelApi");
            this.configuration = configuration;

            ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            publicKey = configuration["Services:marvel:publicKey"] ?? "";
            privateKey = configuration["Services:marvel:privateKey"] ?? "";
        }
        private string CreateMd5Hash()
        {
            var inputString = $"{ts}{privateKey}{publicKey}";
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private async Task<T?> TransformResponse<T>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;
            var results = root.GetProperty("data").GetProperty("results");

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<T>(results.GetRawText(), options);
        }
        public async Task<List<ComicDto>?> GetListAsync()
        {
            var hash = CreateMd5Hash();
            string url = $"comics?ts={ts}&apikey={publicKey}&hash={hash}";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await TransformResponse<List<ComicDto>?>(response);
        }

        public async Task<ComicDto?> GetOneAsync(int comicId)
        {
            List<ComicDto>? comics = await GetListAsync();
            return comics?.FirstOrDefault(w => w.Id.Equals(comicId));
        }
    }
}
