using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.Transport.Sync.Migration;
using Microsoft.Exchange.Transport.Sync.Worker.Agents;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AggregationComponent : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private AggregationComponent()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F9 File Offset: 0x000002F9
		public static ExEventLog EventLogger
		{
			get
			{
				return AggregationComponent.eventLogger;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		public string CurrentState
		{
			get
			{
				string result;
				lock (this.syncRoot)
				{
					result = ((this.scheduler != null) ? this.scheduler.CurrentState : "Unknown");
				}
				return result;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002158 File Offset: 0x00000358
		private bool ShouldExecute
		{
			get
			{
				return this.transportSyncEnabled && this.targetRunningState == ServiceState.Active;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216D File Offset: 0x0000036D
		public static IStartableTransportComponent CreateAggregationComponent()
		{
			return AggregationComponent.instance;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002174 File Offset: 0x00000374
		public void Load()
		{
			lock (this.instrumentationSyncRoot)
			{
				this.lastLoadCallstack = this.lastLoadCallstack + "Load start.  Callstack:" + Environment.StackTrace;
			}
			this.Initialize();
			this.syncLogSession.LogInformation((TSLID)433UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Load.", new object[0]);
			if (AggregationConfiguration.IsDatacenterMode)
			{
				this.remoteServerHealthManager = new RemoteServerHealthManager(this.syncLogSession, AggregationConfiguration.Instance, new Action<EventLogEntry>(AggregationComponent.LogEvent), new Action<RemoteServerHealthData>(AggregationComponent.LogRemoteServerHealth));
				this.scheduler = new AggregationScheduler(this.syncLogSession);
				this.scheduler.EnabledAggregationTypes = AggregationConfiguration.Instance.EnabledAggregationTypes;
				SyncPoisonHandler.Load(this.syncLogSession);
				SubscriptionAgentManager.Instance.RegisterAgentFactory(new SyncMigrationSubscriptionAgentFactory());
				SubscriptionAgentManager.Instance.RegisterAgentFactory(new PeopleConnectSubscriptionAgentFactory());
				SubscriptionAgentManager.Instance.Start(this.syncLogSession, AggregationConfiguration.Instance.DisabledSubscriptionAgents);
				this.StartRpcServer();
				lock (this.instrumentationSyncRoot)
				{
					this.lastLoadCallstack = this.lastLoadCallstack + Environment.NewLine + "Load end" + Environment.NewLine;
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022EC File Offset: 0x000004EC
		public void Unload()
		{
			this.syncLogSession.LogInformation((TSLID)434UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Unload.", new object[0]);
			this.Shutdown();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002324 File Offset: 0x00000524
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			this.syncLogSession.LogInformation((TSLID)435UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Start. (Initially Paused={0})", new object[]
			{
				initiallyPaused
			});
			this.transportSyncPaused = initiallyPaused;
			this.targetRunningState = targetRunningState;
			if (!AggregationConfiguration.IsDatacenterMode)
			{
				this.syncLogSession.LogError((TSLID)436UL, AggregationComponent.diag, (long)this.GetHashCode(), "Transport Sync is not enabled. Will not Start AggregationComponent.", new object[0]);
				return;
			}
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjectionUtil.Callback));
			if (this.ShouldExecute)
			{
				this.StartScheduler();
			}
			this.syncDiagnostics = new SyncDiagnostics(this.scheduler, this.remoteServerHealthManager);
			this.syncDiagnostics.Register();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023F2 File Offset: 0x000005F2
		public void Stop()
		{
			this.syncLogSession.LogInformation((TSLID)437UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Stop.", new object[0]);
			this.Shutdown();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002428 File Offset: 0x00000628
		public void Pause()
		{
			this.syncLogSession.LogInformation((TSLID)438UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Pause. (Enabled={0}, Service State={1})", new object[]
			{
				this.transportSyncEnabled,
				this.targetRunningState
			});
			lock (this.syncRoot)
			{
				this.transportSyncPaused = true;
				if (this.ShouldExecute)
				{
					this.PauseScheduler();
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024C4 File Offset: 0x000006C4
		public void Continue()
		{
			this.syncLogSession.LogInformation((TSLID)439UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Continue. (Enabled={0}, Service state={1})", new object[]
			{
				this.transportSyncEnabled,
				this.targetRunningState
			});
			lock (this.syncRoot)
			{
				this.transportSyncPaused = false;
				if (this.ShouldExecute)
				{
					this.StartScheduler();
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002560 File Offset: 0x00000760
		public string OnUnhandledException(Exception e)
		{
			if (!this.ShouldExecute)
			{
				return null;
			}
			SyncLogSession syncLogSession = this.syncLogSession;
			if (syncLogSession != null)
			{
				syncLogSession.LogInformation((TSLID)440UL, AggregationComponent.diag, (long)this.GetHashCode(), "UnhandledException handler invoked for unhandled exception: {0}.", new object[]
				{
					e
				});
			}
			SyncPoisonHandler.SavePoisonContext(e, syncLogSession);
			if (syncLogSession != null)
			{
				syncLogSession.AddBlackBoxLogToWatson();
			}
			return null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025C0 File Offset: 0x000007C0
		internal static void LogEvent(EventLogEntry eventLogEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("eventLogEntry", eventLogEntry);
			AggregationComponent.EventLogger.LogEvent(eventLogEntry.Tuple, eventLogEntry.PeriodicKey, eventLogEntry.MessageArgs);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025EC File Offset: 0x000007EC
		internal static void LogRemoteServerHealth(RemoteServerHealthData healthData)
		{
			SyncUtilities.ThrowIfArgumentNull("healthData", healthData);
			string machineName = Environment.MachineName;
			DateTime lastBackOffStartTime = (healthData.LastBackOffStartTime != null) ? healthData.LastBackOffStartTime.Value.UniversalTime : DateTime.MinValue;
			AggregationConfiguration.Instance.SyncHealthLog.LogRemoteServerHealth(machineName, healthData.ServerName, healthData.State.ToString(), healthData.BackOffCount, lastBackOffStartTime, healthData.LastUpdateTime.UniversalTime);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002678 File Offset: 0x00000878
		internal static SubscriptionSubmissionResult SubmitWorkItem(AggregationWorkItem workItem)
		{
			SubscriptionSubmissionResult result;
			lock (AggregationComponent.instance.syncRoot)
			{
				if (AggregationComponent.instance.scheduler != null)
				{
					result = AggregationComponent.instance.scheduler.SubmitWorkItem(workItem);
				}
				else
				{
					result = SubscriptionSubmissionResult.EdgeTransportStopped;
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026DC File Offset: 0x000008DC
		internal static void RecordRemoteServerLatency(string remoteServerName, long remoteServerLatency)
		{
			AggregationComponent.instance.remoteServerHealthManager.RecordRemoteServerLatency(remoteServerName, remoteServerLatency);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026EF File Offset: 0x000008EF
		internal static RemoteServerHealthState CalculateRemoteServerHealth(string remoteServerName, bool isPartnerRemoteServer)
		{
			return AggregationComponent.instance.remoteServerHealthManager.CalculateRemoteServerHealth(remoteServerName, isPartnerRemoteServer);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002704 File Offset: 0x00000904
		private void Shutdown()
		{
			this.syncLogSession.LogInformation((TSLID)441UL, AggregationComponent.diag, (long)this.GetHashCode(), "AggregationComponent Shutdown.", new object[0]);
			lock (this.syncRoot)
			{
				if (this.syncDiagnostics != null)
				{
					this.syncDiagnostics.Unregister();
					this.syncDiagnostics = null;
				}
				if (this.subscriptionSubmit != null)
				{
					this.subscriptionSubmit.Stop();
					this.subscriptionSubmit = null;
				}
				if (this.scheduler != null)
				{
					this.scheduler.Unload();
					this.scheduler = null;
				}
				if (this.remoteServerHealthManager != null)
				{
					this.remoteServerHealthManager.Dispose();
					this.remoteServerHealthManager = null;
				}
				SubscriptionAgentManager.Instance.Shutdown();
				SyncStorageProviderFactory.EnableRegistration();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027E4 File Offset: 0x000009E4
		private void StartScheduler()
		{
			this.syncLogSession.LogInformation((TSLID)442UL, AggregationComponent.diag, (long)this.GetHashCode(), "Start the scheduler.", new object[0]);
			lock (this.syncRoot)
			{
				if (this.scheduler != null)
				{
					this.scheduler.Start(this.transportSyncPaused);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002864 File Offset: 0x00000A64
		private void StopScheduler()
		{
			this.syncLogSession.LogInformation((TSLID)443UL, AggregationComponent.diag, (long)this.GetHashCode(), "Stop the scheduler.", new object[0]);
			lock (this.syncRoot)
			{
				if (this.scheduler != null)
				{
					this.scheduler.Stop();
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028E0 File Offset: 0x00000AE0
		private void PauseScheduler()
		{
			this.syncLogSession.LogInformation((TSLID)325UL, AggregationComponent.diag, (long)this.GetHashCode(), "Pause the scheduler.", new object[0]);
			lock (this.syncRoot)
			{
				if (this.scheduler != null)
				{
					this.scheduler.Pause();
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000295C File Offset: 0x00000B5C
		private void StartRpcServer()
		{
			FileSecurity fileSecurity = new FileSecurity();
			IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 631, "StartRpcServer", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Worker\\Core\\AggregationComponent.cs");
			SecurityIdentifier exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
			FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetAccessRule(fileSystemAccessRule);
			this.exchangeServerSddl = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			fileSecurity.SetOwner(securityIdentifier);
			this.rpcSddl = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			this.subscriptionSubmit = (SubscriptionSubmissionServer)RpcServerBase.RegisterServer(typeof(SubscriptionSubmissionServer), fileSecurity, 1, false, (uint)(Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions / 2));
			this.subscriptionSubmit.Start();
			this.syncLogSession.LogDebugging((TSLID)444UL, AggregationComponent.diag, (long)this.GetHashCode(), "RPC Server started with exchangerServerSDDL {0}, rpcSDDL {1}.", new object[]
			{
				this.exchangeServerSddl,
				this.rpcSddl
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A64 File Offset: 0x00000C64
		private void Initialize()
		{
			this.syncLogSession = AggregationConfiguration.Instance.SyncLog.OpenSession();
			if (!AggregationConfiguration.IsDatacenterMode)
			{
				this.transportSyncEnabled = false;
				return;
			}
			this.transportSyncEnabled = Components.Configuration.LocalServer.TransportServer.TransportSyncEnabled;
			Components.Configuration.LocalServerChanged += this.UpdateTransportServerConfig;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType("Microsoft.Exchange.MailboxTransport.ContentAggregation.FrameworkAggregationConfiguration");
			type.InvokeMember("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.ExactBinding, null, null, null);
			Type type2 = executingAssembly.GetType("Microsoft.Exchange.MailboxTransport.ContentAggregation.FrameworkPerfCounterHandler");
			type2.InvokeMember("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.ExactBinding, null, null, null);
			this.LoadSyncStorageProviders(executingAssembly);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B14 File Offset: 0x00000D14
		private void LoadSyncStorageProviders(Assembly workerAssembly)
		{
			foreach (Type type in workerAssembly.GetTypes())
			{
				Type @interface = type.GetInterface("ISyncStorageProvider");
				if (@interface != null && !type.IsAbstract)
				{
					this.syncLogSession.LogInformation((TSLID)445UL, AggregationComponent.diag, (long)this.GetHashCode(), "Creating SyncStorageProvider for type: " + type.FullName, new object[0]);
					object obj = workerAssembly.CreateInstance(type.FullName);
					ISyncStorageProvider syncStorageProvider = (ISyncStorageProvider)obj;
					SyncStorageProviderFactory.Register(syncStorageProvider, syncStorageProvider.SubscriptionType);
					this.syncLogSession.LogInformation((TSLID)446UL, AggregationComponent.diag, (long)this.GetHashCode(), "Registered {0} with SyncStorageProviderFactory.", new object[]
					{
						type.FullName
					});
				}
			}
			SyncStorageProviderFactory.DisableRegistration();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002C00 File Offset: 0x00000E00
		private void UpdateTransportServerConfig(TransportServerConfiguration updatedTransportServerConfig)
		{
			this.syncLogSession.LogInformation((TSLID)447UL, AggregationComponent.diag, (long)this.GetHashCode(), "Server configuration updated. (Enabled: old={0}, new={1})", new object[]
			{
				this.transportSyncEnabled,
				updatedTransportServerConfig.TransportServer.TransportSyncEnabled
			});
			AggregationConfiguration.Instance.UpdateConfiguration(updatedTransportServerConfig.TransportServer);
			lock (this.syncRoot)
			{
				bool flag2 = this.transportSyncEnabled;
				bool flag3 = updatedTransportServerConfig.TransportServer.TransportSyncEnabled;
				if (flag2 ^ flag3)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartOrStopSchedulerWorker), null);
				}
				if (this.scheduler != null)
				{
					this.scheduler.EnabledAggregationTypes = AggregationConfiguration.Instance.EnabledAggregationTypes;
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void StartOrStopSchedulerWorker(object stateInfo)
		{
			lock (this.syncRoot)
			{
				bool shouldExecute = this.ShouldExecute;
				this.transportSyncEnabled = AggregationConfiguration.Instance.LocalTransportServer.TransportSyncEnabled;
				if (shouldExecute && !this.ShouldExecute)
				{
					this.StopScheduler();
				}
				else if (!shouldExecute && this.ShouldExecute)
				{
					this.StartScheduler();
				}
			}
		}

		// Token: 0x04000001 RID: 1
		public static readonly string Name = "ContentAggregation";

		// Token: 0x04000002 RID: 2
		private static readonly Trace diag = ExTraceGlobals.AggregationComponentTracer;

		// Token: 0x04000003 RID: 3
		private static AggregationComponent instance = new AggregationComponent();

		// Token: 0x04000004 RID: 4
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.EventLogTracer.Category, "MSExchangeTransportSyncWorker");

		// Token: 0x04000005 RID: 5
		private readonly object syncRoot = new object();

		// Token: 0x04000006 RID: 6
		private readonly object instrumentationSyncRoot = new object();

		// Token: 0x04000007 RID: 7
		private AggregationScheduler scheduler;

		// Token: 0x04000008 RID: 8
		private SyncLogSession syncLogSession;

		// Token: 0x04000009 RID: 9
		private bool transportSyncEnabled;

		// Token: 0x0400000A RID: 10
		private bool transportSyncPaused;

		// Token: 0x0400000B RID: 11
		private string exchangeServerSddl;

		// Token: 0x0400000C RID: 12
		private string rpcSddl;

		// Token: 0x0400000D RID: 13
		private SubscriptionSubmissionServer subscriptionSubmit;

		// Token: 0x0400000E RID: 14
		private SyncDiagnostics syncDiagnostics;

		// Token: 0x0400000F RID: 15
		private RemoteServerHealthManager remoteServerHealthManager;

		// Token: 0x04000010 RID: 16
		private string lastLoadCallstack = string.Empty;

		// Token: 0x04000011 RID: 17
		private ServiceState targetRunningState;
	}
}
