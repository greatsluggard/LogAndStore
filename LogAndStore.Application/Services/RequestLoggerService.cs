using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Entities;
using LogAndStore.Domain.Interfaces.Repositories;
using LogAndStore.Domain.Interfaces.Services;

namespace LogAndStore.Application.Services
{
    public class RequestLoggerService(IBaseRepository<RequestLog> requestLogRepository) : IRequestLoggerService
    {
        public async Task LogAsync(RequestLog logEntry)
        {
            await requestLogRepository.CreateAsync(logEntry);
        }

        public async Task<List<RequestLogDto>> GetAllLogsAsync(CancellationToken cancellationToken = default)
        {
            var logs = await requestLogRepository.GetListAsync(cancellationToken);

            return logs
                .OrderByDescending(l => l.Timestamp)
                .Select(l => new RequestLogDto
                {
                    Timestamp = l.Timestamp,
                    MethodName = l.MethodName,
                    RequestData = l.RequestData,
                    ResponseData = l.ResponseData,
                    IsSuccess = l.IsSuccess,
                    ErrorMessage = l.ErrorMessage
                })
                .ToList();
        }
    }
}