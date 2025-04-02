namespace LogAndStore.Domain.Entities
{
    public class RequestLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string MethodName { get; set; } = null!;
        public string RequestData { get; set; } = null!;
        public string? ResponseData { get; set; }

        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}