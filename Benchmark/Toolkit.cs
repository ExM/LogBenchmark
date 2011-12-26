using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Benchmark
{
	public static class Toolkit
	{
		public static SortedList<double, int> CreateHistogram(Action<int> action, int repeats, int sums)
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
			
			return hist;
		}
	}
}

