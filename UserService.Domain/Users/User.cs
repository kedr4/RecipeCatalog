using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Users
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }

    }
}
