using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PopImap.CoreService
{
	// Token: 0x02000002 RID: 2
	internal abstract class CoreService : ProcessManagerService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CoreService(string serviceName, string workerProcessPathName, string jobObjectName, bool runningAsService, Microsoft.Exchange.Diagnostics.Trace tracer, ExEventLog eventLogger) : base(serviceName, workerProcessPathName, jobObjectName, false, 30, runningAsService, tracer, eventLogger)
		{
			base.CanShutdown = true;
			base.CanStop = true;
			try
			{
				string text = ConfigurationManager.AppSettings["MaxConnectionRatePerMinute"];
				int maxConnectionRate;
				if (text != null && int.TryParse(text, out maxConnectionRate))
				{
					base.MaxConnectionRate = maxConnectionRate;
				}
			}
			catch (ConfigurationException ex)
			{
				tracer.TraceError<ConfigurationException>(0L, "Corrupt config file: {0}", ex);
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_AppConfigLoadFailed, null, new object[]
				{
					ex.Message
				});
				ProcessManagerService.StopService();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002177 File Offset: 0x00000377
		public static string ShortServiceName
		{
			get
			{
				return CoreService.shortServiceName;
			}
			set
			{
				CoreService.shortServiceName = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002186 File Offset: 0x00000386
		public static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				return CoreService.troubleshootingContext;
			}
			set
			{
				CoreService.troubleshootingContext = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000218E File Offset: 0x0000038E
		public static ServerServiceRole ServerRoleService
		{
			get
			{
				return CoreService.serverRoleService;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002195 File Offset: 0x00000395
		public override bool CanHandleConnectionsIfPassive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public override int MaxWorkerProcessExitTimeoutDefault
		{
			get
			{
				return 180;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
		public static void SendWatsonForUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				CoreService.SendWatson(ex);
				return;
			}
			ExWatson.HandleException(sender, e);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CA File Offset: 0x000003CA
		public static void InMemoryTraceOperationCompleted()
		{
			CoreService.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D6 File Offset: 0x000003D6
		public static void SendWatson(Exception exception)
		{
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				CoreService.TroubleshootingContext.SendExceptionReportWithTraces(exception, true, true);
				return;
			}
			if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
			{
				ExWatson.SendReport(exception);
			}
		}

		// Token: 0x0600000C RID: 12
		public abstract PopImapAdConfiguration GetConfiguration();

		// Token: 0x0600000D RID: 13 RVA: 0x00002200 File Offset: 0x00000400
		protected static string GetWorkerProcessPathName(string imageFileName)
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string fileName = currentProcess.MainModule.FileName;
				string directoryName = Path.GetDirectoryName(fileName);
				result = directoryName + "\\" + imageFileName;
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002250 File Offset: 0x00000450
		protected static void ParseArgs(string[] args, out bool runningAsService)
		{
			bool flag = false;
			bool flag2 = false;
			runningAsService = !Environment.UserInteractive;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.OrdinalIgnoreCase))
				{
					CoreService.Usage();
					Environment.Exit(0);
				}
				if (text.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				if (text.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				if (text.StartsWith("FrontEnd", StringComparison.OrdinalIgnoreCase) && CoreService.serverRoleService == ServerServiceRole.unknown)
				{
					CoreService.serverRoleService = ServerServiceRole.cafe;
				}
				if (text.StartsWith("ClientAccess", StringComparison.OrdinalIgnoreCase) && CoreService.serverRoleService == ServerServiceRole.unknown)
				{
					CoreService.serverRoleService = ServerServiceRole.mailbox;
				}
			}
			if (!runningAsService)
			{
				if (!flag)
				{
					CoreService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002334 File Offset: 0x00000534
		protected static void MainProc(CoreService serviceToRun, bool runningAsService, string[] args)
		{
			if (!serviceToRun.Initialize())
			{
				ExTraceGlobals.ServiceTracer.TraceError(0L, "Failed to initialize the service. Exiting.");
				ProcessManagerService.StopService();
			}
			Globals.InitializeSinglePerfCounterInstance();
			ExTraceGlobals.ServiceTracer.TraceError(0L, "CoreService::MainProc - Initialized Single perf counter instance.");
			ResourceLoadDelayInfo.Initialize();
			ExTraceGlobals.ServiceTracer.TraceError(0L, "CoreService::MainProc - Initialized Resource load delay info.");
			if (!runningAsService)
			{
				serviceToRun.OnStartInternal(args);
				bool flag = false;
				while (!flag)
				{
					Console.WriteLine("Enter 'q' to shutdown.");
					string text = Console.ReadLine();
					if (string.IsNullOrEmpty(text))
					{
						break;
					}
					switch (text[0])
					{
					case 'q':
						flag = true;
						break;
					case 'r':
						serviceToRun.OnCustomCommandInternal(200);
						break;
					case 'u':
						serviceToRun.OnCustomCommandInternal(201);
						break;
					}
				}
				Console.WriteLine("Shutting down ...");
				serviceToRun.OnStopInternal();
				Console.WriteLine("Done.");
				return;
			}
			ServiceBase.Run(serviceToRun);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000241C File Offset: 0x0000061C
		protected static void DetermineServiceRole()
		{
			string fileName;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				fileName = currentProcess.MainModule.FileName;
			}
			string directoryName = Path.GetDirectoryName(fileName);
			string directoryName2 = Path.GetDirectoryName(directoryName);
			int num = directoryName2.LastIndexOf("FrontEnd", StringComparison.OrdinalIgnoreCase);
			int num2 = directoryName2.LastIndexOf("ClientAccess", StringComparison.OrdinalIgnoreCase);
			if (num == directoryName2.Length - "FrontEnd".Length)
			{
				CoreService.serverRoleService = ServerServiceRole.cafe;
				return;
			}
			if (num2 == directoryName2.Length - "ClientAccess".Length)
			{
				CoreService.serverRoleService = ServerServiceRole.mailbox;
				return;
			}
			throw new FileLoadException("Installation error: Pop Imap executables not found under ClientAccess or FrontEnd subdirectory: {0}", fileName);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024C4 File Offset: 0x000006C4
		protected override bool GetBindings(out IPEndPoint[] bindings)
		{
			PopImapAdConfiguration configuration = this.GetConfiguration();
			if (configuration == null)
			{
				bindings = null;
				return false;
			}
			if (CoreService.ServerRoleService == ServerServiceRole.mailbox)
			{
				bindings = new IPEndPoint[]
				{
					new IPEndPoint(IPAddress.Any, configuration.ProxyTargetPort),
					new IPEndPoint(IPAddress.IPv6Any, configuration.ProxyTargetPort)
				};
			}
			else
			{
				bindings = configuration.GetBindings().ToArray();
			}
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002528 File Offset: 0x00000728
		private static void Usage()
		{
			Console.WriteLine(CoreServiceStrings.UsageText, Assembly.GetExecutingAssembly().GetName().Name, CoreService.ShortServiceName);
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string ConsoleMode = "-console";

		// Token: 0x04000003 RID: 3
		private const string WaitToContinue = "-wait";

		// Token: 0x04000004 RID: 4
		private const string ServerRoleCafe = "FrontEnd";

		// Token: 0x04000005 RID: 5
		private const string ServerRoleMbx = "ClientAccess";

		// Token: 0x04000006 RID: 6
		private const int WorkerProcessExitTimeoutDefault = 30;

		// Token: 0x04000007 RID: 7
		private static string shortServiceName;

		// Token: 0x04000008 RID: 8
		private static TroubleshootingContext troubleshootingContext;

		// Token: 0x04000009 RID: 9
		private static ServerServiceRole serverRoleService = ServerServiceRole.unknown;
	}
}
