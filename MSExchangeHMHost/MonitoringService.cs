using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using Microsoft.Exchange.ActiveMonitoring.EventLog;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Security;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.ActiveMonitoring
{
	// Token: 0x02000002 RID: 2
	internal class MonitoringService : ProcessManagerService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static MonitoringService()
		{
			MonitoringDiagnosticLogConfiguration configuration = new MonitoringDiagnosticLogConfiguration("MSExchangeHMHost");
			MonitoringService.monitoringLogger = new MonitoringLogger(configuration);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002164 File Offset: 0x00000364
		internal MonitoringService(bool runningAsService) : base("MSExchangeHM", MonitoringService.GetWorkerProcessPathName(MonitoringService.workerProcessName), MonitoringService.jobObjectName, true, 30, runningAsService, ExTraceGlobals.ServiceTracer, MonitoringService.serviceLogger)
		{
			base.CanPauseAndContinue = true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021A0 File Offset: 0x000003A0
		public override bool CanHandleConnectionsIfPassive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021A3 File Offset: 0x000003A3
		protected override TimeSpan StartTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(30.0);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static void Main(string[] args)
		{
			MonitoringService.LogDiagnosticInfo("Registering for Watson.", new object[0]);
			ExWatson.Register();
			bool flag = !Environment.UserInteractive;
			bool flag2 = false;
			bool flag3 = false;
			MonitoringService.LogDiagnosticInfo("Parsing command line args.", new object[0]);
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.OrdinalIgnoreCase))
				{
					MonitoringService.LogDiagnosticInfo("Printing usage and exiting.", new object[0]);
					MonitoringService.PrintUsage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
				{
					MonitoringService.LogDiagnosticInfo("Running from console.", new object[0]);
					flag2 = true;
				}
				else if (text.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag3 = true;
				}
				else if (text.StartsWith("-ForceConsole", StringComparison.OrdinalIgnoreCase))
				{
					MonitoringService.LogDiagnosticInfo("Force running from console.", new object[0]);
					flag = false;
					flag2 = true;
				}
			}
			if (!flag)
			{
				if (!flag2)
				{
					MonitoringService.LogDiagnosticInfo("Printing usage and exiting.", new object[0]);
					MonitoringService.PrintUsage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag3)
				{
					MonitoringService.LogDiagnosticInfo("Waiting for user input before continuing.", new object[0]);
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			if (!ServiceTopologyProvider.IsAdTopologyServiceInstalled())
			{
				MonitoringService.LogDiagnosticInfo("Can't use AD Topology service; setting admin mode instead.", new object[0]);
				ADSession.SetAdminTopologyMode();
			}
			MonitoringService.LogDiagnosticInfo("Initializing perf counters.", new object[0]);
			Globals.InitializeMultiPerfCounterInstance("MSExchangeHM");
			MonitoringService monitoringService = new MonitoringService(flag);
			if (!monitoringService.Initialize())
			{
				ExTraceGlobals.ServiceTracer.TraceError(0L, "Failed to initialize the service. Exiting.");
				MonitoringService.LogDiagnosticInfo("Initialization of the service failed. Stopping service and exiting.", new object[0]);
				ProcessManagerService.StopService();
			}
			if (!flag)
			{
				monitoringService.OnStartInternal(args);
				bool flag4 = false;
				while (!flag4)
				{
					Console.WriteLine("Enter 'q' to shutdown.");
					string text2 = Console.ReadLine();
					if (string.IsNullOrEmpty(text2))
					{
						break;
					}
					switch (text2[0])
					{
					case 'q':
						flag4 = true;
						break;
					case 'r':
						monitoringService.OnCustomCommandInternal(200);
						break;
					case 'u':
						monitoringService.OnCustomCommandInternal(201);
						break;
					}
				}
				Console.WriteLine("Shutting down ...");
				monitoringService.OnStopInternal();
				Console.WriteLine("Done.");
				return;
			}
			ServiceBase.Run(monitoringService);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002400 File Offset: 0x00000600
		protected override void OnStartInternal(string[] args)
		{
			MonitoringService.LogDiagnosticInfo("Starting the Active Monitoring RPC server", new object[0]);
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Starting RPC server");
			bool flag = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-NoWorker", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
			}
			try
			{
				MonitoringService.ConfigureRpc();
				ActiveMonitoringRpcServer.Start(new ActiveMonitoringRpcServer.DiagnosticLogger(MonitoringService.LogDiagnosticInfo));
				this.rpcServerStarted = true;
				MonitoringService.LogDiagnosticInfo("Started the Active Monitoring RPC server successfully", new object[0]);
			}
			catch (Exception ex)
			{
				MonitoringService.LogDiagnosticInfo("Active monitoring RPC server fails to start: {0}", new object[]
				{
					ex.Message
				});
				ExTraceGlobals.ServiceTracer.TraceError(0L, string.Format("Active monitoring RPC server fails to start: {0}", ex.Message));
				MonitoringService.logger.LogEvent(MSExchangeHMEventLogConstants.Tuple_RpcServerFailedToStart, null, new object[]
				{
					ex.Message
				});
			}
			if (!flag)
			{
				base.OnStartInternal(args);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002504 File Offset: 0x00000704
		protected override bool Initialize()
		{
			try
			{
				MonitoringService.LogDiagnosticInfo("Flushing Kerberos ticket cache.", new object[0]);
				Kerberos.FlushTicketCache();
			}
			catch (Win32Exception ex)
			{
				MonitoringService.LogDiagnosticInfo("Caught Win32Exception: {0}", new object[]
				{
					ex.ToString()
				});
				if (ex.ErrorCode == -2146232828)
				{
					return false;
				}
				throw;
			}
			return base.Initialize();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002570 File Offset: 0x00000770
		protected override void OnStopInternal()
		{
			base.OnStopInternal();
			if (this.rpcServerStarted)
			{
				try
				{
					ActiveMonitoringRpcServer.Stop();
				}
				catch (Exception ex)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug(0L, string.Format("Trying to stop RPC server failed with the following exception: {0}", ex.Message));
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025C4 File Offset: 0x000007C4
		protected override bool GetBindings(out IPEndPoint[] bindings)
		{
			bindings = null;
			return true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025CA File Offset: 0x000007CA
		private static void PrintUsage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000025EC File Offset: 0x000007EC
		private static string GetWorkerProcessPathName(string imageFileName)
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string fileName = currentProcess.MainModule.FileName;
				string directoryName = Path.GetDirectoryName(fileName);
				result = Path.Combine(directoryName, imageFileName);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002638 File Offset: 0x00000838
		private static void LogDiagnosticInfo(string message, params object[] messageArgs)
		{
			MonitoringService.monitoringLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000264C File Offset: 0x0000084C
		private static void ConfigureRpc()
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string path = Path.Combine(directoryName, MonitoringService.workerProcessName);
			Assembly assembly = Assembly.LoadFile(path);
			Type type = assembly.GetType(MonitoringService.rpcConfigurationType);
			if (type == null)
			{
				string message = string.Format("Unable to load class {0}. Reason: Assembly.GetType() returned null when getting the class type.", MonitoringService.rpcConfigurationType);
				throw new InvalidOperationException(message);
			}
			RpcConfiguration rpcConfiguration = (RpcConfiguration)Activator.CreateInstance(type, new object[0]);
			rpcConfiguration.Initialize();
		}

		// Token: 0x04000001 RID: 1
		internal const string MonitoringServiceName = "MSExchangeHM";

		// Token: 0x04000002 RID: 2
		private const string HelpOption = "-?";

		// Token: 0x04000003 RID: 3
		private const string ConsoleOption = "-console";

		// Token: 0x04000004 RID: 4
		private const string ForceConsoleOption = "-ForceConsole";

		// Token: 0x04000005 RID: 5
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000006 RID: 6
		private const string NoWorkerOption = "-NoWorker";

		// Token: 0x04000007 RID: 7
		private const int WorkerProcessExitTimeoutDefault = 30;

		// Token: 0x04000008 RID: 8
		private static string workerProcessName = ConfigurationManager.AppSettings["MonitoringServiceName"];

		// Token: 0x04000009 RID: 9
		private static string rpcConfigurationType = ConfigurationManager.AppSettings["RpcConfigurationClassType"];

		// Token: 0x0400000A RID: 10
		private static string jobObjectName = "Microsoft Exchange Health Manager";

		// Token: 0x0400000B RID: 11
		private static string eventSourceName = "MSExchangeHMHost";

		// Token: 0x0400000C RID: 12
		private static ExEventLog serviceLogger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, MonitoringService.eventSourceName);

		// Token: 0x0400000D RID: 13
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeHM");

		// Token: 0x0400000E RID: 14
		private static MonitoringLogger monitoringLogger;

		// Token: 0x0400000F RID: 15
		private bool rpcServerStarted;
	}
}
