using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Assistants.Logging;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.EDiscovery.MailboxSearch;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor;
using Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000002 RID: 2
	internal sealed class AssistantsService : ExServiceBase, IDiagnosable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		[MTAThread]
		private static void Main(string[] args)
		{
			new LocalizedString("Workaround for bug # 72378");
			int num = Privileges.RemoveAllExcept(AssistantsService.requiredPrivileges);
			if (num != 0)
			{
				Environment.Exit(num);
			}
			ExWatson.Register();
			ExWatson.RegisterReportAction(new WatsonRegKeyReportAction("HKLM\\SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ImagePath"), WatsonActionScope.Process);
			AssistantsService.ReadRegParams();
			Globals.InitializeMultiPerfCounterInstance("MSExchMbxAsst");
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.Assistants);
			AssistantsService service = new AssistantsService();
			if (Environment.UserInteractive && args.Length >= 1 && string.Compare(args[0], "-crash", true) == 0)
			{
				throw new Exception("Startup crash to test ExWatson stuff");
			}
			if (Environment.UserInteractive && args.Length >= 1 && string.Compare(args[0], "-console", true) == 0)
			{
				AssistantsService.TracerPfd.TracePfd<int>(3L, "PFD IWS {0} Starting the Mailbox Assistants Service in Console Mode", 28055);
				ExServiceBase.RunAsConsole(service);
				return;
			}
			ServiceBase.Run(service);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002196 File Offset: 0x00000396
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "MailboxAssistants";
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021A0 File Offset: 0x000003A0
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			ArgumentValidator.ThrowIfNull("parameters", parameters);
			DiagnosticsProcessor diagnosticsProcessor;
			try
			{
				diagnosticsProcessor = new DiagnosticsProcessor(parameters);
			}
			catch (DiagnosticArgumentException exception)
			{
				return DiagnosticsFormatter.FormatErrorElement(exception);
			}
			XElement xelement = diagnosticsProcessor.Process(this.databaseManager.TimeBasedDriverManager.TimeBasedAssistantControllerArray);
			AssistantsService.GetAssistantsSpecificDiagnostics(diagnosticsProcessor.Arguments, xelement);
			return xelement;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000223C File Offset: 0x0000043C
		protected override void OnStartInternal(string[] args)
		{
			if (AssistantsService.debugBreakOnStart)
			{
				Debugger.Break();
			}
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceStarting, null, new object[]
				{
					currentProcess.Id,
					"Microsoft Exchange",
					"15.00.1497.010"
				});
			}
			AssistantsService.TracerPfd.TracePfd<int, DateTime>((long)this.GetHashCode(), "PFD IWS {0} Starting the Mailbox Assistants Service {1} ", 28055, DateTime.UtcNow);
			AssistantsLog.LogServiceStartEvent(this.activityId);
			bool flag = false;
			try
			{
				ADSession.DisableAdminTopologyMode();
				SettingOverrideSync.Instance.Start(true);
				ProcessAccessManager.RegisterComponent(SettingOverrideSync.Instance);
				AssistantsService.TracerPfd.TracePfd<int>((long)this.GetHashCode(), "PFD IWS {0} Initializing the Assistant Infrastructure", 27479);
				this.databaseManager = new DatabaseManager("MSExchangeMailboxAssistants", 50, InfoworkerAssistants.CreateEventBasedAssistantTypes(), InfoworkerAssistants.CreateTimeBasedAssistantTypes(), true);
				this.databaseManager.Start();
				MailboxSearchServer.StartServer();
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceStarted, null, new object[0]);
				AssistantsService.TracerPfd.TracePfd<int>((long)this.GetHashCode(), "PFD IWS {0} Mailbox Assistants Service is started successfully ", 23959);
				if (Configuration.MemoryMonitorEnabled)
				{
					this.memoryMonitor = new MemoryMonitor();
					this.memoryMonitor.Start((ulong)Configuration.MemoryBarrierPrivateBytesUsageLimit, (uint)Configuration.MemoryBarrierNumberOfSamples, Configuration.MemoryBarrierSamplingDelay, Configuration.MemoryBarrierSamplingInterval, delegate(ulong memoryInUse)
					{
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceOutOfMemory, null, new object[]
						{
							memoryInUse
						});
						throw new ServiceOutOfMemoryException();
					});
				}
				this.watermarkCleanupTimer = new Timer(new TimerCallback(InfoworkerAssistants.DeleteWatermarksForDisabledAndDeprecatedAssistants), null, Configuration.WatermarkCleanupInterval, Configuration.WatermarkCleanupInterval);
				ProcessAccessManager.RegisterComponent(this);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceFailedToStart, null, new object[0]);
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002438 File Offset: 0x00000638
		protected override void OnStopInternal()
		{
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceStopping, null, new object[0]);
			AssistantsService.TracerPfd.TracePfd<int>((long)this.GetHashCode(), "PFD IWS {0} Stopping the Mailbox Assistants Service", 32151);
			if (this.memoryMonitor != null)
			{
				this.memoryMonitor.Stop();
			}
			if (this.watermarkCleanupTimer != null)
			{
				this.watermarkCleanupTimer.Dispose();
			}
			if (this.databaseManager != null)
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						this.databaseManager.Stop();
					});
				}
				catch (GrayException arg)
				{
					AssistantsService.Tracer.TraceDebug<AssistantsService, GrayException>((long)this.GetHashCode(), "{0}: Gray Exception reported during shutdown: {1}", this, arg);
				}
				finally
				{
					this.databaseManager.Dispose();
				}
				AssistantsService.TracerPfd.TracePfd<int>((long)this.GetHashCode(), "PFD IWS {0} Stopping the Assistant Infrastructure", 17815);
			}
			MailboxSearchServer.StopServer();
			AssistantsLog.LogServiceStopEvent(this.activityId);
			AssistantsLog.Stop();
			MailboxAssistantsSlaReportLogFactory.StopAll();
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ServiceStopped, null, new object[]
				{
					currentProcess.Id,
					"Microsoft Exchange",
					"15.00.1497.010"
				});
			}
			ProcessAccessManager.UnregisterComponent(this);
			ProcessAccessManager.UnregisterComponent(SettingOverrideSync.Instance);
			SettingOverrideSync.Instance.Stop();
			AssistantsService.TracerPfd.TracePfd<int>((long)this.GetHashCode(), "PFD IWS {0} The Mailbox Assistants Service Stopped successfully", 21911);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025C4 File Offset: 0x000007C4
		protected override void OnCustomCommandInternal(int command)
		{
			if (command != 206)
			{
				return;
			}
			AssistantsLog.Flush();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000025E1 File Offset: 0x000007E1
		protected override void OnCommandTimeout()
		{
			ExWatson.AddExtraData(AIBreadcrumbs.Instance.GenerateReport());
			base.OnCommandTimeout();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025F8 File Offset: 0x000007F8
		private AssistantsService()
		{
			base.AutoLog = false;
			this.activityId = Guid.NewGuid();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002614 File Offset: 0x00000814
		private static void ReadRegParams()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters", false))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(AssistantsService.debugBreakOnStartRegistryName);
					if (value != null && value is int && (int)value != 0)
					{
						AssistantsService.debugBreakOnStart = true;
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002678 File Offset: 0x00000878
		private static void GetAssistantsSpecificDiagnostics(DiagnosticsArgument arguments, XElement root)
		{
			if (arguments != null)
			{
				if (arguments.HasArgument("mailboxprocessor"))
				{
					root.Add(MailboxProcessorAssistant.GetMailboxProcessorAssistantDiagnosticInfo(arguments));
				}
				if (arguments.HasArgument("publicfolder"))
				{
					root.Add(PublicFolderAssistant.GetPublicFolderAssistantDiagnosticInfo(arguments));
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const int MaxNumberOfThreads = 50;

		// Token: 0x04000002 RID: 2
		private DatabaseManager databaseManager;

		// Token: 0x04000003 RID: 3
		private MemoryMonitor memoryMonitor;

		// Token: 0x04000004 RID: 4
		private Timer watermarkCleanupTimer;

		// Token: 0x04000005 RID: 5
		private Guid activityId;

		// Token: 0x04000006 RID: 6
		private static readonly string[] requiredPrivileges = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege",
			"SeCreateSymbolicLinkPrivilege"
		};

		// Token: 0x04000007 RID: 7
		private static bool debugBreakOnStart = false;

		// Token: 0x04000008 RID: 8
		private static readonly string debugBreakOnStartRegistryName = "DebugBreakOnStart";

		// Token: 0x04000009 RID: 9
		internal static CachedStateDictionary CachedObjectsList = new CachedStateDictionary(10);

		// Token: 0x0400000A RID: 10
		internal static Dictionary<WorkloadType, Hashtable> MailboxesStates = new Dictionary<WorkloadType, Hashtable>();

		// Token: 0x0400000B RID: 11
		internal static Dictionary<WorkloadType, Dictionary<string, DateTime>> AssistantsLastScanTimes = new Dictionary<WorkloadType, Dictionary<string, DateTime>>();

		// Token: 0x0400000C RID: 12
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.AssistantBaseTracer;

		// Token: 0x0400000D RID: 13
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
