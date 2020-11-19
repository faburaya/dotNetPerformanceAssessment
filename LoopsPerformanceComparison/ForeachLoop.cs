using System;

using Benchmarking;

namespace LoopsPerformanceComparison
{
    /// <summary>
    /// Ermöglicht die Messung der Leistung einer foreach-Schleife.
    /// </summary>
    class ForeachLoop : IBenchmarkable
    {
        public string Name { get { return "foreach-Schleife"; } }

        public void Run(uint levelOfLoad)
        {
            var data = new int[levelOfLoad];
            int count = 0;
            foreach (int x in data)
            {
                if (x == 0)
                {
                    ++count;
                }
            }
        }
    }
}
