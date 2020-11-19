using System;

using Benchmarking;

namespace LoopsPerformanceComparison
{
    class Program
    {
        static void Main()
        {
            var implementations = new IBenchmarkable[] { new SimpleForLoop(), new ForeachLoop() };
            foreach (IBenchmarkable implementation in implementations)
            {
                Console.WriteLine($"\nLeistung der Implementierung '{implementation.Name}':");

                var benchmarker = new Benchmarking.Benchmarker();
                var performanceMeasurements = benchmarker.Measure(implementation);

                foreach (var measurement in performanceMeasurements)
                {
                    Console.WriteLine(measurement.Explain("Durchlauf", "Durchläufe"));
                }
            }
        }
    }
}
