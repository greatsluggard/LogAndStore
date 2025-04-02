using LogAndStore.DAL.Repositories;
using LogAndStore.DAL.Services;
using LogAndStore.Domain.Entities;
using LogAndStore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogAndStore.DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddHostedService<MigrationHostedService>();

            services.InitRepositories();
        }

        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<MyData>, BaseRepository<MyData>>();
            services.AddScoped<IBaseRepository<RequestLog>, BaseRepository<RequestLog>>();
        }
    }
}