using System;
using System.IO;
using log4net;
using log4net.Config;
using Benchmark;

namespace LogBenchmark
{
	class Program
	{
		static void Main(string[] args)
		{
			string dir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			XmlConfigurator.Configure(new FileInfo(Path.Combine(dir, "log4net.config")));

			Log4NetUse.CheckWrite();
			NLogUse.CheckWrite();

			Calc("None",    "Em_1",   None,              200000, 10000, 1);

			Calc("Log4Net", "N0_1",   Log4NetUse.NCh_P0, 200000, 1000,  1);
			Calc("Log4Net", "N0_100", Log4NetUse.NCh_P0, 2000,   1000,  100);
			Calc("Log4Net", "N1_1",   Log4NetUse.NCh_P1, 200000, 1000,  1);
			Calc("Log4Net", "N3_1",   Log4NetUse.NCh_P3, 200000, 800,   1);
			Calc("Log4Net", "P0_1",   Log4NetUse.PCh_P0, 200000, 1200,  1);

			Calc("NLog",    "N0_1",   NLogUse.NCh_P0,    200000, 5000,  1);
			Calc("NLog",    "N0_100", NLogUse.NCh_P0,    2000,   5000,  100);
			Calc("NLog",    "N1_1",   NLogUse.NCh_P1,    200000, 5000,  1);
			Calc("NLog",    "N3_1",   NLogUse.NCh_P3,    200000, 5000,  1);
			Calc("NLog",    "P0_1",   NLogUse.PCh_P0,    200000, 10000, 1);
		}

		static void Calc(string logger, string testName, Action<int> f, int total, int sums, int threads)
		{
			Toolkit.Calc(logger + "_dotNet", testName, f, total, sums, threads, 1e+9, "ns");
		}

		static void None(int a1)
		{
		}
	}
}
