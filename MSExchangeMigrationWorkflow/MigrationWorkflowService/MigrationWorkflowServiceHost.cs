using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MigrationWorkflowService;
using Microsoft.Exchange.MailboxLoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MigrationWorkflowService.Servicelets;
using Microsoft.Exchange.Servicelets.BatchCreator;

namespace Microsoft.Exchange.MigrationWorkflowService
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MigrationWorkflowServiceHost : ExServiceBase
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020D0 File Offset: 0x000002D0
		public MigrationWorkflowServiceHost()
		{
			base.ServiceName = "Migration Workflow Service";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			this.eventLog = new ExEventLog(ExTraceGlobals.MigrationWorkflowServiceTracer.Category, "MSExchange Migration Workflow");
			base.AutoLog = false;
			this.serviceStopHandle = new ManualResetEvent(false);
			this.servicelets = new List<IServicelet>();
			this.servicelets.Add(new AnchorServicelet<LoadBalanceAnchorContext, LoadBalanceServiceBootstrapper>(this.serviceStopHandle));
			this.servicelets.Add(new SimpleAnchorServicelet<BatchCreatorContext>(this.serviceStopHandle));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		public static void Main(string[] args)
		{
			MigrationWorkflowServiceHost.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					MigrationWorkflowServiceHost.Usage();
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
			if (!MigrationWorkflowServiceHost.runningAsService)
			{
				if (!flag)
				{
					MigrationWorkflowServiceHost.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0} in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			ExWatson.Register();
			Globals.InitializeMultiPerfCounterInstance("MSExchMigWkfl");
			MigrationWorkflowServiceHost.instance = new MigrationWorkflowServiceHost();
			if (!MigrationWorkflowServiceHost.runningAsService)
			{
				ExServiceBase.RunAsConsole(MigrationWorkflowServiceHost.instance);
				return;
			}
			ServiceBase.Run(MigrationWorkflowServiceHost.instance);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		protected override void OnStartInternal(string[] args)
		{
			this.logger = new LoadBalanceAnchorContext().Logger;
			int num = this.servicelets.Select(new Func<IServicelet, bool>(this.StartServicelet)).Count((bool serviceletStarted) => serviceletStarted);
			this.logger.Log(MigrationEventType.Information, "MigWorkflow service host started {0} servicelets.", new object[]
			{
				num
			});
			this.logger.Log(MigrationEventType.Verbose, "MSExchangeWorkflow services started.", new object[0]);
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.LogEvent(MWSEventLogConstants.Tuple_ServiceStarted, new object[]
				{
					LoadBalancerVersionInformation.LoadBalancerVersion.ProductMajor,
					LoadBalancerVersionInformation.LoadBalancerVersion.ProductMinor,
					LoadBalancerVersionInformation.LoadBalancerVersion.BuildMajor,
					LoadBalancerVersionInformation.LoadBalancerVersion.BuildMinor,
					currentProcess.Id
				});
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002368 File Offset: 0x00000568
		protected override void OnStopInternal()
		{
			this.serviceStopHandle.Set();
			this.logger.Log(MigrationEventType.Verbose, "Stopping MSExchangeWorkflow service.", new object[0]);
			if (this.servicelets != null)
			{
				foreach (IServicelet servicelet in this.servicelets)
				{
					this.logger.Log(MigrationEventType.Information, "Stopping servicelet {0}.", new object[]
					{
						servicelet.Name
					});
					this.logger.Log(MigrationEventType.Verbose, "Un-registering diagnostics interfaces.", new object[0]);
					foreach (IDiagnosable diagnosable in servicelet.GetDiagnosableComponents())
					{
						ProcessAccessManager.UnregisterComponent(diagnosable);
					}
					servicelet.Stop();
				}
			}
			this.logger.Log(MigrationEventType.Verbose, "MSExchangeWorkflow service stopped.", new object[0]);
			this.LogEvent(MWSEventLogConstants.Tuple_ServiceStopped, new object[0]);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000248C File Offset: 0x0000068C
		private static void Usage()
		{
			Console.WriteLine(MigrationWorkflowServiceStrings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024AC File Offset: 0x000006AC
		private static void RunServicelet(object serviceletObject)
		{
			IServicelet servicelet = serviceletObject as IServicelet;
			if (servicelet == null)
			{
				return;
			}
			ExWatson.SendReportOnUnhandledException(new ExWatson.MethodDelegate(servicelet.Run));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024D8 File Offset: 0x000006D8
		private static void StartServiceletThread(IServicelet servicelet)
		{
			new Thread(new ParameterizedThreadStart(MigrationWorkflowServiceHost.RunServicelet))
			{
				Name = string.Format("{0}-Serviclet", servicelet.Name),
				IsBackground = true
			}.Start(servicelet);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000251C File Offset: 0x0000071C
		private bool StartServicelet(IServicelet servicelet)
		{
			bool result;
			try
			{
				if (!servicelet.IsEnabled)
				{
					this.logger.Log(MigrationEventType.Information, "Servicelet {0} is not enabled, skipped.", new object[]
					{
						servicelet.Name
					});
					result = false;
				}
				else
				{
					this.logger.Log(MigrationEventType.Information, "Starting servicelet {0}.", new object[]
					{
						servicelet.Name
					});
					this.logger.Log(MigrationEventType.Verbose, "Initializing servicelet.", new object[0]);
					if (!servicelet.Initialize())
					{
						this.logger.Log(MigrationEventType.Verbose, "Servicelet did not initialize, not starting it.", new object[0]);
						result = false;
					}
					else
					{
						this.logger.Log(MigrationEventType.Verbose, "Registering diagnostics interfaces.", new object[0]);
						foreach (IDiagnosable diagnosable in servicelet.GetDiagnosableComponents())
						{
							ProcessAccessManager.RegisterComponent(diagnosable);
						}
						MigrationWorkflowServiceHost.StartServiceletThread(servicelet);
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				this.LogEvent(MWSEventLogConstants.Tuple_ServiceletFailedToStart, new object[]
				{
					servicelet.Name,
					CommonUtils.FullExceptionMessage(ex, true)
				});
				this.logger.LogError(ex, "Servicelet {0} failed to start.", new object[]
				{
					servicelet.Name
				});
				if (!(ex is LocalizedException))
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002694 File Offset: 0x00000894
		private void AbortStartup(Exception ex)
		{
			this.LogEvent(MWSEventLogConstants.Tuple_ServiceFailedToStart, new object[]
			{
				(ex != null) ? CommonUtils.FullExceptionMessage(ex, true) : string.Empty
			});
			base.GracefullyAbortStartup();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026D3 File Offset: 0x000008D3
		private void LogEvent(ExEventLog.EventTuple eventTuple, params object[] messageArgs)
		{
			CommonUtils.LogEvent(this.eventLog, eventTuple, messageArgs);
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string ConsoleOption = "-console";

		// Token: 0x04000003 RID: 3
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000004 RID: 4
		private static MigrationWorkflowServiceHost instance;

		// Token: 0x04000005 RID: 5
		private static bool runningAsService;

		// Token: 0x04000006 RID: 6
		private readonly ExEventLog eventLog;

		// Token: 0x04000007 RID: 7
		private readonly List<IServicelet> servicelets;

		// Token: 0x04000008 RID: 8
		private readonly ManualResetEvent serviceStopHandle;

		// Token: 0x04000009 RID: 9
		private ILogger logger;
	}
}
