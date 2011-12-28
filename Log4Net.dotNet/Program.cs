using System;
using System.IO;
using log4net;
using log4net.Config;
using System.Reflection;

namespace Benchmark
{
	class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

		static void Main(string[] args)
		{
			string dir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			XmlConfigurator.Configure(new FileInfo(Path.Combine(dir, "log4net.config")));

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
			Toolkit.Calc("Log4Net_dotNet", name, f);
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
			Log.DebugFormat("Test message: {0}", a1);
		}
		
		static void NCh_P3(int a1)
		{
			Log.DebugFormat("Test message: {0} {1} {2}", a1, a1, a1);
		}
		
		static void PCh_P0(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.Debug("Test message");
		}
		
		static void PCh_P1(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.DebugFormat("Test message: {0}", a1);
		}
		
		static void PCh_P3(int a1)
		{
			if(Log.IsDebugEnabled)
				Log.DebugFormat("Test message: {0} {1} {2}", a1, a1, a1);
		}
	}
}
