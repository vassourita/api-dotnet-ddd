using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required for logging in")]
        [EmailAddress(ErrorMessage = "Email has invalid format")]
        [StringLength(100, ErrorMessage = "Email should not have more than {1} characters")]
        public string Email { get; set; }
    }
}