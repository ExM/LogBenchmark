using System;
using System.Collections.Generic;

namespace Benchmark
{
	public class Measure
	{
		public double Average { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		
		public double LDev
		{
			get
			{
				return Average - Min;
			}
		}
		
		public double RDev
		{
			get
			{
				return Max - Average;
			}
		}
		
		public Measure()
		{
		}
		
		public static Measure Create(SortedList<double, int> hist, int total, double precision)
		{
			Measure m = new Measure();
			
			double threshold = total * precision;
			int sum = 0;
			double average = 0;
			foreach(var pair in hist)
			{
				if(sum == 0) // min value
					m.Min = pair.Key;
				sum += pair.Value;
				average += pair.Value * pair.Key;
				
				if(sum > threshold)
				{
					m.Max = pair.Key; // last value
					break;
				}
			}
			
			m.Average = average / sum;
			
			return m;
		}
	}
}

