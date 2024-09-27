namespace SF.BikeTheft.WebApi.Models;

public class BikeTheft
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string City { get; set; }
    public DateTime DateStolen { get; set; }
    public string StolenLocation { get; set; }
    public string Description { get; set; }
    public BikeDetails BikeDetails { get; set; }
}
