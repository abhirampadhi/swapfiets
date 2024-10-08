namespace SF.BikeTheft.Application.Queries;

public sealed class GetBikeTheftsCountQuery
{
    public GetBikeTheftsCountQuery(string city, int distance)
    {
        City = city;
        Distance = distance;
    }

    public string City { get; set; }
    public int Distance { get; set; }
}

