using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Reflection;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000033 RID: 51
	internal class LogSearchService : ExServiceBase
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000810B File Offset: 0x0000630B
		public static LogSearchService Instance
		{
			get
			{
				return LogSearchService.logSearchService;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00008112 File Offset: 0x00006312
		public static ExEventLog Logger
		{
			get
			{
				return LogSearchService.logger;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00008119 File Offset: 0x00006319
		public HealthMonitoringLog HealthLog
		{
			get
			{
				return this.healthLog;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008124 File Offset: 0x00006324
		public LogSearchService(bool consoleTracing)
		{
			base.ServiceName = "MSExchangeTransportLogSearch";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008194 File Offset: 0x00006394
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
			if (!Debugger.IsAttached)
			{
				ExWatson.Register();
			}
			LogSearchService.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					LogSearchService.Usage();
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
			Globals.InitializeMultiPerfCounterInstance("LogSearchSvc");
			ADSession.DisableAdminTopologyMode();
			if (!LogSearchService.runningAsService)
			{
				if (!flag)
				{
					LogSearchService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			SettingOverrideSync.Instance.Start(true);
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.ADExceptionHandling.Enabled)
			{
				LogSearchService.logSearchService = new LogSearchService(!LogSearchService.runningAsService);
			}
			else
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					LogSearchService.logSearchService = new LogSearchService(!LogSearchService.runningAsService);
				}, 0);
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.ServiceTracer.TraceError<Exception>(0L, "Create Log Search Service failed: {0}", adoperationResult.Exception);
					Environment.Exit(1);
				}
			}
			try
			{
				Kerberos.FlushTicketCache();
			}
			catch (Win32Exception arg)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<Win32Exception>(0L, "MsExchangeLogSearch caught a Win32Exception when flushing the Kerberos Ticket Cache at startup: {0}", arg);
			}
			if (!LogSearchService.runningAsService)
			{
				LogSearchService.logSearchService.OnStartInternal(args);
				bool flag3 = false;
				while (!flag3)
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
						flag3 = true;
						break;
					case 's':
						LogSearchService.logSearchService.sessionManager.ShowSessions();
						break;
					}
				}
				Console.WriteLine("Shutting down ...");
				LogSearchService.logSearchService.OnStopInternal();
				Console.WriteLine("Done.");
				return;
			}
			ServiceBase.Run(LogSearchService.logSearchService);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000083EC File Offset: 0x000065EC
		private static void SkipRoleCheck(TransportServerConfiguration.Builder builder)
		{
			builder.RoleCheck = false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000083F5 File Offset: 0x000065F5
		protected override void OnStartInternal(string[] args)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service is starting");
			if (LogSearchService.runningAsService)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.BeginStart));
				return;
			}
			this.BeginStart(null);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000842E File Offset: 0x0000662E
		private static void Usage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000844E File Offset: 0x0000664E
		private static bool IsValidSyncHubHealthLogConfiguration(Server server)
		{
			return server.IsHubTransportServer && server.TransportSyncHubHealthLogEnabled && server.TransportSyncHubHealthLogFilePath != null && !string.IsNullOrEmpty(server.TransportSyncHubHealthLogFilePath.PathName);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008483 File Offset: 0x00006683
		private static bool IsValidSyncMailboxHealthLogConfiguration(Server server)
		{
			return server.IsMailboxServer && server.TransportSyncMailboxHealthLogEnabled && server.TransportSyncMailboxHealthLogFilePath != null && !string.IsNullOrEmpty(server.TransportSyncMailboxHealthLogFilePath.PathName);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000084B8 File Offset: 0x000066B8
		private void BeginStart(object state)
		{
			lock (LogSearchService.logSearchService)
			{
				if (this.localServer == null)
				{
					return;
				}
				this.localServer.Changed += this.OnConfigurationLoad;
				try
				{
					this.localServer.Load();
				}
				catch (TransportComponentLoadFailedException ex)
				{
					LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchReadConfigFailed, DateTime.UtcNow.Hour.ToString(), new object[]
					{
						ex
					});
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch service failed to start: {0}. Will retry in 2 minutes.", ex.Message);
					if (!LogSearchService.runningAsService)
					{
						Environment.Exit(1);
					}
					if (LogSearchService.configReloadTimer == null)
					{
						LogSearchService.configReloadTimer = new GuardedTimer(new TimerCallback(this.BeginStart), null, LogSearchService.ConfigLoadRetryInterval, LogSearchService.InfiniteInterval);
					}
					else
					{
						LogSearchService.configReloadTimer.Change(LogSearchService.ConfigLoadRetryInterval, LogSearchService.InfiniteInterval);
					}
					return;
				}
			}
			LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStartSuccess, null, new object[0]);
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service started successfully");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008600 File Offset: 0x00006800
		protected override void OnStopInternal()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service is stopping");
			bool flag = false;
			if (Monitor.TryEnter(LogSearchService.logSearchService, TimeSpan.FromSeconds(10.0)))
			{
				try
				{
					this.localServer.Unload();
					if (LogSearchService.configReloadTimer != null)
					{
						LogSearchService.configReloadTimer.Dispose(false);
					}
					if (this.sessionManager != null)
					{
						this.sessionManager.Stop();
					}
					if (LogSearchRpcServer.RpcIntfHandle != IntPtr.Zero)
					{
						RpcServerBase.StopServer(LogSearchRpcServer.RpcIntfHandle);
					}
					if (this.messageTrackingLog != null)
					{
						this.messageTrackingLog.Stop();
					}
					if (this.healthLog != null)
					{
						this.healthLog.Dispose();
						this.healthLog = null;
					}
					EHALogSearchComponent.Stop();
					LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStopSuccess, null, new object[0]);
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service stopped successfully");
					this.localServer = null;
					flag = true;
				}
				finally
				{
					Monitor.Exit(LogSearchService.logSearchService);
					if (!flag)
					{
						LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStopFailure, null, new object[0]);
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "MsExchangeLogSearch services failure to stop");
					}
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008744 File Offset: 0x00006944
		protected override void OnCustomCommandInternal(int command)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service is OnCustomCommandInternal");
			switch (command)
			{
			case 201:
				this.localServer.Reload(null, null);
				return;
			case 202:
				if (this.messageTrackingLog != null)
				{
					this.messageTrackingLog.ForceUpdate();
				}
				if (this.syncHealthLogHub != null)
				{
					this.syncHealthLogHub.ForceUpdate();
				}
				if (this.syncHealthLogMailbox != null)
				{
					this.syncHealthLogMailbox.ForceUpdate();
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000087C8 File Offset: 0x000069C8
		private void OnConfigurationLoad(TransportServerConfiguration args)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service is reloading configuration from AD");
			Server transportServer = args.TransportServer;
			if (this.sessionManager == null)
			{
				this.Initialize(transportServer, args.TransportServerSecurity);
				return;
			}
			if (this.messageTrackingLog != null && transportServer.MessageTrackingLogPath != null)
			{
				this.messageTrackingLog.ChangePath(transportServer.MessageTrackingLogPath.ToString());
				ExTraceGlobals.ServiceTracer.TraceDebug<LocalLongFullPath>((long)this.GetHashCode(), "Updating the path to: {0}", transportServer.MessageTrackingLogPath);
			}
			this.ConfigureTransportSyncComponent(transportServer);
			LogSearchService.logSearchServer.SecurityDescriptor = args.TransportServerSecurity;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008870 File Offset: 0x00006A70
		private void Initialize(Server server, ObjectSecurity adminSecurity)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch service is entering Initialize");
			bool flag = false;
			LogSearchService.LogSearchServiceStartState logSearchServiceStartState = LogSearchService.LogSearchServiceStartState.Init;
			try
			{
				this.sessionManager = new LogSessionManager();
				this.sessionManager.Start();
				logSearchServiceStartState = LogSearchService.LogSearchServiceStartState.SessionManagerStartup;
				if (server.IsHubTransportServer)
				{
					EHALogSearchComponent.Start(server.Name);
				}
				if (LogSearchAppConfig.Instance.HealthMonitoringLog == null)
				{
					ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "Health monitoring log could not be initialized, service will be stopped");
				}
				else
				{
					this.healthLog = new HealthMonitoringLog();
					this.healthLog.Configure(LogSearchAppConfig.Instance.HealthMonitoringLog);
					logSearchServiceStartState = LogSearchService.LogSearchServiceStartState.HealthMonitoringLogStartup;
					this.ConfigureTransportSyncComponent(server);
					logSearchServiceStartState = LogSearchService.LogSearchServiceStartState.SyncHealthLogStartup;
					if (server.MessageTrackingLogPath != null && !string.IsNullOrEmpty(server.MessageTrackingLogPath.PathName))
					{
						this.messageTrackingLog = new Log(MessageTrackingSchema.MessageTrackingEvent, server.MessageTrackingLogPath.ToString(), "MSGTRK", server.Name, "LOG", LogSearchIndexingParameters.MessageTrackingIndexPercentageByPrefix);
						this.messageTrackingLog.Config(server.MessageTrackingLogPath.ToString());
						this.messageTrackingLog.Start();
						this.sessionManager.RegisterLog("MSGTRK", this.messageTrackingLog);
						logSearchServiceStartState = LogSearchService.LogSearchServiceStartState.MessageTrackingLogStartup;
					}
					else if (!server.IsClientAccessServer)
					{
						LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchNullOrEmptyLogPath, null, new object[0]);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportLogSearch", null, "The Microsoft Exchange Transport Log Search service failed because the parameter that sets the location of the message tracking logs was set to an invalid value.", ResultSeverityLevel.Warning, false);
					}
					ActiveDirectoryRights accessMask = ActiveDirectoryRights.GenericRead;
					bool flag2 = !server.IsHubTransportServer && !server.IsMailboxServer;
					LogSearchService.logSearchServer = (LogSearchServer)RpcServerBase.RegisterServer(typeof(LogSearchServer), adminSecurity, accessMask, flag2);
					ExTraceGlobals.ServiceTracer.TraceDebug<bool>((long)this.GetHashCode(), "RPC is registered. LocalRpcOnly = {0}", flag2);
					LogSearchService.logSearchServer.SessionManager = this.sessionManager;
					flag = true;
				}
			}
			finally
			{
				if (!flag)
				{
					if (this.sessionManager != null)
					{
						this.sessionManager.Stop();
						this.sessionManager = null;
					}
					if (this.messageTrackingLog != null)
					{
						this.messageTrackingLog.Stop();
						this.messageTrackingLog = null;
					}
					if (this.healthLog != null)
					{
						this.healthLog.Dispose();
						this.healthLog = null;
					}
					switch (logSearchServiceStartState)
					{
					case LogSearchService.LogSearchServiceStartState.Init:
					{
						string text = "MsExchangeLogSearch services Initialize failure because session manager startup failed";
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), text);
						LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStartFailureInit, null, new object[0]);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportLogSearch", null, text, ResultSeverityLevel.Error, false);
						break;
					}
					case LogSearchService.LogSearchServiceStartState.SessionManagerStartup:
					{
						string text = "MsExchangeLogSearch services Initialize failure because message tracking log failed";
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), text);
						LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStartFailureSessionManager, null, new object[0]);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportLogSearch", null, text, ResultSeverityLevel.Error, false);
						break;
					}
					case LogSearchService.LogSearchServiceStartState.MessageTrackingLogStartup:
					{
						string text = "MsExchangeLogSearch services Initialize failure because RPC server startup failed";
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), text);
						LogSearchService.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStartFailureMTGLog, null, new object[0]);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportLogSearch", null, text, ResultSeverityLevel.Error, false);
						break;
					}
					}
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008BB4 File Offset: 0x00006DB4
		private Log CreateSyncHealthLogAgent(string syncHealthLogDirectory, string syncHealthLogName, string serverName)
		{
			Log log = new Log(SyncHealthLogSchema.SyncHealthLogEvent, syncHealthLogDirectory, "SYNCHEALTHLOG", serverName, "LOG", null);
			log.Config(syncHealthLogDirectory);
			log.Start();
			this.sessionManager.RegisterLog(syncHealthLogName, log);
			return log;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008BF4 File Offset: 0x00006DF4
		private void ConfigureTransportSyncComponent(Server server)
		{
			bool flag = LogSearchService.IsValidSyncHubHealthLogConfiguration(server);
			bool flag2 = LogSearchService.IsValidSyncMailboxHealthLogConfiguration(server);
			if (flag)
			{
				if (this.syncHealthLogHub == null)
				{
					this.syncHealthLogHub = this.CreateSyncHealthLogAgent(server.TransportSyncHubHealthLogFilePath.PathName, "SyncHealthHub", server.Name);
				}
				else
				{
					this.syncHealthLogHub.ChangePath(server.TransportSyncHubHealthLogFilePath.PathName);
					ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Updating the Sync Hub Health Log Path to: {0}", server.TransportSyncHubHealthLogFilePath.PathName);
				}
			}
			if (flag2)
			{
				if (this.syncHealthLogMailbox == null)
				{
					this.syncHealthLogMailbox = this.CreateSyncHealthLogAgent(server.TransportSyncMailboxHealthLogFilePath.PathName, "SyncHealthMailbox", server.Name);
					return;
				}
				this.syncHealthLogMailbox.ChangePath(server.TransportSyncMailboxHealthLogFilePath.PathName);
				ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Updating the Sync Mailbox Health Log Path to: {0}", server.TransportSyncMailboxHealthLogFilePath.PathName);
			}
		}

		// Token: 0x040000B4 RID: 180
		private const int ConfigUpdate = 201;

		// Token: 0x040000B5 RID: 181
		private const int RefreshIndex = 202;

		// Token: 0x040000B6 RID: 182
		private const string LogSearchSvcName = "MSExchangeTransportLogSearch";

		// Token: 0x040000B7 RID: 183
		private const string HelpOption = "-?";

		// Token: 0x040000B8 RID: 184
		private const string ConsoleOption = "-console";

		// Token: 0x040000B9 RID: 185
		private const string WaitToContinueOption = "-wait";

		// Token: 0x040000BA RID: 186
		private const string LogSearchServiceEventSource = "MSExchangeTransportLogSearch";

		// Token: 0x040000BB RID: 187
		private static readonly TimeSpan ConfigLoadRetryInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x040000BC RID: 188
		private static readonly TimeSpan InfiniteInterval = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x040000BD RID: 189
		private static GuardedTimer configReloadTimer;

		// Token: 0x040000BE RID: 190
		private static bool runningAsService;

		// Token: 0x040000BF RID: 191
		private static LogSearchService logSearchService;

		// Token: 0x040000C0 RID: 192
		private static LogSearchServer logSearchServer;

		// Token: 0x040000C1 RID: 193
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeTransportLogSearch");

		// Token: 0x040000C2 RID: 194
		private LogSessionManager sessionManager;

		// Token: 0x040000C3 RID: 195
		private Log messageTrackingLog;

		// Token: 0x040000C4 RID: 196
		private Log syncHealthLogHub;

		// Token: 0x040000C5 RID: 197
		private Log syncHealthLogMailbox;

		// Token: 0x040000C6 RID: 198
		private HealthMonitoringLog healthLog;

		// Token: 0x040000C7 RID: 199
		private ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder> localServer = new ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder>(new ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder>.ExternalConfigurationSetter(LogSearchService.SkipRoleCheck), Components.TransportAppConfig.ADPolling.TransportLogSearchServiceReloadInterval);

		// Token: 0x02000034 RID: 52
		private enum LogSearchServiceStartState
		{
			// Token: 0x040000CA RID: 202
			Init,
			// Token: 0x040000CB RID: 203
			HealthMonitoringLogStartup,
			// Token: 0x040000CC RID: 204
			SessionManagerStartup,
			// Token: 0x040000CD RID: 205
			MessageTrackingLogStartup,
			// Token: 0x040000CE RID: 206
			SyncHealthLogStartup
		}
	}
}
