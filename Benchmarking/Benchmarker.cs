using System;
using System.Collections.Generic;

namespace Benchmarking
{
    /// <summary>
    /// Stellt eine Implementierung auf dem Prüfstand,
    /// um ihre Leistung festzustellen.
    /// </summary>
    public class Benchmarker
    {
        /// <summary>
        /// Beschreibt das Ergebnis einer Runde.
        /// </summary>
        public struct Result
        {
            public uint Load { get; }

            public double Milliseconds { get; }

            public double Speed { get; }

            public Result(uint load, TimeSpan elapsedTime)
            {
                this.Load = load;
                this.Milliseconds = elapsedTime.TotalMilliseconds;
                this.Speed = load / elapsedTime.TotalMilliseconds;
            }

            public string Explain(string nameOfLoadUnit, string pluralNameOfLoad)
            {
                return $"{Load,12} {(Load > 1 ? pluralNameOfLoad : nameOfLoadUnit)} dauerte {Milliseconds,9:F1} Millisekunden bei Tempo = {Speed:E2} ({nameOfLoadUnit} pro Sekunde)";
            }
        }

        /// <summary>
        /// Misst wie schnell eine vorgegebene Implementierung ist.
        /// </summary>
        /// <param name="implementation">Die zu messende Implementierung.</param>
        /// <returns>Eine Liste, die einige Messungen enthält.</returns>
        public List<Result> Measure(IBenchmarkable implementation)
        {
            uint maxLoad = (uint)Math.Pow(10, Math.Floor(Math.Log10(uint.MaxValue)));
            double loadPerSecond = 0.0;
            for (uint load = 1; load < maxLoad; load *= 10)
            {
                TimeSpan elapsed = Measure(implementation, load);
                if (elapsed.TotalMilliseconds >= 5)
                {
                    loadPerSecond = 1000 * load / elapsed.TotalMilliseconds;
                    break;
                }
            }

            if (loadPerSecond == 0.0)
            {
                throw new ApplicationException($"Die Implementierung von '{implementation.Name}' ist schlicht so schnell, dass die Messung von ihrem Tempo unmöglich ist.");
            }

            var targetedTimeLapses = new TimeSpan[] {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            };

            var measurements = new List<Result>(targetedTimeLapses.Length);
            foreach (TimeSpan timeLapse in targetedTimeLapses)
            {
                var load = (uint)(loadPerSecond * timeLapse.Seconds); // eingeschätzt
                TimeSpan elapsed = Measure(implementation, load);
                measurements.Add(new Result(load, elapsed));
            }

            return measurements;
        }

        /// <summary>
        /// Misst die Zeitspane, wenn eine Implementierung mit einer vorgegebenen Menge an Arbeit läuft.
        /// </summary>
        /// <param name="implementation">Die Implementierung, deren Leistung gemessen wird.</param>
        /// <param name="levelOfLoad">Die Menge an Arbeit zum Testen.</param>
        /// <returns>Die Zeitspane, bis die Aufgabe erledigt wird.</returns>
        private TimeSpan Measure(IBenchmarkable implementation, uint levelOfLoad)
        {
            DateTime start = System.DateTime.Now;
            implementation.Run(levelOfLoad);
            DateTime end = System.DateTime.Now;
            return end - start;
        }
    }
}
