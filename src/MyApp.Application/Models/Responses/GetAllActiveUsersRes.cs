using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Models.Responses
{
    public class GetAllActiveUsersRes
    {
        public IList<UserDTO> Data { get; set; }
    }
}
