using System;
using System.Diagnostics;
using System.IO;

namespace TupleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 1000;
            for (int i = 0; i < iterations; i++)
            {
                RunDelegate(i == 0);
                RunTuple(i == 0);
            }

            File.AppendAllText("log.txt", "Delegate avg: " + (sumDelegate / (iterations - 1)) + Environment.NewLine);
            File.AppendAllText("log.txt", "Tuple avg: " + (sumTuple / (iterations - 1)) + Environment.NewLine);
        }

        private static void RunDelegate(bool firstRun)
        {
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

            stopwatch.Restart();

            ParallelRunner.Run(DelegateCalc, arrayDelegate.Length);

            if (!firstRun)
            {
                sumDelegate += stopwatch.ElapsedTicks;
            }
            File.AppendAllText("log.txt", "Delegate: " + stopwatch.ElapsedTicks + Environment.NewLine);
        }

        private static void RunTuple(bool firstRun)
        {
            void TupleCalc(Tuple<int, int> range)
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    arrayTuple[i] = Math.Pow(i, 2);
                }
            }

            stopwatch.Restart();

            ParallelRunner.Run(TupleCalc, arrayTuple.Length);

            if (!firstRun)
            {
                sumTuple += stopwatch.ElapsedTicks;
            }
            File.AppendAllText("log.txt", "Tuple: " + stopwatch.ElapsedTicks + Environment.NewLine);
        }

        private static readonly Stopwatch stopwatch = new Stopwatch();
        private static readonly double[] arrayDelegate = new double[1000000];
        private static readonly double[] arrayTuple = new double[1000000];
        private static double sumDelegate = 0;
        private static double sumTuple = 0;
    }
}
