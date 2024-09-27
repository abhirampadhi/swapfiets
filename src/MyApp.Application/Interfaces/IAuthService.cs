namespace SF.BikeTheft.Application.Interfaces;

public interface IAuthService
{
    Task<UserDto> LoginAsync(LoginUserDto loginUser);
    Task RegisterAsync(RegisterUserDto registerUser);
}
