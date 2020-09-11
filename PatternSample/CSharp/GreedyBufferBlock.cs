using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PatternSample.CSharp
{
    public class GreedyBufferBlock<T>
    {
        private readonly int _batchSize;
        private readonly BufferBlock<T> _bufferBlock = new BufferBlock<T>();

        public GreedyBufferBlock(int batchSize)
        {
            _batchSize = batchSize;
        }

        public bool Post(T item) => _bufferBlock.Post(item);

        public IEnumerable<T> Receive()
        {
            var list = new List<T>
            {
                _bufferBlock.Receive()
            };
            var count = 1;

            while (count < _batchSize && _bufferBlock.TryReceive(out var item))
            {
                count++;
                list.Add(item);
            }

            return list;
        }

        public bool TryReceive(out IEnumerable<T> items)
        {
            items = Receive();

            return true;
        }
    }
}