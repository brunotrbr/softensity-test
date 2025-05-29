using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccessControl.Domain.Models
{
    public class Door
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("doors_ids_with_access")]
        public List<string> DoorsIdsWithAccess { get; set; } = [];
    }
}