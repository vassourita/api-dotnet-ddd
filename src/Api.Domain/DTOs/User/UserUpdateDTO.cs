using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }

        [MaxLength(60, ErrorMessage = "Name should not have more than {1} characters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100, ErrorMessage = "Email should not have more than {1} characters")]
        public string Email { get; set; }
    }
}