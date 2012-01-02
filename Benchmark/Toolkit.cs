using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace Benchmark
{
	public static class Toolkit
	{
		public static SortedList<long, int> CreateHistogram(Action<int> action, int repeats, int sums)
		{
			SortedList<long, int> hist = new SortedList<long, int>();
			
			for(int i = 0; i < repeats; i++)
			{
				action(0);
				long start = Stopwatch.GetTimestamp();
				for(int j = 0; j < sums; j++)
					action(j);
				long t = Stopwatch.GetTimestamp() - start;
				
				
				int ex;
				if (hist.TryGetValue(t, out ex))
					hist[t] = ex + 1;
				else
					hist.Add(t, 1);
			}

			return hist;
		}

		public static List<Point> Aggregate(SortedList<long, int>[] hists, int repeats, int sums, out long minTime)
		{
			SortedList<long, int> sumHist = new SortedList<long, int>();

			foreach (var hist in hists)
				foreach (var pair in hist)
				{
					int ex;
					if (sumHist.TryGetValue(pair.Key, out ex))
						sumHist[pair.Key] = ex + pair.Value;
					else
						sumHist.Add(pair.Key, pair.Value);
				}

			minTime = sumHist.First().Key;

			return Normalize(sumHist, repeats * hists.Length, sums);
		}

		public static List<Point> Normalize(SortedList<long, int> hist, int repeats, int sums)
		{
			List<Point> result = new List<Point>();

			foreach (var p in hist)
				result.Add(new Point() { Time = (double)p.Key / sums / Stopwatch.Frequency, Value = (double)p.Value / repeats });

			return result;
		}

		public static void Calc(string platform, string testName, Action<int> f, int total, int sums, int threads, double scale, string unit)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();

			Thread[] ths = new Thread[threads];
			SortedList<long, int>[] preHists = new SortedList<long,int>[threads];

			for (int i = 0; i < threads; i++)
			{
				int index = i;
				ths[i] = new Thread(o =>
				{
					preHists[index] = Toolkit.CreateHistogram(f, total, sums);
				});
			}

			for (int i = 0; i < threads; i++)
				ths[i].Start();

			for (int i = 0; i < threads; i++)
				ths[i].Join();

			long minTime;
			var hist = Aggregate(preHists, total, sums, out minTime);

			if (minTime == 0)
				Console.WriteLine("WARNING! measured time interval is too small. Necessary to increase the parameter 'sums' (surrent:{0})", sums);

			File.WriteAllLines(
				string.Format("{0}_{1}.dat", platform, testName),
				hist.Select(p => string.Format(CultureInfo.InvariantCulture, "{0}\t{1}", p.Time * scale, p.Value)).ToArray());

			Measure m = Measure.Create(hist, 0.95d);
			m.MinTime = minTime;

			Console.WriteLine("{0}\t{1}\t{2}", platform, testName, m.ToString(scale, unit));
		}
	}
}

