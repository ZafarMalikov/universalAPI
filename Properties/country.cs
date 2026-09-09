using System.Text.Json.Serialization;

namespace WebApplication2.Properties;

public class Country
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("name_ru")]
    public string NameRu { get; set; } = string.Empty;

    [JsonPropertyName("name_uz")]
    public string NameUz { get; set; } = string.Empty;

    [JsonPropertyName("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [JsonPropertyName("is_popular")]
    public int IsPopular { get; set; }

    [JsonPropertyName("is_schengen")]
    public int IsSchengen { get; set; }
}