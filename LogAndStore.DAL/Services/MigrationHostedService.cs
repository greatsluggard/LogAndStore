using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogAndStore.DAL.Services
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MigrationHostedService> _logger;

        public MigrationHostedService(IServiceProvider serviceProvider, ILogger<MigrationHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            const int maxRetries = 10;
            int delayMs = 2000;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    _logger.LogInformation("Попытка подключения к БД и применения миграций...");
                    await dbContext.Database.MigrateAsync(cancellationToken);
                    _logger.LogInformation("Миграции успешно применены.");
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Попытка {i + 1} не удалась: {ex.Message}");
                    if (i == maxRetries - 1) throw;

                    await Task.Delay(delayMs, cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}