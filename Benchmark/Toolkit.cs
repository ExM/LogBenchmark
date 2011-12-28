using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Benchmark
{
	public static class Toolkit
	{
		public static List<Point> CreateHistogram(Action<int> action, int repeats, int sums)
		{
			SortedList<double, int> hist = new SortedList<double, int>();
			
			for(int i = 0; i < repeats; i++)
			{
				action(0);
				long start = Stopwatch.GetTimestamp();
				for(int j = 0; j < sums; j++)
					action(j);
				long t = Stopwatch.GetTimestamp() - start;
				double m = (double)t / sums / Stopwatch.Frequency;
				
				int ex;
				if(hist.TryGetValue(m, out ex))
					hist[m] = ex + 1;
				else
					hist.Add(m, 1);
			}

			List<Point> result = new List<Point>();

			foreach (var p in hist)
				result.Add(new Point() { Time = p.Key, Value = (double)p.Value / repeats });

			return result;
		}

		public static void Calc(string platform, string name, Action<int> f)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			double scale = 1000000000d; // ns

			int total = 10000;
			int sums = 10000;

			var hist = Toolkit.CreateHistogram(f, total, sums);

			File.WriteAllLines(
				string.Format("{0}_{1}.dat", platform, name),
				hist.Select(p => string.Format(CultureInfo.InvariantCulture, "{0}\t{1}", p.Time * scale, p.Value)).ToArray());

			Measure m = Measure.Create(hist, 0.95d);

			Console.WriteLine("{0}\t{1:G4} ns (-{2:G4}|+{3:G4}) F:{4:G4}", name,
				m.Average * scale,
				m.LDev * scale,
				m.RDev * scale,
				m.Fullness);
		}
	}
}

