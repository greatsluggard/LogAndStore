using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Entities;

namespace LogAndStore.Domain.Interfaces.Services
{
    public interface IRequestLoggerService
    {
        Task LogAsync(RequestLog logEntry);

        Task<List<RequestLogDto>> GetAllLogsAsync(CancellationToken cancellationToken = default);
    }
}