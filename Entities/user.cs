// using System;
// using System.ComponentModel.DataAnnotations;

namespace Rawwr.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}