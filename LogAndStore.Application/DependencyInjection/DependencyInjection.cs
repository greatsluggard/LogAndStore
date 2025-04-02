using LogAndStore.Application.Mapping;
using LogAndStore.Application.Services;
using LogAndStore.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LogAndStore.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMyDataService, MyDataService>();
            services.AddScoped<IRequestLoggerService, RequestLoggerService>();

            services.AddAutoMapper(typeof(MyDataMappingProfile).Assembly);
        }
    }
}