using System.Text.Json.Serialization;

namespace ShopElectronics.Services.Models.Dto;

public class LogInResultDto
{
    [JsonPropertyName("username")] public string Username { get; set; }

    [JsonPropertyName("accessToken")] public string AccessToken { get; set; }
}