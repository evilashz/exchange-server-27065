using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000030 RID: 48
	internal sealed class DatabaseManager : Base, IDisposable
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00007464 File Offset: 0x00005664
		public DatabaseManager(string serviceName, int maxThreads, IEventBasedAssistantType[] eventBasedAssistantTypeArray, ITimeBasedAssistantType[] timeBasedAssistantTypeArray, bool provideRpc)
		{
			SingletonEventLogger.GetSingleton(serviceName);
			if (eventBasedAssistantTypeArray == null && timeBasedAssistantTypeArray == null)
			{
				throw new ArgumentNullException("eventBasedAssistantTypeArray and timeBasedAssistantTypeArray cannot be both null");
			}
			this.serviceName = serviceName;
			Configuration.Initialize(serviceName);
			if (eventBasedAssistantTypeArray != null)
			{
				PerformanceCountersPerAssistantInstance performanceCountersPerAssistantsTotal = new PerformanceCountersPerAssistantInstance(this.serviceName + "-Total", null);
				this.assistantTypes = AssistantType.CreateArray(eventBasedAssistantTypeArray, performanceCountersPerAssistantsTotal);
			}
			this.poisonControlMaster = new PoisonControlMaster(Configuration.ServiceRegistryKeyPath);
			this.throttle = Throttle.CreateParentThrottle("DatabaseManager", maxThreads);
			if (timeBasedAssistantTypeArray != null)
			{
				SystemWorkloadManager.Initialize(AssistantsLog.Instance);
				this.timeBasedDriverManager = new TimeBasedDriverManager(this.throttle, timeBasedAssistantTypeArray, provideRpc);
			}
			this.eventGovernor = new ServerGovernor("DatabaseManagerEvent", new Throttle("DatabaseManagerEvent", maxThreads, this.throttle));
			this.starter = new Starter(new Init(this.Init));
			OnlineDiagnostics.Instance.RegisterDatabaseManager(this);
			base.TracePfd("PFD AIS {0} Database manager: Inialized for Service {1}", new object[]
			{
				30103,
				this.serviceName
			});
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000758A File Offset: 0x0000578A
		internal AssistantType[] AssistantTypes
		{
			get
			{
				return this.assistantTypes;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007592 File Offset: 0x00005792
		internal Throttle Throttle
		{
			get
			{
				return this.throttle;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000759A File Offset: 0x0000579A
		internal ThrottleGovernor EventGovernor
		{
			get
			{
				return this.eventGovernor;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000075A2 File Offset: 0x000057A2
		internal PoisonControlMaster PoisonControlMaster
		{
			get
			{
				return this.poisonControlMaster;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000075AA File Offset: 0x000057AA
		internal TimeBasedDriverManager TimeBasedDriverManager
		{
			get
			{
				return this.timeBasedDriverManager;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000075B2 File Offset: 0x000057B2
		internal string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000075BA File Offset: 0x000057BA
		internal PerformanceCountersPerDatabaseInstance PerformanceCountersTotal
		{
			get
			{
				return this.performanceCountersTotal;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000075C4 File Offset: 0x000057C4
		public void Dispose()
		{
			if (SystemWorkloadManager.Status != WorkloadExecutionStatus.NotInitialized)
			{
				SystemWorkloadManager.Shutdown();
			}
			if (this.exRpcAdmin != null)
			{
				this.exRpcAdmin.Dispose();
				this.exRpcAdmin = null;
			}
			if (this.databaseStatusTimer != null)
			{
				this.databaseStatusTimer.Dispose();
				this.databaseStatusTimer = null;
			}
			if (this.storeService != null)
			{
				this.storeService.Dispose();
				this.storeService = null;
			}
			this.eventGovernor.Dispose();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007636 File Offset: 0x00005836
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Database manager for " + this.serviceName;
			}
			return this.toString;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000765C File Offset: 0x0000585C
		public void Start()
		{
			ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Starting", this);
			AIBreadcrumbs.StartupTrail.Drop("DatabaseManager startup requested. Waiting for lock.");
			lock (this)
			{
				AIBreadcrumbs.StartupTrail.Drop("DatabaseManager startup progressing. Lock acquired.");
				if (this.startState != DatabaseManager.StartState.Stopped)
				{
					ExTraceGlobals.DatabaseManagerTracer.TraceError<DatabaseManager>((long)this.GetHashCode(), "{0}: Start when already started.", this);
				}
				else
				{
					this.starter.Start();
					this.startState = DatabaseManager.StartState.Started;
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000077C0 File Offset: 0x000059C0
		public void Stop()
		{
			this.isStopping = true;
			ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Stopping", this);
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Shutdown requested for DatabaseManager. Waiting for lock.");
			bool flag = false;
			try
			{
				Monitor.Enter(this, ref flag);
				if (this.startState != DatabaseManager.StartState.Initialized)
				{
					return;
				}
				ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Deinitializing...", this);
				AIBreadcrumbs.DatabaseStatusTrail.Drop("Shutdown in progress. Lock acquired");
				if (this.databaseStatusTimer != null)
				{
					this.databaseStatusTimer.Dispose();
					this.databaseStatusTimer = null;
				}
				RpcHangDetector rpcHangDetector = RpcHangDetector.Create();
				rpcHangDetector.InvokeUnderHangDetection(delegate(HangDetector hangDetector)
				{
					foreach (OnlineDatabase onlineDatabase3 in this.onlineDatabases.Values)
					{
						AIBreadcrumbs.ShutdownTrail.Drop("Stopping database: " + onlineDatabase3.ToString());
						onlineDatabase3.RequestStop(rpcHangDetector);
						AIBreadcrumbs.ShutdownTrail.Drop("Finished stoping " + onlineDatabase3.ToString());
					}
					if (this.timeBasedDriverManager != null)
					{
						this.timeBasedDriverManager.RequestStop(rpcHangDetector);
					}
				});
				if (Test.NotifyPhase1ShutdownComplete != null)
				{
					Test.NotifyPhase1ShutdownComplete();
				}
				foreach (OnlineDatabase onlineDatabase in this.onlineDatabases.Values)
				{
					AIBreadcrumbs.ShutdownTrail.Drop("Waiting for Online Database to stop: " + onlineDatabase.ToString());
					onlineDatabase.WaitUntilStopped();
					AIBreadcrumbs.ShutdownTrail.Drop("Done waiting for " + onlineDatabase.ToString());
				}
				if (this.timeBasedDriverManager != null)
				{
					AIBreadcrumbs.DatabaseStatusTrail.Drop("Waiting for TimeBasedDriverManager to stop");
					this.timeBasedDriverManager.WaitUntilStopped();
				}
				AIBreadcrumbs.DatabaseStatusTrail.Drop("Done waiting. Disposing of objects.");
				foreach (OnlineDatabase onlineDatabase2 in this.onlineDatabases.Values)
				{
					onlineDatabase2.Dispose();
				}
				this.onlineDatabases.Clear();
				this.DisposePerformanceCounters();
				this.startState = DatabaseManager.StartState.Stopped;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			this.starter.Stop();
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Shutdown completed at DatabaseManager::Stop().");
			base.TracePfd("PFD AIS {0} {1}: Stopped", new object[]
			{
				31575,
				this
			});
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007A68 File Offset: 0x00005C68
		public void Init()
		{
			ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Really starting", this);
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Init Starting. Waiting on lock.");
			bool flag = false;
			try
			{
				Monitor.Enter(this, ref flag);
				AIBreadcrumbs.DatabaseStatusTrail.Drop("Init startup progressing. Lock acquired.");
				SecurityIdentifier exchangeServersSid = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 514, "Init", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\Assistants\\DatabaseManager.cs");
					exchangeServersSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
				});
				if (exchangeServersSid == null)
				{
					AIBreadcrumbs.DatabaseStatusTrail.Drop("Database Manager unable to contact AD.");
					TransientServerException ex = new TransientServerException(adoperationResult.Exception);
					ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, ADOperationErrorCode, TransientServerException>((long)this.GetHashCode(), "{0}: Unable to contact AD. Will not continue to start at this time. Code: {1}, Exception: {2}", this, adoperationResult.ErrorCode, ex);
					throw ex;
				}
				if (this.startState != DatabaseManager.StartState.Started)
				{
					ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, DatabaseManager.StartState>((long)this.GetHashCode(), "{0}: Init when not started: {1}", this, this.startState);
					return;
				}
				this.performanceCountersTotal = new PerformanceCountersPerDatabaseInstance(this.serviceName + "-Total", null);
				this.performanceCountersTotal.Reset();
				if (this.timeBasedDriverManager != null)
				{
					this.timeBasedDriverManager.Start(exchangeServersSid);
				}
				this.databaseStatusTimer = new Timer(new TimerCallback(this.CheckDatabaseStatusTimerProc));
				this.databaseStatusTimer.Change(TimeSpan.Zero, DatabaseManager.DatabaseStatusPollingInterval);
				base.TracePfd("PFD AIS {0} {1}: Started", new object[]
				{
					23383,
					this
				});
				this.startState = DatabaseManager.StartState.Initialized;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Database manager startup completed.");
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007C20 File Offset: 0x00005E20
		public bool IsStoreServiceRunning()
		{
			if (this.storeService == null)
			{
				this.storeService = new ServiceController("MSExchangeIS");
			}
			else
			{
				this.storeService.Refresh();
			}
			ServiceControllerStatus serviceControllerStatus = ServiceControllerStatus.Stopped;
			Exception ex = null;
			try
			{
				serviceControllerStatus = this.storeService.Status;
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (Win32Exception ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				this.storeService = null;
				ExTraceGlobals.DatabaseManagerTracer.TraceError<DatabaseManager, Exception>((long)this.GetHashCode(), "{0}: cannot open MSExchangeIS service. Exception: {1}", this, ex);
				return false;
			}
			if (serviceControllerStatus != ServiceControllerStatus.Running)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, ServiceControllerStatus>((long)this.GetHashCode(), "{0}: MSExchangeIS service status: {1}", this, serviceControllerStatus);
			}
			return serviceControllerStatus == ServiceControllerStatus.Running;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableDatabaseManager queryableDatabaseManager = queryableObject as QueryableDatabaseManager;
			if (queryableDatabaseManager != null)
			{
				queryableDatabaseManager.StartState = this.startState.ToString();
				queryableDatabaseManager.IsStopping = this.isStopping;
				QueryableThrottle queryableObject2 = new QueryableThrottle();
				this.throttle.ExportToQueryableObject(queryableObject2);
				queryableDatabaseManager.Throttle = queryableObject2;
				QueryableThrottleGovernor queryableThrottleGovernor = new QueryableThrottleGovernor();
				this.eventGovernor.ExportToQueryableObject(queryableThrottleGovernor);
				queryableDatabaseManager.Governor = queryableThrottleGovernor;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007D44 File Offset: 0x00005F44
		public IList<OnlineDatabase> GetOnlineDatabases(Guid? databaseGuid)
		{
			List<OnlineDatabase> list = new List<OnlineDatabase>();
			lock (this)
			{
				foreach (KeyValuePair<Guid, OnlineDatabase> keyValuePair in this.onlineDatabases)
				{
					if (databaseGuid == null || keyValuePair.Value.DatabaseInfo.Guid == databaseGuid.Value)
					{
						list.Add(keyValuePair.Value);
					}
				}
			}
			return list;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public List<QueryableOnlineDatabase> GetQueryableOnlineDatabases(Guid? databaseGuid)
		{
			List<QueryableOnlineDatabase> list = new List<QueryableOnlineDatabase>();
			lock (this)
			{
				foreach (KeyValuePair<Guid, OnlineDatabase> keyValuePair in this.onlineDatabases)
				{
					if (databaseGuid == null || keyValuePair.Value.DatabaseInfo.Guid == databaseGuid.Value)
					{
						QueryableOnlineDatabase queryableOnlineDatabase = new QueryableOnlineDatabase();
						keyValuePair.Value.ExportToQueryableObject(queryableOnlineDatabase);
						list.Add(queryableOnlineDatabase);
					}
				}
			}
			return list;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007EB0 File Offset: 0x000060B0
		internal static bool IsOnlineDatabase(MdbStatus mdbStatus)
		{
			return (mdbStatus.Status & MdbStatusFlags.InRecoverySG) == MdbStatusFlags.Offline && (mdbStatus.Status & MdbStatusFlags.Online) != MdbStatusFlags.Offline;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007ECC File Offset: 0x000060CC
		private static bool IsOnlineDatabase(Guid databaseGuid, MdbStatus[] databases)
		{
			foreach (MdbStatus mdbStatus in databases)
			{
				if (mdbStatus.MdbGuid == databaseGuid)
				{
					return DatabaseManager.IsOnlineDatabase(mdbStatus);
				}
			}
			return false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007F40 File Offset: 0x00006140
		private void CheckDatabaseStatusTimerProc(object state)
		{
			if (!Monitor.TryEnter(this.checkDatabaseStatusLock))
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Currently unable to CheckDatabaseStatus -- we're busy", this);
				return;
			}
			try
			{
				RpcHangDetector rpcHangDetector = RpcHangDetector.Create();
				rpcHangDetector.InvokeUnderHangDetection(delegate(HangDetector hangDetector)
				{
					AIBreadcrumbs.DatabaseStatusTrail.Drop("Checking Database Status. Waiting on lock..");
					this.CheckDatabaseStatus(rpcHangDetector);
					AIBreadcrumbs.DatabaseStatusTrail.Drop("Exiting database status check.");
				});
				if (rpcHangDetector.HangsDetected > 0)
				{
					ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, DateTime>((long)this.GetHashCode(), "{0}: The DatabaseStatus check finally returned. It was invoked at {1} (Utc).", this, rpcHangDetector.InvokeTime);
					base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseStatusThreadResumed, null, new object[]
					{
						rpcHangDetector.InvokeTime.ToLocalTime()
					});
				}
			}
			finally
			{
				Monitor.Exit(this.checkDatabaseStatusLock);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000823C File Offset: 0x0000643C
		private void CheckDatabaseStatus(HangDetector hangDetector)
		{
			try
			{
				long timestamp = Stopwatch.GetTimestamp();
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(2370186557U, ref timestamp);
				}
				this.performanceCountersTotal.ElapsedTimeSinceLastDatabaseStatusUpdateAttempt.RawValue = timestamp;
				lock (this)
				{
					if (this.startState != DatabaseManager.StartState.Initialized)
					{
						ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: CheckDatabaseStatus called when not initialized.", this);
						return;
					}
					AIBreadcrumbs.DatabaseStatusTrail.Drop("Checking if Store is running.");
					if (!this.IsStoreServiceRunning())
					{
						ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager>((long)this.GetHashCode(), "{0}: Store is not running, cannot check status of databases. Assuming all databases are dismounted.", this);
						this.RemoveAllOnlineDatabases(hangDetector);
						return;
					}
					this.CheckForRestart(hangDetector);
				}
				base.CatchMeIfYouCan(delegate
				{
					if (this.exRpcAdmin == null)
					{
						AIBreadcrumbs.DatabaseStatusTrail.Drop("Creating ExRpcAdmin.");
						this.exRpcAdmin = ExRpcAdmin.Create("Client=EBA", null, null, null, null);
					}
					MdbStatus[] array = this.GetOnlineDatabases();
					lock (this)
					{
						List<Guid> list = new List<Guid>();
						foreach (Guid guid in this.onlineDatabases.Keys)
						{
							if (!DatabaseManager.IsOnlineDatabase(guid, array))
							{
								list.Add(guid);
							}
						}
						foreach (Guid databaseGuid in list)
						{
							this.RemoveDatabase(databaseGuid, hangDetector);
						}
						foreach (MdbStatus mdbStatus in array)
						{
							if (this.isStopping)
							{
								break;
							}
							if (!this.onlineDatabases.ContainsKey(mdbStatus.MdbGuid))
							{
								this.TracePfd("PFD AIS {0} {1}: found new online database '{2}'", new object[]
								{
									17239,
									this,
									mdbStatus.MdbName
								});
								AIBreadcrumbs.DatabaseStatusTrail.Drop("Adding database " + mdbStatus.MdbGuid);
								DatabaseInfo databaseInfo = this.TryCreateDatabaseInfo(mdbStatus);
								if (databaseInfo != null)
								{
									this.AttemptAddDatabase(databaseInfo);
								}
							}
						}
					}
				}, "DatabaseManager");
			}
			catch (AIException ex)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceError<DatabaseManager, AIException>((long)this.GetHashCode(), "{0}: Failed to process databases. Exception={1}", this, ex);
				base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseManagerTransientFailure, ex.ToString(), new object[]
				{
					ex.ToString()
				});
				if (this.exRpcAdmin != null)
				{
					this.exRpcAdmin.Dispose();
					this.exRpcAdmin = null;
				}
				if (!(ex is AITransientException))
				{
					this.RemoveAllOnlineDatabases(hangDetector);
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000083D0 File Offset: 0x000065D0
		private MdbStatus[] GetOnlineDatabases()
		{
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Requesting list of databases.");
			MdbStatus[] array = this.exRpcAdmin.ListMdbStatus(false);
			List<MdbStatus> list = new List<MdbStatus>();
			foreach (MdbStatus mdbStatus in array)
			{
				if (this.isStopping)
				{
					return new MdbStatus[0];
				}
				if (DatabaseManager.IsOnlineDatabase(mdbStatus))
				{
					ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, string>((long)this.GetHashCode(), "{0}: found online database '{1}'", this, mdbStatus.MdbName);
					list.Add(mdbStatus);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000084FC File Offset: 0x000066FC
		private DatabaseInfo TryCreateDatabaseInfo(MdbStatus mdbStatus)
		{
			DatabaseInfo databaseInfo = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					Exception ex2 = null;
					try
					{
						databaseInfo = new DatabaseInfo(mdbStatus.MdbGuid, mdbStatus.MdbName, mdbStatus.MdbLegacyDN, (mdbStatus.Status & MdbStatusFlags.IsPublic) != MdbStatusFlags.Offline);
					}
					catch (LocalServerNotFoundException ex3)
					{
						ex2 = ex3;
					}
					catch (DataValidationException ex4)
					{
						ex2 = ex4;
					}
					catch (ArgumentException ex5)
					{
						ex2 = ex5;
					}
					if (ex2 != null)
					{
						throw new TransientDatabaseException(ex2);
					}
				}, "DatabaseManager");
			}
			catch (AIException ex)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceError<DatabaseManager, string, AIException>((long)this.GetHashCode(), "{0}: Failed to create DatabaseInfo for database {1}. Exception={2}", this, mdbStatus.MdbName, ex);
				base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseManagerTransientFailure, ex.ToString(), new object[]
				{
					ex.ToString()
				});
			}
			return databaseInfo;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000859C File Offset: 0x0000679C
		private void RemoveAllOnlineDatabases(HangDetector hangDetector)
		{
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Removing all online databases.");
			List<Guid> list = new List<Guid>();
			lock (this)
			{
				list.AddRange(this.onlineDatabases.Keys);
				foreach (Guid databaseGuid in list)
				{
					this.RemoveDatabase(databaseGuid, hangDetector);
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008638 File Offset: 0x00006838
		private void CheckForRestart(HangDetector hangDetector)
		{
			List<Guid> list = new List<Guid>();
			foreach (KeyValuePair<Guid, OnlineDatabase> keyValuePair in this.onlineDatabases)
			{
				if (keyValuePair.Value.RestartRequired)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (Guid databaseGuid in list)
			{
				this.RemoveDatabase(databaseGuid, hangDetector);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008710 File Offset: 0x00006910
		private void AttemptAddDatabase(DatabaseInfo databaseInfo)
		{
			OnlineDatabase onlineDatabase = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					onlineDatabase = new OnlineDatabase(databaseInfo, this);
					onlineDatabase.Start();
				}, "DatabaseManager");
			}
			catch (AIException ex)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceError<DatabaseManager, AIException>((long)this.GetHashCode(), "{0}: Failed to start database. Exception={1}", this, ex);
				base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseManagerTransientFailure, ex.ToString(), new object[]
				{
					ex.ToString()
				});
				if (onlineDatabase != null)
				{
					onlineDatabase.Dispose();
				}
				return;
			}
			this.onlineDatabases.Add(databaseInfo.Guid, onlineDatabase);
			base.LogEvent(databaseInfo.IsPublic ? AssistantsEventLogConstants.Tuple_DatabaseManagerStartedPublicDatabase : AssistantsEventLogConstants.Tuple_DatabaseManagerStartedPrivateDatabase, null, new object[]
			{
				databaseInfo.DisplayName
			});
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008810 File Offset: 0x00006A10
		private void RemoveDatabase(Guid databaseGuid, HangDetector hangDetector)
		{
			AIBreadcrumbs.DatabaseStatusTrail.Drop("Removing database " + databaseGuid);
			OnlineDatabase onlineDatabase = this.onlineDatabases[databaseGuid];
			hangDetector.DatabaseName = ((onlineDatabase == null || onlineDatabase.DatabaseInfo == null) ? null : onlineDatabase.DatabaseInfo.DisplayName);
			this.onlineDatabases.Remove(databaseGuid);
			try
			{
				onlineDatabase.Stop(hangDetector);
				base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseManagerStoppedDatabase, null, new object[]
				{
					onlineDatabase.DatabaseInfo.DisplayName
				});
			}
			finally
			{
				onlineDatabase.Dispose();
				hangDetector.DatabaseName = null;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000088BC File Offset: 0x00006ABC
		private void DisposePerformanceCounters()
		{
			if (this.performanceCountersTotal != null)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceDebug<DatabaseManager, string>((long)this.GetHashCode(), "{0}: Removing performance counters instance {1}", this, this.performanceCountersTotal.Name);
				this.performanceCountersTotal.Reset();
				this.performanceCountersTotal.Close();
				this.performanceCountersTotal.Remove();
				this.performanceCountersTotal = null;
			}
		}

		// Token: 0x04000147 RID: 327
		private const string DatabaseManagerName = "DatabaseManager";

		// Token: 0x04000148 RID: 328
		private static readonly TimeSpan DatabaseStatusPollingInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000149 RID: 329
		private AssistantType[] assistantTypes;

		// Token: 0x0400014A RID: 330
		private Dictionary<Guid, OnlineDatabase> onlineDatabases = new Dictionary<Guid, OnlineDatabase>();

		// Token: 0x0400014B RID: 331
		private ExRpcAdmin exRpcAdmin;

		// Token: 0x0400014C RID: 332
		private Timer databaseStatusTimer;

		// Token: 0x0400014D RID: 333
		private Starter starter;

		// Token: 0x0400014E RID: 334
		private TimeBasedDriverManager timeBasedDriverManager;

		// Token: 0x0400014F RID: 335
		private Throttle throttle;

		// Token: 0x04000150 RID: 336
		private ThrottleGovernor eventGovernor;

		// Token: 0x04000151 RID: 337
		private string serviceName;

		// Token: 0x04000152 RID: 338
		private PoisonControlMaster poisonControlMaster;

		// Token: 0x04000153 RID: 339
		private string toString;

		// Token: 0x04000154 RID: 340
		private PerformanceCountersPerDatabaseInstance performanceCountersTotal;

		// Token: 0x04000155 RID: 341
		private ServiceController storeService;

		// Token: 0x04000156 RID: 342
		private object checkDatabaseStatusLock = new object();

		// Token: 0x04000157 RID: 343
		private DatabaseManager.StartState startState;

		// Token: 0x04000158 RID: 344
		private bool isStopping;

		// Token: 0x02000031 RID: 49
		private enum StartState
		{
			// Token: 0x0400015A RID: 346
			Stopped,
			// Token: 0x0400015B RID: 347
			Started,
			// Token: 0x0400015C RID: 348
			Initialized
		}
	}
}
