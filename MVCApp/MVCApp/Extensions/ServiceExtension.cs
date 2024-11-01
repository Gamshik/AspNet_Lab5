using AutoMapper;
using BusinessLogic;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using DbAccess.Context;
using DbAccess.Repositories;
using MapperHelper.Profiles;
using MapperHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCApp.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<LogisticContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LogisticConnection"),
                db => db.MigrationsAssembly("MVCApp")));
        }
        public static void ConfigureScopedDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<ISettlementRepository, SettlementRepository>();

            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ISettlementService, SettlementService>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(opt => opt.AddProfiles(new List<Profile>()
            {
                new CargoProfile(),
                new RouteProfile(),
                new SettlementProfile(),
            }));

            services.AddScoped<IMapper>(x => new Mapper(config));

            services.AddScoped<IMapperService, MapperService>();
        }

        public static void ConfigureCacheProfiles(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("EntityCache",
                    new CacheProfile()
                    {
                        Duration = 268,
                        Location = ResponseCacheLocation.Client,
                        NoStore = false,
                    });
            });
        }
    }
}
