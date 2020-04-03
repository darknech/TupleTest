using System;
using System.Diagnostics;
using System.IO;

namespace TupleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                RunDelegate();
                RunTuple();
            }
        }

        private static void RunDelegate()
        {
            var arrayDelegate = new double[1000000];

            Action DelegateCalc(Tuple<int, int> range)
            {
                return () =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        arrayDelegate[i] = Math.Pow(i, 2);
                    }
                };
            }

            myStopwatch.Restart();

            ParallelRunner.Run(DelegateCalc, arrayDelegate.Length);

            File.AppendAllText("log.txt", "Delegate: " + myStopwatch.ElapsedTicks + Environment.NewLine);
        }

        private static void RunTuple()
        {
            var arrayTuple = new double[1000000];

            myStopwatch.Restart();

            void TupleCalc(Tuple<int, int> range)
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    arrayTuple[i] = Math.Pow(i, 2);
                }
            }

            ParallelRunner.Run(TupleCalc, arrayTuple.Length);

            File.AppendAllText("log.txt", "Tuple: " + myStopwatch.ElapsedTicks + Environment.NewLine);
        }

        private static readonly Stopwatch myStopwatch = new Stopwatch();
    }
}
