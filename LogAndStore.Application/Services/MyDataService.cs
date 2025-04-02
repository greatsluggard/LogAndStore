using AutoMapper;
using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Entities;
using LogAndStore.Domain.Interfaces.Repositories;
using LogAndStore.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace LogAndStore.Application.Services
{
    public class MyDataService(
        IBaseRepository<MyData> myDataRepository, 
        IRequestLoggerService requestLogger,
        IMapper mapper) : IMyDataService
    {
        public async Task<List<MyData>> SaveDataAsync(List<InputMyDataDto> inputList)
        {
            var logEntry = CreateLog(nameof(SaveDataAsync), inputList);

            try
            {
                var myDataList = mapper
                    .Map<List<MyData>>(inputList)
                    .Where(x => x.Code != 0)
                    .OrderBy(x => x.Code)
                    .ToList();

                await myDataRepository.ClearTableAsync();
                await myDataRepository.CreateRangeAsync(myDataList);

                await HandleLogAsync(logEntry, myDataList);
                return myDataList;
            }
            catch (Exception ex)
            {
                await HandleLogAsync(logEntry, null, ex);
                throw;
            }
        }

        public async Task<List<OutputMyDataDto>> GetDataAsync(
            int? codeFilter = null,
            string? valueFilter = null,
            CancellationToken cancellationToken = default)
        {
            var logEntry = CreateLog(nameof(GetDataAsync), new { codeFilter, valueFilter });

            try
            {
                var dataList = await myDataRepository.GetListAsync(cancellationToken);

                var filteredData = dataList
                    .Where(x =>
                        (!codeFilter.HasValue || x.Code == codeFilter.Value) &&
                        (string.IsNullOrEmpty(valueFilter) || (x.Value != null && x.Value.Contains(valueFilter))))
                    .OrderBy(x => x.Code)
                    .ToList();

                var result = mapper.Map<List<OutputMyDataDto>>(filteredData);

                await HandleLogAsync(logEntry, result);
                return result;
            }
            catch (Exception ex)
            {
                await HandleLogAsync(logEntry, null, ex);
                throw;
            }
        }

        #region Helpers
        private RequestLog CreateLog(string methodName, object requestData) => new()
        {
            MethodName = methodName,
            RequestData = JsonConvert.SerializeObject(requestData)
        };

        private async Task HandleLogAsync(RequestLog log, object? response = null, Exception? ex = null)
        {
            if (ex is not null)
            {
                log.IsSuccess = false;
                log.ErrorMessage = ex.Message;
            }
            else
            {
                log.IsSuccess = true;
                if (response is not null)
                    log.ResponseData = JsonConvert.SerializeObject(response);
            }

            await requestLogger.LogAsync(log);
        }
        #endregion
    }
}