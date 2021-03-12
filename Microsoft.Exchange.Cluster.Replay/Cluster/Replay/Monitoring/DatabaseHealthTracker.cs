using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.Monitoring.Client;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C3 RID: 451
	internal class DatabaseHealthTracker : TimerComponent, IDatabaseHealthTracker
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004810C File Offset: 0x0004630C
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DatabaseHealthTrackerTracer;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00048114 File Offset: 0x00046314
		public string PersistedFilePath
		{
			get
			{
				if (this.m_persistedFilePath != null)
				{
					return this.m_persistedFilePath;
				}
				lock (this)
				{
					if (this.m_persistedFilePath == null)
					{
						string path = Path.Combine(ExchangeSetupContext.LoggingPath, "Diagnostics", "HAHealthLogs");
						this.m_persistedFilePath = Path.Combine(path, "TrackerHealthInfo.xml");
					}
				}
				return this.m_persistedFilePath;
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00048198 File Offset: 0x00046398
		public DatabaseHealthTracker(IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup) : base(TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds((double)RegistryParameters.MonitoringDHTPeriodicIntervalInSec), "DatabaseHealthTracker")
		{
			this.m_adConfigProvider = adConfigProvider;
			this.m_statusLookup = statusLookup;
			this.m_healthTable = new DbCopyHealthInfoTable(this.PersistedFilePath);
			this.m_pamTracker = new PrimaryRoleTracker();
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00048240 File Offset: 0x00046440
		protected override void TimerCallbackInternal()
		{
			try
			{
				this.Run();
			}
			catch (MonitoringADServiceShuttingDownException arg)
			{
				DatabaseHealthTracker.Tracer.TraceError<MonitoringADServiceShuttingDownException>((long)this.GetHashCode(), "TimerCallbackInternal: Got service shutting down exception when retrieving AD config: {0}", arg);
			}
			catch (MonitoringADConfigException ex)
			{
				DatabaseHealthTracker.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "TimerCallbackInternal: Got exception when retrieving AD config: {0}", ex);
				ReplayCrimsonEvents.DatabaseHealthTrackerError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
			finally
			{
				this.m_initCompleted.Set();
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000482E4 File Offset: 0x000464E4
		protected override void StopInternal()
		{
			base.StopInternal();
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "StopInternal(): DatabaseHealthTracker is being shut down.");
			this.m_initCompleted.Close();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00048318 File Offset: 0x00046518
		private void Run()
		{
			IMonitoringADConfig config = this.m_adConfigProvider.GetConfig(true);
			if (config.ServerRole == MonitoringServerRole.Standalone)
			{
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "DatabaseHealthTracker: Skipping running health checks because local server is not a member of a DAG.");
				return;
			}
			if (config.Dag.ThirdPartyReplication == ThirdPartyReplicationMode.Enabled)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthTracker: Dag '{0}' is in ThirdPartyReplication mode. Shutting down the DatabaseHealthTracker component.", config.Dag.Name);
				base.ChangeTimer(InvokeWithTimeout.InfiniteTimeSpan, InvokeWithTimeout.InfiniteTimeSpan);
				ThreadPool.QueueUserWorkItem(delegate(object stateNotUsed)
				{
					base.Stop();
				});
				return;
			}
			IEnumerable<IADDatabase> expectedDatabases = config.DatabaseMap[config.TargetServerName];
			IEnumerable<CopyStatusClientCachedEntry> copyStatusesByServer = this.m_statusLookup.GetCopyStatusesByServer(config.TargetServerName, expectedDatabases, CopyStatusClientLookupFlags.None);
			this.m_pamTracker.ReportPAMStatus(copyStatusesByServer);
			bool isAMRoleChanged = this.m_pamTracker.IsAMRoleChanged;
			if (isAMRoleChanged)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveManager role changed: Local node {0} the PAM!", this.m_pamTracker.IsPAM ? "*IS*" : "is *NOT*");
			}
			this.m_healthTable.Initialize();
			this.m_initCompleted.Set();
			if (this.m_pamTracker.ShouldTryToBecomePrimary())
			{
				bool flag;
				bool flag2;
				bool latestHealthInfoAcrossDag = this.GetLatestHealthInfoAcrossDag(config, out flag, out flag2);
				if (latestHealthInfoAcrossDag)
				{
					DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "Local node is becoming the Primary role.");
					if (flag2)
					{
						this.m_healthTable.SetCreateTimeIfNecessary(DateTime.UtcNow);
					}
					this.m_pamTracker.BecomePrimary();
					ReplayCrimsonEvents.DHTBecomingPrimaryRole.Log();
				}
			}
			if (this.m_pamTracker.IsPAM && this.m_pamTracker.HasBecomePrimary)
			{
				ReplayCrimsonEvents.DHTRunningAsRole.LogPeriodic<string>(Environment.MachineName, DateTimeHelper.OneDay, "Primary");
			}
			else if (!this.m_pamTracker.IsPAM)
			{
				ReplayCrimsonEvents.DHTRunningAsRole.LogPeriodic<string>(Environment.MachineName, DateTimeHelper.OneDay, "Secondary");
			}
			if (!this.m_pamTracker.IsPAM)
			{
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "Timer thread exiting because the local node is *NOT* the PAM!");
				return;
			}
			if (!this.m_pamTracker.HasBecomePrimary)
			{
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "Timer thread exiting because the local node has not yet become the Primary role!");
				return;
			}
			foreach (AmServerName amServerName in config.AmServerNames)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Running for server '{0}'", amServerName.NetbiosName);
				this.m_healthTable.ReportServerFoundInAD(amServerName);
				IEnumerable<IADDatabase> enumerable = config.DatabaseMap[amServerName];
				this.m_healthTable.ReportDbCopiesFoundInAD(enumerable, amServerName);
				IEnumerable<CopyStatusClientCachedEntry> copyStatusesByServer2 = this.m_statusLookup.GetCopyStatusesByServer(amServerName, enumerable, CopyStatusClientLookupFlags.None);
				this.m_healthTable.ReportDbCopyStatusesFound(config, amServerName, copyStatusesByServer2);
			}
			this.m_healthTable.PossiblyReportObjectsNotFoundInAD(config);
			this.m_healthTable.UpdateAvailabilityRedundancyStates(config);
			this.m_healthTable.SetLastUpdateTime(DateTime.UtcNow);
			TransientErrorInfo.ErrorType errorType;
			if (this.m_publishTableSuppression.ReportSuccessPeriodic(out errorType))
			{
				this.PublishHealthInfoAcrossDag(config);
				Exception ex = this.m_healthTable.PersistHealthInfoToXml();
				if (ex != null)
				{
					DatabaseHealthTracker.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to persist health info to XML file. Error: {0}", ex);
					ReplayCrimsonEvents.DHTPrimaryPersistFailed.LogPeriodic<string, Exception>(Environment.MachineName, DateTimeHelper.OneHour, ex.Message, ex);
					return;
				}
			}
			else
			{
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "Skipping to publish and persist health info locally and to other nodes due to periodic suppression.");
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00048680 File Offset: 0x00046880
		public void UpdateHealthInfo(HealthInfoPersisted healthInfo)
		{
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UpdateHealthInfo() called.");
			this.m_healthTable.UpdateHealthInfo(healthInfo, false);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000486A8 File Offset: 0x000468A8
		public DateTime GetDagHealthInfoUpdateTimeUtc()
		{
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtc() called.");
			ManualOneShotEvent.Result result = this.m_initCompleted.WaitOne(this.InitWaitTimeout);
			if (result == ManualOneShotEvent.Result.WaitTimedOut)
			{
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtc() timed out waiting for initialization to complete.");
				throw new DbHTFirstLookupTimeoutException((int)this.InitWaitTimeout.TotalMilliseconds);
			}
			if (result == ManualOneShotEvent.Result.ShuttingDown)
			{
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtc(): Errorring out since the service is shutting down.");
				throw new DbHTServiceShuttingDownException();
			}
			DateTime lastUpdateTime = this.m_healthTable.GetLastUpdateTime();
			DatabaseHealthTracker.Tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtc() returning '{0}'", lastUpdateTime);
			return lastUpdateTime;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00048750 File Offset: 0x00046950
		public HealthInfoPersisted GetDagHealthInfo()
		{
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetDagHealthInfo() called.");
			ManualOneShotEvent.Result result = this.m_initCompleted.WaitOne(this.InitWaitTimeout);
			if (result == ManualOneShotEvent.Result.WaitTimedOut)
			{
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetDagHealthInfo() timed out waiting for initialization to complete.");
				throw new DbHTFirstLookupTimeoutException((int)this.InitWaitTimeout.TotalMilliseconds);
			}
			if (result == ManualOneShotEvent.Result.ShuttingDown)
			{
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetDagHealthInfo(): Errorring out since the service is shutting down.");
				throw new DbHTServiceShuttingDownException();
			}
			HealthInfoPersisted healthInfo = this.m_healthTable.GetHealthInfo();
			DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GetDagHealthInfo() returning health info serialized structure of update time '{0}'.", healthInfo.LastUpdateTimeUtcStr);
			return healthInfo;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00048814 File Offset: 0x00046A14
		private void PublishHealthInfoAcrossDag(IMonitoringADConfig adConfig)
		{
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "PublishHealthInfoAcrossDag() called.");
			HealthInfoPersisted hiSerializable = this.m_healthTable.ConvertToHealthInfoPersisted();
			this.ExecuteRemoteCallAcrossDagExcludingPAM<bool>("PublishHealthInfoAcrossDag", adConfig, (MonitoringServiceClient client) => client.WPublishDagHealthInfoAsync(hiSerializable));
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00048B24 File Offset: 0x00046D24
		private bool GetLatestHealthInfoAcrossDag(IMonitoringADConfig adConfig, out bool allFailed, out bool isNewPublish)
		{
			DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag() called.");
			allFailed = false;
			isNewPublish = false;
			Task<Tuple<string, DateTime>>[] array = this.ExecuteRemoteCallAcrossDagExcludingPAM<DateTime>("GetDagHealthInfoUpdateTimeUtcAsync", adConfig, (MonitoringServiceClient client) => client.WGetDagHealthInfoUpdateTimeUtcAsync());
			if (array == null)
			{
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): There are no other servers in the DAG to contact. Proceed as Primary role.");
				return true;
			}
			IEnumerable<Task<Tuple<string, DateTime>>> successful = array.GetSuccessful<Task<Tuple<string, DateTime>>>();
			if (successful.Count<Task<Tuple<string, DateTime>>>() == 0)
			{
				allFailed = true;
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): All GetDagHealthInfoUpdateTimeUtcAsync() calls failed!");
				ReplayCrimsonEvents.DHTPrimaryStartupFailedNoResponses.LogPeriodic<string>("GetDagHealthInfoUpdateTimeUtcAsync", DateTimeHelper.OneHour, "GetDagHealthInfoUpdateTimeUtcAsync");
				return false;
			}
			IOrderedEnumerable<Task<Tuple<string, DateTime>>> source = from task in successful
			orderby task.Result.Item2 descending
			select task;
			Task<Tuple<string, DateTime>> task2 = source.First<Task<Tuple<string, DateTime>>>();
			DateTime item = task2.Result.Item2;
			string item2 = task2.Result.Item1;
			DatabaseHealthTracker.Tracer.TraceDebug<string, DateTime>((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): Found server '{0}' with latest update time of '{1}'", item2, item);
			if (item == DateTime.MinValue)
			{
				isNewPublish = true;
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): Setting isNewPublish to 'true'.");
				DatabaseHealthTracker.Tracer.TraceDebug((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): Skipping pulling health table from other nodes.");
				ReplayCrimsonEvents.DHTPrimaryStartupNewTable.Log();
				return true;
			}
			DateTime localUpdateTime = this.m_healthTable.GetLastUpdateTime();
			string text = DateTimeHelper.ToStringInvariantCulture(localUpdateTime);
			if (localUpdateTime >= item)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): Local server has newer health table. Skipping pulling health table from other nodes. Local update time: {0}", text);
				return true;
			}
			TimeSpan timeSpan = DateTimeHelper.SafeSubtract(DateTime.UtcNow, item);
			if (timeSpan > DatabaseHealthTracker.LastUpdateTimeDiffThreshold)
			{
				DatabaseHealthTracker.Tracer.TraceError<DateTime, TimeSpan, TimeSpan>((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): The newest health table was only modified too long ago at '{0}'! updateTimeDiff = '{1}', Threshold = '{2}'", item, timeSpan, DatabaseHealthTracker.LastUpdateTimeDiffThreshold);
				ReplayCrimsonEvents.DHTPrimaryStartupFromFileTooOld.LogPeriodic<DateTime, TimeSpan, TimeSpan>(Environment.MachineName, DateTimeHelper.OneHour, item, timeSpan, DatabaseHealthTracker.LastUpdateTimeDiffThreshold);
			}
			DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): Another server has newer health table. Will attempt to pull in an existing table. Local update time: {0}", text);
			IEnumerable<string> enumerable = from task in source
			let updateTime = task.Result.Item2
			let serverName = task.Result.Item1
			where updateTime > localUpdateTime
			select serverName;
			Task<Tuple<string, HealthInfoPersisted>>[] tasks = this.ExecuteRemoteCallForServers<HealthInfoPersisted>("GetDagHealthInfoAsync", enumerable, (MonitoringServiceClient client) => client.WGetDagHealthInfoAsync());
			IEnumerable<Task<Tuple<string, HealthInfoPersisted>>> successful2 = tasks.GetSuccessful<Task<Tuple<string, HealthInfoPersisted>>>();
			if (successful2.Count<Task<Tuple<string, HealthInfoPersisted>>>() == 0)
			{
				allFailed = true;
				DatabaseHealthTracker.Tracer.TraceError((long)this.GetHashCode(), "GetLatestHealthInfoAcrossDag(): All GetDagHealthInfoAsync() calls failed!");
				ReplayCrimsonEvents.DHTPrimaryStartupFailedNoResponses.LogPeriodic<string>("GetDagHealthInfoAsync", DateTimeHelper.OneHour, "GetDagHealthInfoAsync");
				return false;
			}
			Dictionary<string, HealthInfoPersisted> dictionary = successful2.ToDictionary((Task<Tuple<string, HealthInfoPersisted>> task) => task.Result.Item1, (Task<Tuple<string, HealthInfoPersisted>> task) => task.Result.Item2, StringComparer.OrdinalIgnoreCase);
			foreach (string text2 in enumerable)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "UpdateHealthInfo() called with table from server '{0}'.", text2);
				HealthInfoPersisted healthInfoPersisted = dictionary[text2];
				if (this.m_healthTable.UpdateHealthInfo(healthInfoPersisted, true))
				{
					ReplayCrimsonEvents.DHTPrimaryStartupReplacingTable.Log<string, string, string, string>(text2, healthInfoPersisted.LastUpdateTimeUtcStr, healthInfoPersisted.CreateTimeUtcStr, text);
					break;
				}
			}
			return true;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00048F18 File Offset: 0x00047118
		private Task<Tuple<string, T>>[] ExecuteRemoteCallAcrossDagExcludingPAM<T>(string operationTraceName, IMonitoringADConfig adConfig, Func<MonitoringServiceClient, Task<Tuple<string, T>>> remoteOperation)
		{
			IEnumerable<AmServerName> source = from serverName in adConfig.AmServerNames
			where !serverName.IsLocalComputerName
			select serverName;
			return this.ExecuteRemoteCallForServers<T>(operationTraceName, from server in source
			select server.Fqdn, remoteOperation);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00048F58 File Offset: 0x00047158
		private Task<Tuple<string, T>>[] ExecuteRemoteCallForServers<T>(string operationTraceName, IEnumerable<string> serverFqdnsToContact, Func<MonitoringServiceClient, Task<Tuple<string, T>>> remoteOperation)
		{
			int num = serverFqdnsToContact.Count<string>();
			if (num == 0)
			{
				DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}(): There are no servers to contact. Returning <NULL>.", operationTraceName);
				return null;
			}
			Task<Tuple<string, T>>[] array = new Task<Tuple<string, T>>[num];
			int num2 = 0;
			Task<Tuple<string, T>>[] result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				foreach (string serverName in serverFqdnsToContact)
				{
					MonitoringServiceClient monitoringServiceClient = MonitoringServiceClient.Open(serverName);
					disposeGuard.Add<MonitoringServiceClient>(monitoringServiceClient);
					Task<Tuple<string, T>> task = remoteOperation(monitoringServiceClient);
					array[num2++] = task;
				}
				try
				{
					TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
					if (!Task.WaitAll((Task[])array, timeSpan))
					{
						DatabaseHealthTracker.Tracer.TraceError<string, TimeSpan>((long)this.GetHashCode(), "{0}(): One or more calls timed out after '{1}'.", operationTraceName, timeSpan);
						ReplayCrimsonEvents.DHTRemoteOperationTimedOut.LogPeriodic<string, TimeSpan>(operationTraceName, DateTimeHelper.OneHour, operationTraceName, timeSpan);
					}
					else
					{
						DatabaseHealthTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}(): All calls were issued successfully.", operationTraceName);
					}
				}
				catch (AggregateException ex)
				{
					foreach (Exception ex2 in ex.Flatten().InnerExceptions)
					{
						DatabaseHealthTracker.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "{0}() got Exception: {1}", operationTraceName, ex2);
						ReplayCrimsonEvents.DHTRemoteOperationFailed.LogPeriodic<string, string, Exception>(ex2.Message, DateTimeHelper.OneHour, operationTraceName, ex2.Message, ex2);
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x040006B4 RID: 1716
		private const string PrimaryRole = "Primary";

		// Token: 0x040006B5 RID: 1717
		private const string SecondaryRole = "Secondary";

		// Token: 0x040006B6 RID: 1718
		private const string TrackerDirectoryName = "HAHealthLogs";

		// Token: 0x040006B7 RID: 1719
		private const string TrackerFileName = "TrackerHealthInfo.xml";

		// Token: 0x040006B8 RID: 1720
		private const string InitializeCompletedEventName = "InitializeCompletedEvent";

		// Token: 0x040006B9 RID: 1721
		internal static readonly TimeSpan LastUpdateTimeDiffThreshold = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringDHTInitLastUpdateTimeDiffInSec);

		// Token: 0x040006BA RID: 1722
		private IMonitoringADConfigProvider m_adConfigProvider;

		// Token: 0x040006BB RID: 1723
		private ICopyStatusClientLookup m_statusLookup;

		// Token: 0x040006BC RID: 1724
		private DbCopyHealthInfoTable m_healthTable;

		// Token: 0x040006BD RID: 1725
		private volatile string m_persistedFilePath;

		// Token: 0x040006BE RID: 1726
		private readonly TimeSpan InitWaitTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x040006BF RID: 1727
		private ManualOneShotEvent m_initCompleted = new ManualOneShotEvent("InitializeCompletedEvent");

		// Token: 0x040006C0 RID: 1728
		private PrimaryRoleTracker m_pamTracker;

		// Token: 0x040006C1 RID: 1729
		private TransientErrorInfoPeriodic m_publishTableSuppression = new TransientErrorInfoPeriodic(TimeSpan.Zero, TimeSpan.FromSeconds((double)RegistryParameters.MonitoringDHTPrimaryPublishPeriodicSuppressionInSec), TimeSpan.Zero, TransientErrorInfoPeriodic.InfiniteTimeSpan, TransientErrorInfo.ErrorType.Success);
	}
}
