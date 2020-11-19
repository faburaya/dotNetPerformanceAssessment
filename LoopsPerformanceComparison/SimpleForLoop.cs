using System;

using Benchmarking;

namespace LoopsPerformanceComparison
{
    /// <summary>
    /// Ermöglicht die Messung der Leistung einer einfachen for-Schleife.
    /// </summary>
    class SimpleForLoop : IBenchmarkable
    {
        public string Name { get { return "Einfach for-Schleife"; } }

        public void Run(uint levelOfLoad)
        {
            var data = new int[levelOfLoad];
            int count = 0;
            for (int idx = 0; idx < data.Length; ++idx)
            {
                if (data[idx] == 0)
                {
                    ++count;
                }
            }
        }
    }
}
