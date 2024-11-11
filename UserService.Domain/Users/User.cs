using Microsoft.AspNet.Identity.EntityFramework;

namespace UserService.Domain.Users
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }

    }
}
