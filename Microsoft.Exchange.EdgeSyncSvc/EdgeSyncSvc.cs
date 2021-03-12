using System;
using System.Configuration;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000014 RID: 20
	internal class EdgeSyncSvc : ExServiceBase
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000088EB File Offset: 0x00006AEB
		public EdgeSyncSvc(bool consoleTracing)
		{
			base.ServiceName = EdgeSyncSvc.EdgeSyncSvcName;
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008913 File Offset: 0x00006B13
		public EdgeSyncSvc()
		{
			EdgeSyncSvc.edgeSync = new EdgeSync();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00008927 File Offset: 0x00006B27
		public static EdgeSync EdgeSync
		{
			get
			{
				return EdgeSyncSvc.edgeSync;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008930 File Offset: 0x00006B30
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
			AppDomain.CurrentDomain.UnhandledException += EdgeSyncSvc.MainUnhandledExceptionHandler;
			EdgeSyncSvc.runningAsService = !Environment.UserInteractive;
			foreach (string text in args)
			{
				if (text.StartsWith("-?"))
				{
					EdgeSyncSvc.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-wait"))
				{
					EdgeSyncSvc.waitToContinue = true;
				}
			}
			Globals.InitializeSinglePerfCounterInstance();
			if (EdgeSyncSvc.runningAsService)
			{
				ServiceBase.Run(new EdgeSyncSvc(false));
				return;
			}
			EdgeSyncSvc.RunConsole();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000089FC File Offset: 0x00006BFC
		protected override void OnStartInternal(string[] args)
		{
			if (EdgeSyncSvc.runningAsService)
			{
				base.RequestAdditionalTime((int)TimeSpan.FromSeconds(90.0).TotalMilliseconds);
			}
			try
			{
				this.config = EdgeSyncAppConfig.Load();
			}
			catch (ConfigurationErrorsException ex)
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_ConfigurationFailureException, null, new object[]
				{
					ex.Message
				});
				if (EdgeSyncSvc.runningAsService)
				{
					base.Stop();
					return;
				}
				Environment.Exit(1);
			}
			this.DelayStart();
			EdgeSyncSvc.edgeSync = new EdgeSync(!EdgeSyncSvc.runningAsService, this.config);
			if (!EdgeSyncSvc.edgeSync.Initialize())
			{
				if (EdgeSyncSvc.runningAsService)
				{
					base.Stop();
					return;
				}
				Environment.Exit(1);
			}
			this.timeSliceThread = new Thread(new ThreadStart(EdgeSyncSvc.edgeSync.Synchronize));
			this.timeSliceThread.Start();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008AF0 File Offset: 0x00006CF0
		protected override void OnStopInternal()
		{
			Thread thread = this.timeSliceThread;
			if (thread != null)
			{
				thread.Join();
			}
			if (EdgeSyncSvc.edgeSync != null)
			{
				EdgeSyncSvc.edgeSync.InitiateShutdown();
				EdgeSyncSvc.edgeSync.WaitShutdown();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008B30 File Offset: 0x00006D30
		private static void MainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			if (EdgeSyncSvc.edgeSync.Shutdown)
			{
				Environment.Exit(0);
			}
			int num = Interlocked.Exchange(ref EdgeSyncSvc.busyUnhandledException, 1);
			if (num == 1)
			{
				return;
			}
			ExWatson.HandleException(sender, eventArgs);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008B69 File Offset: 0x00006D69
		private static void Usage()
		{
			Console.WriteLine("usage");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008B78 File Offset: 0x00006D78
		private static void RunConsole()
		{
			Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
			if (EdgeSyncSvc.waitToContinue)
			{
				Console.WriteLine("Press ENTER to continue startup.");
				Console.ReadLine();
			}
			using (EdgeSyncSvc edgeSyncSvc = new EdgeSyncSvc(true))
			{
				edgeSyncSvc.OnStartInternal(null);
				for (;;)
				{
					Console.WriteLine("Press enter to shutdown or 'n' to initiate SyncNow.");
					string text = Console.ReadLine();
					if (text.Length == 0)
					{
						break;
					}
					if (text.StartsWith("n"))
					{
						if (EdgeSyncSvc.edgeSync.SyncNowState.Start(null, true, false))
						{
							Console.WriteLine("SyncNow started.");
							for (;;)
							{
								Status status = EdgeSyncSvc.edgeSync.SyncNowState.TryGetNextResult();
								if (status == null)
								{
									if (!EdgeSyncSvc.edgeSync.SyncNowState.Running)
									{
										break;
									}
									Console.WriteLine("sync now wait");
									Thread.Sleep(1000);
								}
								else
								{
									status.Dump();
									Console.WriteLine("dumped status to debug log for {0} : {1}", status.Type, status.Name);
								}
							}
							Console.WriteLine("sync now done");
						}
						else
						{
							Console.WriteLine("could not start SyncNow.");
						}
					}
				}
				Console.WriteLine("Shutting down ...");
				edgeSyncSvc.OnStopInternal();
				Console.WriteLine("Done.");
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008CC0 File Offset: 0x00006EC0
		private void DelayStart()
		{
			if (this.config.DelayStart != TimeSpan.MinValue)
			{
				Thread.Sleep(this.config.DelayStart);
			}
		}

		// Token: 0x0400006D RID: 109
		public const int ConfigUpdate = 201;

		// Token: 0x0400006E RID: 110
		private const string HelpOption = "-?";

		// Token: 0x0400006F RID: 111
		private const int SleepTime = 1000;

		// Token: 0x04000070 RID: 112
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000071 RID: 113
		public static readonly string EdgeSyncSvcName = "MSExchangeEdgeSync";

		// Token: 0x04000072 RID: 114
		private static bool runningAsService;

		// Token: 0x04000073 RID: 115
		private static bool waitToContinue = false;

		// Token: 0x04000074 RID: 116
		private static int busyUnhandledException;

		// Token: 0x04000075 RID: 117
		private static volatile EdgeSync edgeSync;

		// Token: 0x04000076 RID: 118
		private EdgeSyncAppConfig config;

		// Token: 0x04000077 RID: 119
		private Thread timeSliceThread;
	}
}
