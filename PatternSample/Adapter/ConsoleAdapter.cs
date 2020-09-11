using System;
using System.Collections.Generic;

namespace PatternSample.Adapter
{
    public class ConsoleAdapter : ILogAdapter
    {
        public void Write(IEnumerable<LogEntry> logs)
        {
            foreach (var logEntry in logs)
            {
                Console.WriteLine("Console: {0}", logEntry);
            }
        }
    }
}