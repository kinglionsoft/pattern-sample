using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatternSample.CSharp;

namespace PatternSample.Adapter
{
    public class SimpleLogger : ISimpleLogger
    {
        private readonly GreedyBufferBlock<LogEntry> _bufferBlock;
        private readonly IReadOnlyList<ILogAdapter> _adapters;

        public SimpleLogger(ILogAdapterManager adapterManager)
        {
            _adapters = adapterManager.GetAdapters().ToList();
            _bufferBlock = new GreedyBufferBlock<LogEntry>(100);

            Task.Run(() =>
            {
                while (_bufferBlock.TryReceive(out var logs))
                {
                    foreach (var adapter in _adapters)
                    {
                        adapter.Write(logs);
                    }
                }
            });
        }

        public void Log(LogEntry entry)
        {
            _bufferBlock.Post(entry); // 立即返回，不等待日志的处理流程
        }
    }
}