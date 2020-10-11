using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Name is a required field")]
        [MaxLength(60, ErrorMessage = "Name should not have more than {1} characters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        [Required(ErrorMessage = "Email is a required field")]
        [MaxLength(100, ErrorMessage = "Email should not have more than {1} characters")]
        public string Email { get; set; }
    }
}