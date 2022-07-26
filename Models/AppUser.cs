using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class AppUser: IdentityUser<int>
    {
        public string Occupation { get; set; }
    }
}
