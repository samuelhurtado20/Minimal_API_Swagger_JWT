﻿namespace Minimal_API_Swagger_JWT.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }

    }
}
