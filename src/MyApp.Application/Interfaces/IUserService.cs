using SF.BikeTheft.Application.Models.Requests;
using SF.BikeTheft.Application.Models.Responses;

namespace SF.BikeTheft.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserRes> CreateUser(CreateUserReq req);

        Task<ValidateUserRes> ValidateUser(ValidateUserReq req);

        Task<GetAllActiveUsersRes> GetAllActiveUsers();
    }
}