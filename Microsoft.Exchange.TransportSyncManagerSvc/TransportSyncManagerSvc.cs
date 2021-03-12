using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000002 RID: 2
	internal class TransportSyncManagerSvc : ExServiceBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public TransportSyncManagerSvc()
		{
			base.ServiceName = TransportSyncManagerSvc.TransportSyncManagerSvcName;
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static void Main(string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			ExWatson.Init();
			AppDomain.CurrentDomain.UnhandledException += TransportSyncManagerSvc.MainUnhandledExceptionHandler;
			TransportSyncManagerSvc.runningAsService = !Environment.UserInteractive;
			foreach (string text in args)
			{
				if (text.StartsWith("-?"))
				{
					TransportSyncManagerSvc.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-wait"))
				{
					TransportSyncManagerSvc.waitToContinue = true;
				}
			}
			Globals.InitializeSinglePerfCounterInstance();
			if (TransportSyncManagerSvc.runningAsService)
			{
				ServiceBase.Run(new TransportSyncManagerSvc());
				return;
			}
			TransportSyncManagerSvc.RunConsole();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021C0 File Offset: 0x000003C0
		protected override void OnStartInternal(string[] args)
		{
			TransportSyncManagerSvc.LogEvent(TransportSyncManagerEventLogConstants.Tuple_ServiceStarted, null, new object[0]);
			if (!DataAccessLayer.Initialize())
			{
				if (TransportSyncManagerSvc.runningAsService)
				{
					base.Stop();
					return;
				}
				this.OnStopInternal();
				Environment.Exit(1);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021F4 File Offset: 0x000003F4
		protected override void OnStopInternal()
		{
			try
			{
				DataAccessLayer.Shutdown();
			}
			finally
			{
				TransportSyncManagerSvc.LogEvent(TransportSyncManagerEventLogConstants.Tuple_ServiceStopped, null, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000222C File Offset: 0x0000042C
		private static void MainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			int num = Interlocked.Exchange(ref TransportSyncManagerSvc.busyUnhandledException, 1);
			if (num == 1)
			{
				return;
			}
			Exception exception = (Exception)eventArgs.ExceptionObject;
			if (!DataAccessLayer.Initialized)
			{
				if (!TransportSyncManagerSvc.runningAsService)
				{
					Console.WriteLine("Exception before Initialize or after Shutdown.\n{0}", eventArgs.ExceptionObject);
				}
				ExWatson.SendReport(exception, ReportOptions.None, null);
			}
			else
			{
				DataAccessLayer.ReportWatson(null, exception);
			}
			ExWatson.HandleException(sender, eventArgs);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000228C File Offset: 0x0000048C
		private static void Usage()
		{
			Console.WriteLine("Usage: Microsoft.Exchange.TransportSyncManagerSvc.exe [-wait]");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002298 File Offset: 0x00000498
		private static void RunConsole()
		{
			Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
			if (TransportSyncManagerSvc.waitToContinue)
			{
				Console.WriteLine("Press ENTER to continue startup.");
				Console.ReadLine();
			}
			using (TransportSyncManagerSvc transportSyncManagerSvc = new TransportSyncManagerSvc())
			{
				transportSyncManagerSvc.OnStartInternal(null);
				string text;
				do
				{
					Console.WriteLine("Press ENTER to shutdown.");
					text = Console.ReadLine();
				}
				while (text.Length != 0);
				Console.WriteLine("Shutting down ...");
				transportSyncManagerSvc.OnStopInternal();
				Console.WriteLine("Done.");
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002330 File Offset: 0x00000530
		private static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			TransportSyncManagerSvc.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000003 RID: 3
		private const string UsageString = "Usage: Microsoft.Exchange.TransportSyncManagerSvc.exe [-wait]";

		// Token: 0x04000004 RID: 4
		public static readonly string TransportSyncManagerSvcName = "MSExchangeTransportSyncManagerSvc";

		// Token: 0x04000005 RID: 5
		private static readonly ExEventLog eventLogger = new ExEventLog(new Guid("{DF4B5565-53E9-4776-A824-185F22FB3CA6}"), "MSExchangeTransportSyncManager");

		// Token: 0x04000006 RID: 6
		private static bool runningAsService = true;

		// Token: 0x04000007 RID: 7
		private static bool waitToContinue = false;

		// Token: 0x04000008 RID: 8
		private static int busyUnhandledException = 0;
	}
}
