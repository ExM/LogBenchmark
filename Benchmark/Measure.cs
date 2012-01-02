using System;
using System.Collections.Generic;

namespace Benchmark
{
	public class Measure
	{
		public double Average { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public long MinTime { get; set; }

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

		public static Measure Create(List<Point> hist, double precision)
		{
			Measure m = new Measure();

			double fullness = 0;
			double average = 0;
			foreach(var p in hist)
			{
				if (fullness == 0) // min value
					m.Min = p.Time;
				fullness += p.Value;
				average += p.Value * p.Time;

				if (fullness > precision)
				{
					m.Max = p.Time; // last value
					break;
				}
			}

			m.Average = average / fullness;
			return m;
		}

		public string ToString(double scale, string unit)
		{
			return string.Format("{0:G4} {1} (-{2:G4}|+{3:G4}) min time:{4} tics",
				Average * scale,
				unit,
				LDev * scale,
				RDev * scale,
				MinTime);
		}
	}
}

