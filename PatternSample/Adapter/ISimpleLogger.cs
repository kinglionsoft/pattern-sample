namespace PatternSample.Adapter
{
    public interface ISimpleLogger
    {
        void Log(LogEntry entry);

        void Info(string message) => Log(new LogEntry { EventId = 0, Message = message });
    }
}