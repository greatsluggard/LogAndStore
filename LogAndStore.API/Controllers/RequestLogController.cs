using LogAndStore.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogAndStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestLogsController(IRequestLoggerService loggerService) : ControllerBase
    {
        /// <summary>
        /// Получение всех логов отсортированных по дате.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllLogs(CancellationToken cancellationToken)
        {
            var logs = await loggerService.GetAllLogsAsync(cancellationToken);
            return Ok(logs);
        }
    }
}