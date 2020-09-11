using System;
using System.Collections.Generic;

namespace PatternSample.Adapter
{
    public class FileAdapter : ILogAdapter
    {
        public void Write(IEnumerable<LogEntry> logs)
        {
            foreach (var logEntry in logs)
            {
                Console.WriteLine("File: {0}", logEntry);
            }
        }
    }
}