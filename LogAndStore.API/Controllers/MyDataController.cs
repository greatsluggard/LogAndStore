using LogAndStore.Domain.DTO;
using LogAndStore.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogAndStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDataController(IMyDataService myDataService) : ControllerBase
    {
        /// <summary>
        /// Сохраняет данные в таблицу. Таблица предварительно очищается.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     [
        ///         { "code": "18", "value": "value1" },
        ///         { "code": "7", "value": "value2" },
        ///         { "code": "32", "value": "value32" }
        ///     ]
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] List<InputMyDataDto> inputList)
        {
            var savedData = await myDataService.SaveDataAsync(inputList);
            return Ok(savedData);
        }

        /// <summary>
        /// Получение отсортированных по порядковому номеру данных из таблицы. 
        /// Фильтрацию можно сделать по полям code или value. 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetData(
            [FromQuery] int? code,
            [FromQuery] string? value,
            CancellationToken cancellationToken)
        {
            var result = await myDataService.GetDataAsync(code, value, cancellationToken);
            return Ok(result);
        }
    }
}