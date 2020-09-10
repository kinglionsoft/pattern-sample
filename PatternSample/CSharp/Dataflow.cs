using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
#pragma warning disable 4014

namespace PatternSample.CSharp
{
    public class Dataflow
    {
        public static async Task Run()
        {
            var throwIfNegative = new ActionBlock<int>(n =>
            {
                Console.WriteLine("n = {0}", n);
                if (n < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            });

            throwIfNegative.Completion.ContinueWith(task =>
            {
                Console.WriteLine("The status of the completion task is '{0}'.",
                    task.Status);
            });

            throwIfNegative.Post(0);
            throwIfNegative.Post(-1);
            throwIfNegative.Post(1);
            throwIfNegative.Post(-2);
            throwIfNegative.Complete();

            try
            {
                throwIfNegative.Completion.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine("Encountered {0}: {1}",
                        e.GetType().Name, e.Message);
                    return true;
                });
            }


            var bufferBlock = new BufferBlock<int>();

            Task.Run(async () =>
            {
                while (true)
                {
                    while (bufferBlock.TryReceive(out var item))
                    {
                        Console.WriteLine(item);
                    }

                    await Task.Delay(100);
                }
            });

            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            await Task.Delay(1000);
        }
    }
}