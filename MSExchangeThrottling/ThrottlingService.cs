using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ThrottlingService;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000006 RID: 6
	internal sealed class ThrottlingService : ExServiceBase
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002C62 File Offset: 0x00000E62
		public ThrottlingService()
		{
			base.ServiceName = "MSExchangeThrottling";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C8A File Offset: 0x00000E8A
		public static Trace Tracer
		{
			get
			{
				return ThrottlingService.tracer;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002C91 File Offset: 0x00000E91
		public static ExEventLog EventLogger
		{
			get
			{
				return ThrottlingService.eventLogger;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C98 File Offset: 0x00000E98
		public static ThrottlingService.Breadcrumbs StartStopBreadcrumbs
		{
			get
			{
				return ThrottlingService.startStopBreadcrumbs;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public static void Main(string[] args)
		{
			ThrottlingService.StartStopBreadcrumbs.Drop("Execution began", new object[0]);
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege"
			});
			if (num != 0)
			{
				ThrottlingService.StartStopBreadcrumbs.Drop("Failed to remove excessive privileges; Win32 error: {0}", new object[]
				{
					num
				});
				ThrottlingService.tracer.TraceError<int>(0L, "Failed to remove excessive privileges; Win32 error: {0}", num);
				ThrottlingService.eventLogger.LogEvent(ThrottlingServiceEventLogConstants.Tuple_RemovePrivilegesFailure, null, new object[]
				{
					num
				});
				Environment.Exit(num);
			}
			ExWatson.Register();
			ThrottlingService.runningAsService = !Environment.UserInteractive;
			Globals.InitializeSinglePerfCounterInstance();
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					ThrottlingService.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console"))
				{
					flag = true;
				}
				else if (text.StartsWith("-wait"))
				{
					flag2 = true;
				}
			}
			ThrottlingService.StartStopBreadcrumbs.Drop("runningAsService={0}", new object[]
			{
				ThrottlingService.runningAsService
			});
			if (!ThrottlingService.runningAsService)
			{
				if (!flag)
				{
					ThrottlingService.Usage();
					ThrottlingService.StartStopBreadcrumbs.Drop("Usage shown", new object[0]);
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					ThrottlingService.StartStopBreadcrumbs.Drop("Waiting for user input to proceed with initialization", new object[0]);
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			ThrottlingService.instance = new ThrottlingService();
			if (!ThrottlingService.runningAsService)
			{
				ExServiceBase.RunAsConsole(ThrottlingService.instance);
			}
			else
			{
				ServiceBase.Run(ThrottlingService.instance);
			}
			ThrottlingService.StartStopBreadcrumbs.Drop("Execution ended", new object[0]);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E90 File Offset: 0x00001090
		protected override void OnStartInternal(string[] args)
		{
			TimeSpan startupTimeout = TimeSpan.FromMinutes(6.0);
			base.ExRequestAdditionalTime((int)startupTimeout.TotalMilliseconds);
			ThrottlingService.StartStopBreadcrumbs.Drop("Starting Throttling Service", new object[0]);
			ThrottlingService.tracer.TraceDebug(0L, "Starting Throttling Service");
			if (ThrottlingAppConfig.LoggingEnabled && ThrottlingAppConfig.LogPath != null)
			{
				ThrottlingServiceLog.Start();
			}
			if (!ThrottlingRpcServerImpl.TryCreateInstance(startupTimeout, out this.rpcServer))
			{
				ThrottlingService.StartStopBreadcrumbs.Drop("Failed to start Throttling Service, exiting", new object[0]);
				Environment.Exit(1);
			}
			ThrottlingService.tracer.TraceDebug(0L, "Throttling Service started successfully");
			ThrottlingService.StartStopBreadcrumbs.Drop("Throttling Service started successfully", new object[0]);
			ThrottlingServiceLog.LogServiceStart();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F48 File Offset: 0x00001148
		protected override void OnStopInternal()
		{
			ThrottlingService.StartStopBreadcrumbs.Drop("Stopping Throttling Service", new object[0]);
			ThrottlingService.tracer.TraceDebug(0L, "Stopping Throttling Service");
			ThrottlingServiceLog.Stop();
			if (this.rpcServer != null)
			{
				this.rpcServer.Stop();
				this.rpcServer = null;
			}
			ThrottlingService.tracer.TraceDebug(0L, "Throttling Service stopped successfully");
			ThrottlingService.StartStopBreadcrumbs.Drop("Throttling Service stopped successfully", new object[0]);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FC0 File Offset: 0x000011C0
		protected override void OnCustomCommandInternal(int command)
		{
			if (command != 202)
			{
				return;
			}
			if (this.rpcServer != null)
			{
				this.rpcServer.ExportData();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FEB File Offset: 0x000011EB
		private static void Usage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x04000019 RID: 25
		public const string ThrottlingServiceName = "MSExchangeThrottling";

		// Token: 0x0400001A RID: 26
		public const string ThrottlingServiceDisplayName = "Microsoft Exchange Throttling Service";

		// Token: 0x0400001B RID: 27
		public const string ThrottlingServiceDescription = "Microsoft Exchange Throttling Service";

		// Token: 0x0400001C RID: 28
		private const string HelpOption = "-?";

		// Token: 0x0400001D RID: 29
		private const string ConsoleOption = "-console";

		// Token: 0x0400001E RID: 30
		private const string WaitToContinueOption = "-wait";

		// Token: 0x0400001F RID: 31
		private const int ExportData = 202;

		// Token: 0x04000020 RID: 32
		private static ThrottlingService instance;

		// Token: 0x04000021 RID: 33
		private static bool runningAsService;

		// Token: 0x04000022 RID: 34
		private static Trace tracer = ExTraceGlobals.ThrottlingServiceTracer;

		// Token: 0x04000023 RID: 35
		private static ExEventLog eventLogger = new ExEventLog(ThrottlingService.tracer.Category, "MSExchangeThrottling");

		// Token: 0x04000024 RID: 36
		private static ThrottlingService.Breadcrumbs startStopBreadcrumbs = new ThrottlingService.Breadcrumbs();

		// Token: 0x04000025 RID: 37
		private ThrottlingRpcServerImpl rpcServer;

		// Token: 0x02000007 RID: 7
		public sealed class Breadcrumbs
		{
			// Token: 0x06000035 RID: 53 RVA: 0x0000303A File Offset: 0x0000123A
			public Breadcrumbs()
			{
				this.data = new List<string>();
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003050 File Offset: 0x00001250
			public void Drop(string format, params object[] args)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(DateTime.UtcNow.ToString("G"));
				stringBuilder.Append(": ");
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, args);
				string item = stringBuilder.ToString();
				lock (this.data)
				{
					if (this.data.Count >= 10000)
					{
						throw new InvalidOperationException("Too many breadcrumbs");
					}
					this.data.Add(item);
				}
			}

			// Token: 0x04000026 RID: 38
			private const int MaxRecordCount = 10000;

			// Token: 0x04000027 RID: 39
			private List<string> data;
		}
	}
}
