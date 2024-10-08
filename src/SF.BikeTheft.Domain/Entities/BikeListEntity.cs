using Newtonsoft.Json;

namespace SF.BikeTheft.Domain.Entities;

public sealed class BikeListEntity
{
    [JsonProperty("bikes")]
    public List<BikeEntity> Bikes { get; set; }
}
