using System;
using System.Diagnostics;
using log4net;
using log4net.Config;
using System.IO;
using System.Runtime.CompilerServices;

namespace Log4Net.mono
{
	class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

		static void Main(string[] args)
		{
			XmlConfigurator.Configure(new FileInfo("log4net.config"));

			Log.Info("Check write");

			Stopwatch sw = null;

			int N = 10000000;
			
			double scale = 1000000000d / N; // ns
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				NCh_P0(i);
			Log.InfoFormat("NCh_P0: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				NCh_P1(i);
			Log.InfoFormat("NCh_P1: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				NCh_P3(i);
			Log.InfoFormat("NCh_P3: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				PCh_P0(i);
			Log.InfoFormat("PCh_P0: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				PCh_P1(i);
			Log.InfoFormat("PCh_P1: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				PCh_P3(i);
			Log.InfoFormat("PCh_P3: {0:###} ns", sw.Elapsed.TotalSeconds * scale);
			
			sw = Stopwatch.StartNew();
			for(int i = 0; i<N; i++)
				None(i);
			Log.InfoFormat("None:   {0:###} ns", sw.Elapsed.TotalSeconds * scale);
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
