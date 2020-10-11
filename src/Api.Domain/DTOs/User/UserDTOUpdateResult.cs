using System;

namespace Api.Domain.DTOs.User
{
    public class UserDTOUpdateResult
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}