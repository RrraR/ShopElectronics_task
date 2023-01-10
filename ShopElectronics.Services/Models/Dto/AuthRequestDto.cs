using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopElectronics.Services.Models.Dto;

public class AuthRequestDto
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}