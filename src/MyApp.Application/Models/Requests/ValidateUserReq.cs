using System.ComponentModel.DataAnnotations;

namespace SF.BikeTheft.Application.Models.Requests
{
    public class ValidateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
