using System;

namespace LoopsPerformanceComparison
{
    class Program
    {
        static void Main()
        {
            Benchmarking.IBenchmarkable simpleForLoopImplementation = new SimpleForLoop();
            var benchmarker = new Benchmarking.Benchmarker();
            var performanceMeasurements = benchmarker.Measure(simpleForLoopImplementation);
            
            foreach (var measurement in performanceMeasurements)
            {
                Console.WriteLine(measurement.Explain("Durchlauf", "Durchläufe"));
            }
        }
    }
}
