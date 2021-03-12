using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000006 RID: 6
	internal class NotificationsBrokerService : ExServiceBase
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002394 File Offset: 0x00000594
		private NotificationsBrokerService()
		{
			base.ServiceName = "MSExchangeNotificationsBroker";
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023A7 File Offset: 0x000005A7
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023AE File Offset: 0x000005AE
		private static ExEventLog Log { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x000023B6 File Offset: 0x000005B6
		public static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (TestHooks.OnEventLog != null)
			{
				TestHooks.OnEventLog(tuple, messageArgs);
				return;
			}
			if (NotificationsBrokerService.Log != null)
			{
				NotificationsBrokerService.Log.LogEvent(tuple, periodicKey, messageArgs);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E4 File Offset: 0x000005E4
		public static void Main(string[] args)
		{
			NotificationsBrokerService.Log = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchange Notifications Broker");
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
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				else
				{
					if (!text.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
					{
						Console.WriteLine("invalid options\n");
						return;
					}
					flag = true;
				}
			}
			ExWatson.Register();
			flag |= !NotificationsBrokerService.IsRunningAsService();
			NotificationsBrokerService service = new NotificationsBrokerService();
			if (flag)
			{
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue...");
					Console.ReadLine();
				}
				ExServiceBase.RunAsConsole(service);
				return;
			}
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (!current.IsSystem)
				{
					NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_NotRunningAsLocalSystem, string.Empty, new object[0]);
					Environment.Exit(5);
				}
			}
			ServiceBase.Run(service);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002518 File Offset: 0x00000718
		protected override void OnStartInternal(string[] args)
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_ServiceStarting, string.Empty, new object[]
				{
					currentProcess.Id
				});
			}
			Globals.InitializeSinglePerfCounterInstance();
			SettingOverrideSync.Instance.Start(true);
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).NotificationBrokerService.Service.Enabled)
			{
				this.InitializeService();
			}
			else
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Do Nothing on Startup. Service was not enabled.");
			}
			using (Process currentProcess2 = Process.GetCurrentProcess())
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
				NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_ServiceStarted, string.Empty, new object[]
				{
					versionInfo.FileMajorPart,
					versionInfo.FileMinorPart,
					versionInfo.FileBuildPart,
					versionInfo.FilePrivatePart,
					currentProcess2.Id
				});
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002650 File Offset: 0x00000850
		protected override void OnStopInternal()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_ServiceStopping, string.Empty, new object[]
				{
					currentProcess.Id
				});
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Stopping MSExchangeNotificationsBroker service");
			if (this.serviceInitialized)
			{
				this.CleanupService();
			}
			else
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Do Nothing on Stop. Service was not initialied during Startup.");
			}
			NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_ServiceStopped, string.Empty, new object[0]);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026F8 File Offset: 0x000008F8
		private static bool IsRunningAsService()
		{
			bool flag = false;
			bool flag2 = false;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IdentityReferenceCollection groups = current.Groups;
				foreach (IdentityReference identityReference in groups)
				{
					if (identityReference is SecurityIdentifier)
					{
						SecurityIdentifier securityIdentifier = (SecurityIdentifier)identityReference;
						if (securityIdentifier.IsWellKnown(WellKnownSidType.ServiceSid))
						{
							flag = true;
						}
						if (securityIdentifier.IsWellKnown(WellKnownSidType.InteractiveSid))
						{
							flag2 = true;
						}
					}
				}
			}
			return flag || (!flag && !flag2);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027A4 File Offset: 0x000009A4
		private void InitializeService()
		{
			BrokerLogger.Initialize();
			using (BrokerLogger.StartEvent("InitializeService"))
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Starting MSExchangeNotificationsBroker service");
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents();
				IEventBasedAssistantType[] eventBasedAssistantTypeArray = new IEventBasedAssistantType[]
				{
					new MailboxChangeAssistantType()
				};
				this.databaseManager = new DatabaseManager(base.ServiceName, BrokerConfiguration.MaxDatabaseManagerThreads.Value, eventBasedAssistantTypeArray, null, false);
				ExTraceGlobals.MailboxChangeTracer.TraceDebug((long)this.GetHashCode(), "DatabaseManager about to start");
				this.databaseManager.Start();
				ExTraceGlobals.MailboxChangeTracer.TraceDebug((long)this.GetHashCode(), "DatabaseManager started");
				ConfigProvider.Instance.AutoRefresh = true;
				ConfigProvider.Instance.LoadTrustedIssuers = true;
				NotificationsBrokerRpcServer.Start();
				RemoteConduit.Singleton.Start();
				this.serviceInitialized = true;
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Service was initialized successfully on Startup. Service was enabled.");
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028A0 File Offset: 0x00000AA0
		private void CleanupService()
		{
			using (BrokerLogger.StartEvent("CleanupService"))
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Starting Clean up on Stop. Service was initialized during Startup.");
				RemoteConduit.Singleton.Stop();
				NotificationsBrokerRpcServer.Stop();
				ExchangeDiagnosticsHelper.UnRegisterDiagnosticsComponents();
				Generator.Singleton.Dispose();
				if (this.databaseManager != null)
				{
					this.databaseManager.Stop();
					this.databaseManager.Dispose();
				}
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Service was cleaned up successfully.");
			}
		}

		// Token: 0x04000027 RID: 39
		public const string NotificationsBrokerServiceName = "MSExchangeNotificationsBroker";

		// Token: 0x04000028 RID: 40
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000029 RID: 41
		private const string RunAsConsoleOption = "-console";

		// Token: 0x0400002A RID: 42
		private bool serviceInitialized;

		// Token: 0x0400002B RID: 43
		private DatabaseManager databaseManager;
	}
}
