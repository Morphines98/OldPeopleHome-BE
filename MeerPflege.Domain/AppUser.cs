using Microsoft.AspNetCore.Identity;

namespace MeerPflege.Domain
{
    public class AppUser : IdentityUser
    {
        public int HomeId { get; set; }

        public int? NurseId { get; set; }

        public int? CarerId { get; set; }

        public bool IsInactive { get; set; }
    }
}