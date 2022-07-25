using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class AppUser: IdentityUser
    {
        public string Occupation { get; set; }
    }
}
