using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MeerPflege.API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeerPflegeAPI", Version = "v1" });
      });
      services.AddDbContext<DataContext>(opt =>
      {
        opt.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
      });
      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy =>
        {
          // policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:9000");
          policy.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowAnyOrigin();
        });
      });

      services.AddMediatR(typeof(MeerPflege.Application.HomeGroups.List.Handler).Assembly);
      services.AddMediatR(typeof(MeerPflege.Application.Homes.List.Handler).Assembly);
      services.AddAutoMapper(typeof(MappingProfiles).Assembly);

      return services;
    }
  }
}