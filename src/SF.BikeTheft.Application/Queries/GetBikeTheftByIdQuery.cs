namespace SF.BikeTheft.Application.Queries;

public sealed class GetBikeTheftByIdQuery
{
    public GetBikeTheftByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
