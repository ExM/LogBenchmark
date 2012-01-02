using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LogBenchmark
{
	internal static class Log4NetUse
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Log4NetUse));

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
			Log.DebugFormat("Test message: {0}", a1);
		}

		internal static void NCh_P3(int a1)
		{
			Log.DebugFormat("Test message: {0} {1} {2}", a1, a1, a1);
		}

		internal static void PCh_P0(int a1)
		{
			if (Log.IsDebugEnabled)
				Log.Debug("Test message");
		}
	}
}
