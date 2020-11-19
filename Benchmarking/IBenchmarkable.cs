using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking
{
    /// <summary>
    /// Eine Schnittstelle für das Messen der Leistung eines Stück Implementierung.
    /// </summary>
    public interface IBenchmarkable
    {
        string Name { get; }

        void Run(uint levelOfLoad);
    }
}
