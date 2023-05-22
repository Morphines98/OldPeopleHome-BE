using Microsoft.AspNetCore.Identity;

namespace MeerPflege.Domain
{
    public class AppUser : IdentityUser
    {
        public int HomeId { get; set; }
    }
}