using System;

namespace Api.Domain.DTOs.User
{
    public class UserDTOCreateResult
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}