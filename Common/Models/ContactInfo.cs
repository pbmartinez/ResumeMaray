using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class ContactInfo
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("subject")]
        public string? Subject { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("message")]
        public string? Message { get; set; } = string.Empty;
    }
}
