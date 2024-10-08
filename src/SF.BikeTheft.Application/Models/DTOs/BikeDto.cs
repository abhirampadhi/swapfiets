namespace SF.BikeTheft.Application.Models.DTOs;

public sealed class BikeDto
{
    public int Id { get; set; }
    public long DateStolen { get; set; }
    public string Description { get; set; }
    public List<string> FrameColors { get; set; }
    public string FrameModel { get; set; }
    public bool IsStockImg { get; set; }
    public string LargeImg { get; set; }
    public string LocationFound { get; set; }
    public string ManufacturerName { get; set; }
    public string ExternalId { get; set; }
    public string RegistryName { get; set; }
    public string RegistryUrl { get; set; }
    public string Serial { get; set; }
    public string Status { get; set; }
    public bool Stolen { get; set; }
    public List<double> StolenCoordinates { get; set; }
    public string StolenLocation { get; set; }
    public string Thumb { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int Year { get; set; }
    public string PropulsionTypeSlug { get; set; }
    public string CycleTypeSlug { get; set; }
}
