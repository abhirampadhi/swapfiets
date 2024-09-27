using SF.BikeTheft.Domain.Entities;

namespace SF.BikeTheft.Infrastructure.Interface;

public interface IUserRepository
{
    Task<User> GetByUserNameAsync(string userName);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddUserAsync(User user);
}