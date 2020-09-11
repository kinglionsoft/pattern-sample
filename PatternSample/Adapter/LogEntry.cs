namespace PatternSample.Adapter
{
    public class LogEntry
    {
        public int EventId { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"[{EventId}]{Message}";
        }
    }
}