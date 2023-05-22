using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Persistence
{
    //dotnet ef migrations add addInitialCreate -p .\MeerPflege.Persistence\ -s .\MeerPflege.API\

    //dotnet ef migrations add IdentityAdded -p .\MeerPflege.Persistence\ -s .\MeerPflege.API\
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        // public DbSet<Activity> Activities { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeGroup> HomeGroups { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<NewsItemAttachment> NewsItemAttachments { get; set; }
    }
}