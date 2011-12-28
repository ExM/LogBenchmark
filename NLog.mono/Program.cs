using System;
using NLog;

namespace Benchmark
{
	class Program
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			Log.Info("Check write");

			Calc("N0", NCh_P0);
			Calc("N1", NCh_P1);
			Calc("N3", NCh_P3);
			Calc("P0", PCh_P0);
			Calc("P1", PCh_P1);
			Calc("P3", PCh_P3);
			Calc("Em", None);
		}

		static void Calc(string name, Action<int> f)
		{
			Toolkit.Calc("NLog_mono", name, f);
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
