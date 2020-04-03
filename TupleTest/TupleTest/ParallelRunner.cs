using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TupleTest
{
    public static class ParallelRunner
    {
        public static void Run(Action<Tuple<int, int>> act, int collectionSize)
        {
            var rangePartitioner = Partitioner.Create(0, collectionSize, collectionSize / MaxCoreCount + 1);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                act.Invoke(range);
            });
        }

        public static void Run(Calculator act, int collectionSize)
        {
            var rangePartitioner = Partitioner.Create(0, collectionSize, collectionSize / MaxCoreCount + 1);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                act(range).Invoke();
            });
        }

        public delegate Action Calculator(Tuple<int, int> range);

        public static readonly int MaxCoreCount = Environment.ProcessorCount;
    }
}
