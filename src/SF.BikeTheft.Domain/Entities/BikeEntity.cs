using Newtonsoft.Json;

namespace SF.BikeTheft.Domain.Entities;

public sealed class BikeEntity
{
    [JsonProperty("date_stolen")]
    public long DateStolen { get; set; } 

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("frame_colors")]
    public List<string> FrameColors { get; set; }

    [JsonProperty("frame_model")]
    public string FrameModel { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("is_stock_img")]
    public bool IsStockImg { get; set; }

    [JsonProperty("large_img")]
    public string LargeImg { get; set; }

    [JsonProperty("location_found")]
    public string LocationFound { get; set; }

    [JsonProperty("manufacturer_name")]
    public string ManufacturerName { get; set; }

    [JsonProperty("external_id")]
    public string ExternalId { get; set; }

    [JsonProperty("registry_name")]
    public string RegistryName { get; set; }

    [JsonProperty("registry_url")]
    public string RegistryUrl { get; set; }

    [JsonProperty("serial")]
    public string Serial { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("stolen")]
    public bool Stolen { get; set; }

    [JsonProperty("stolen_coordinates")]
    public List<double> StolenCoordinates { get; set; }

    [JsonProperty("stolen_location")]
    public string StolenLocation { get; set; }

    [JsonProperty("thumb")]
    public string Thumb { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("year")]
    public int? Year { get; set; }

    [JsonProperty("propulsion_type_slug")]
    public string PropulsionTypeSlug { get; set; }

    [JsonProperty("cycle_type_slug")]
    public string CycleTypeSlug { get; set; }
}
