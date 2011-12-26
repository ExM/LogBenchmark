using System;
using System.Diagnostics;
using Benchmark;

namespace NLog.mono
{
	class Program
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			Log.Info("Check write");

			Calc("NCh_P0", NCh_P0);
			Calc("NCh_P1", NCh_P1);
			Calc("NCh_P3", NCh_P3);
			Calc("PCh_P0", PCh_P0);
			Calc("PCh_P1", PCh_P1);
			Calc("PCh_P3", PCh_P3);
			Calc("None  ", None);
		}
		
		static void Calc(string name, Action<int> f)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			
			double scale = 1000000000d; // ns
			
			int total = 10000;
			int sums = 1000;
			
			var hist = Toolkit.CreateHistogram(f, total, sums);
			Measure m = Measure.Create(hist, total, 0.95d);
			
			Console.WriteLine("{0}: {1:G4} ns (-{2:G4}|+{3:G4})", name,
				m.Average * scale,
				m.LDev * scale,
				m.RDev * scale);
		}
		
		static void None(int a1)
		{
		}

		static void NCh_P0(int a1)
		{
			Log.Debug("Test message");
		}
		
		static void NCh_P1(int a1)
		{
			Log.Debug("Test message: {0}", a1);
		}
		
		static void NCh_P3(int a1)
		{
			Log.Debug("Test message: {0} {1} {2}", a1, a1, a1);
		}
		
		static void PCh_P0(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.Debug("Test message");
		}
		
		static void PCh_P1(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.Debug("Test message: {0}", a1);
		}
		
		static void PCh_P3(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.Debug("Test message: {0} {1} {2}", a1, a1, a1);
		}
	}
}
