using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Persistence
{
  //dotnet ef migrations add addInitialCreate -p .\MeerPflege.Persistence\ -s .\MeerPflege.API\
  //din persistance dotnet ef -s ../MeerPflege.API  migrations add addNurse 
  //din persistance dotnet ef -s ../MeerPflege.API  database update

  //dotnet ef migrations add IdentityAdded -p .\MeerPflege.Persistence\ -s .\MeerPflege.API\
  public class DataContext : IdentityDbContext<AppUser>
  {
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<Home> Homes { get; set; }
    public DbSet<HomeGroup> HomeGroups { get; set; }
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<NewsItemAttachment> NewsItemAttachments { get; set; }
    public DbSet<WallItem> WallItems { get; set; }
    public DbSet<WallItemAttachment> WallItemAttachments { get; set; }
    public DbSet<Nurse> Nurses { get; set; }
    public DbSet<Carer> Carers { get; set; }
    public DbSet<Elder> Elders { get; set; }
    public DbSet<ActivityElderPresence> ActivityElderPresences { get; set; }
    public DbSet<WorkingHoursForDay> WorkingHoursForDays { get; set; }
    public DbSet<WorkingInterval> WorkingIntervals { get; set; }
  }
}