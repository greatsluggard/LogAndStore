namespace LogAndStore.Domain.DTO
{
    public class RequestLogDto
    {
        public DateTime Timestamp { get; set; }
        public string MethodName { get; set; } = string.Empty;
        public string RequestData { get; set; } = string.Empty;
        public string? ResponseData { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}