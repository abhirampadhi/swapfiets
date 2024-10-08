using Newtonsoft.Json;

namespace SF.BikeTheft.Domain.Entities;

public sealed class BikeCountEntity
{
    [JsonProperty("non")]
    public int Non { get; set; }
    [JsonProperty("stolen")]
    public int Stolen { get; set; }
    [JsonProperty("proximity")]
    public int Proximity { get; set; }
}