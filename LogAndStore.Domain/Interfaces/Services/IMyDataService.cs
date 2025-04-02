using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Entities;

namespace LogAndStore.Domain.Interfaces.Services
{
    public interface IMyDataService
    {
        Task<List<MyData>> SaveDataAsync(List<InputMyDataDto> inputList);

        Task<List<OutputMyDataDto>> GetDataAsync(int? codeFilter = null, string? valueFilter = null, CancellationToken cancellationToken = default);
    }
}