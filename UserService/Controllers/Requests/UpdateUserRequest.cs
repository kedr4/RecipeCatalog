﻿namespace UserService.Api.Controllers.Requests
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
    }
}
