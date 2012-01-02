using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace LogBenchmark
{
	public static class NLogUse
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		internal static void CheckWrite()
		{
			Log.Info("Check write");
		}

		internal static void NCh_P0(int a1)
		{
			Log.Debug("Test message");
		}

		internal static void NCh_P1(int a1)
		{
			Log.Debug("Test message: {0}", a1);
		}

		internal static void NCh_P3(int a1)
		{
			Log.Debug("Test message: {0} {1} {2}", a1, a1, a1);
		}

		internal static void PCh_P0(int a1)
		{
			if (Log.IsDebugEnabled)
				Log.Debug("Test message");
		}
	}
}
