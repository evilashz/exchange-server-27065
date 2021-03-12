using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200012D RID: 301
	internal class SyncMonitorEventTracker
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public SyncMonitorEventTracker(SyncJob syncJob) : this(syncJob.CurrentWorkItem.ExternalIdentity, syncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), syncJob.CurrentWorkItem.TryCount + 1, syncJob.SyncAgentContext.SyncAgentConfig.PolicySyncSLA, syncJob.SyncAgentContext.MonitorProvider, syncJob.CurrentWorkItem.FirstChangeArriveUTC, syncJob.CurrentWorkItem.LastChangeArriveUTC, syncJob.CurrentWorkItem.ExecuteTimeUTC, syncJob.SyncAgentContext.PerfCounterProvider, syncJob.Errors, null)
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001C358 File Offset: 0x0001A558
		internal SyncMonitorEventTracker(string notificationId, string tenantId, int tryCount, TimeSpan policySyncLSA, IMonitoringNotification monitorProvider, DateTime firstNotificationArriveUtc, DateTime lastNotificationArriveUtc, DateTime currentWorkItemScheduleUtc, PerfCounterProvider perfCounterProvider, IEnumerable<SyncAgentExceptionBase> errors, Func<TimeSpan, TimeSpan> getLatencyValueDelegate)
		{
			this.notificationId = notificationId;
			this.tenantId = tenantId;
			this.tryCount = tryCount;
			this.policySyncLSA = policySyncLSA;
			this.monitorProvider = monitorProvider;
			this.firstNotificationArriveUtc = firstNotificationArriveUtc;
			this.lastNotificationArriveUtc = lastNotificationArriveUtc;
			this.currentWorkItemScheduleUtc = currentWorkItemScheduleUtc;
			this.getLatencyValueDelegate = getLatencyValueDelegate;
			this.ObjectSyncLatencyTable = new Dictionary<ConfigurationObjectType, PolicySyncLatencyInformation>();
			this.ObjectSyncStartTimeTable = new Dictionary<ConfigurationObjectType, Dictionary<LatencyType, DateTime>>();
			this.NonObjectSyncLatencyTable = new Dictionary<LatencyType, KeyValuePair<DateTime, TimeSpan>>
			{
				{
					LatencyType.Initialization,
					default(KeyValuePair<DateTime, TimeSpan>)
				},
				{
					LatencyType.TenantInfo,
					default(KeyValuePair<DateTime, TimeSpan>)
				},
				{
					LatencyType.PersistentQueue,
					default(KeyValuePair<DateTime, TimeSpan>)
				}
			};
			this.FailureTable = new Dictionary<Guid, PolicySyncFailureInformation>();
			this.perfCounterProvider = perfCounterProvider;
			this.errors = errors;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001C41E File Offset: 0x0001A61E
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0001C426 File Offset: 0x0001A626
		internal Dictionary<ConfigurationObjectType, PolicySyncLatencyInformation> ObjectSyncLatencyTable { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001C42F File Offset: 0x0001A62F
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001C437 File Offset: 0x0001A637
		internal Dictionary<ConfigurationObjectType, Dictionary<LatencyType, DateTime>> ObjectSyncStartTimeTable { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001C440 File Offset: 0x0001A640
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0001C448 File Offset: 0x0001A648
		internal Dictionary<LatencyType, KeyValuePair<DateTime, TimeSpan>> NonObjectSyncLatencyTable { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001C451 File Offset: 0x0001A651
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001C459 File Offset: 0x0001A659
		internal Dictionary<Guid, PolicySyncFailureInformation> FailureTable { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001C462 File Offset: 0x0001A662
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001C46A File Offset: 0x0001A66A
		internal TimeSpan NotificationPickUpDelay { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001C473 File Offset: 0x0001A673
		internal TimeSpan TotalProcessTime
		{
			get
			{
				return DateTime.UtcNow - this.firstNotificationArriveUtc;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001C485 File Offset: 0x0001A685
		internal TimeSpan TotalProcessTimeForCurrentSyncCycle
		{
			get
			{
				return DateTime.UtcNow - this.lastNotificationArriveUtc;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001C497 File Offset: 0x0001A697
		internal TimeSpan ExecutionDelayTime
		{
			get
			{
				return this.currentWorkItemScheduleUtc - this.lastNotificationArriveUtc + this.NotificationPickUpDelay;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001C4B5 File Offset: 0x0001A6B5
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		internal long WsCallNumber { get; private set; }

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
		public void TriggerAlertIfNecessary()
		{
			foreach (PolicySyncFailureInformation policySyncFailureInformation in this.FailureTable.Values)
			{
				this.monitorProvider.PublishEvent("UnifiedPolicySync.PermanentError", this.tenantId, this.GetFailureContext(policySyncFailureInformation), policySyncFailureInformation.LastException);
			}
			if (this.TotalProcessTime > this.policySyncLSA)
			{
				this.monitorProvider.PublishEvent("UnifiedPolicySync.PolicySyncTimeExceededError", this.tenantId, this.GetLatencyContext(), null);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C56C File Offset: 0x0001A76C
		public void MarkNotificationPickedUp()
		{
			this.NotificationPickUpDelay = DateTime.UtcNow - this.currentWorkItemScheduleUtc;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001C584 File Offset: 0x0001A784
		public void TrackLatencyWrapper(LatencyType latencyType, Action action)
		{
			this.TrackLatencyWrapper(latencyType, ConfigurationObjectType.Policy, action, true);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C590 File Offset: 0x0001A790
		public void TrackLatencyWrapper(LatencyType latencyType, ConfigurationObjectType objectType, Action action, bool alwaysMarkEnd = true)
		{
			int num = 0;
			this.TrackLatencyWrapper(latencyType, objectType, ref num, action, alwaysMarkEnd, false);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public void TrackLatencyWrapper(LatencyType latencyType, ConfigurationObjectType objectType, ref int deltaObjectCount, Action action, bool alwaysMarkEnd = true, bool markEndOnly = false)
		{
			if (!markEndOnly)
			{
				this.MarkSyncOperationStart(latencyType, objectType);
			}
			if (alwaysMarkEnd)
			{
				try
				{
					action();
					return;
				}
				finally
				{
					this.MarkSyncOperationEnd(latencyType, objectType, deltaObjectCount);
				}
			}
			try
			{
				action();
			}
			catch
			{
				this.MarkSyncOperationEnd(latencyType, objectType, deltaObjectCount);
				throw;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C614 File Offset: 0x0001A814
		public void ReportTenantLevelFailure(Exception exception)
		{
			this.InternalReportFailure(exception, Guid.Empty, null);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C636 File Offset: 0x0001A836
		public void ReportObjectLevelFailure(Exception exception, ConfigurationObjectType objectType, Guid? policyId)
		{
			this.InternalReportFailure(exception, (policyId != null) ? policyId.Value : Guid.Empty, new ConfigurationObjectType?(objectType));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public void PublishPerfData()
		{
			this.perfCounterProvider.IncrementBy("Total Processing Time Per Sync Request", this.TotalProcessTimeForCurrentSyncCycle.Ticks, "Total Processing Time Per Sync Request Base");
			this.perfCounterProvider.IncrementBy("Execution Delay Time Per Sync Request", this.ExecutionDelayTime.Ticks, "Execution Delay Time Per Sync Request Base");
			this.perfCounterProvider.IncrementBy("Initialization Time Per Sync Request", this.NonObjectSyncLatencyTable[LatencyType.Initialization].Value.Ticks, "Initialization Time Per Sync Request Base");
			foreach (ConfigurationObjectType key in new ConfigurationObjectType[]
			{
				ConfigurationObjectType.Policy,
				ConfigurationObjectType.Rule,
				ConfigurationObjectType.Binding,
				ConfigurationObjectType.Association
			})
			{
				long incrementValue = 0L;
				long incrementValue2 = 0L;
				if (this.ObjectSyncLatencyTable.ContainsKey(key))
				{
					incrementValue = this.ObjectSyncLatencyTable[key].Latencies[LatencyType.FfoWsCall].Ticks;
					incrementValue2 = this.ObjectSyncLatencyTable[key].Latencies[LatencyType.CrudMgr].Ticks;
				}
				this.perfCounterProvider.IncrementBy(PerfCounters.PolicyObjectWsPerfCounters[key].Key, incrementValue, PerfCounters.PolicyObjectWsPerfCounters[key].Value);
				this.perfCounterProvider.IncrementBy(PerfCounters.PolicyObjectCrudMgrPerfCounters[key].Key, incrementValue2, PerfCounters.PolicyObjectCrudMgrPerfCounters[key].Value);
			}
			this.perfCounterProvider.IncrementBy("TenantInfo Processing Time Per Sync Request", this.NonObjectSyncLatencyTable[LatencyType.TenantInfo].Value.Ticks, "TenantInfo Processing Time Per Sync Request Base");
			this.perfCounterProvider.IncrementBy("Persistent Queue Processing Time Per Sync Request", this.NonObjectSyncLatencyTable[LatencyType.PersistentQueue].Value.Ticks, "Persistent Queue Processing Time Per Sync Request Base");
			this.perfCounterProvider.Increment("Processed Sync Request Number Per Second");
			this.perfCounterProvider.Increment("Processed Sync Request Number");
			if (!this.errors.Any<SyncAgentExceptionBase>())
			{
				this.perfCounterProvider.Increment("Successful Sync Request Number Per Second");
				this.perfCounterProvider.Increment("Successful Sync Request Number");
			}
			else
			{
				int num = this.errors.Count((SyncAgentExceptionBase p) => p is SyncAgentTransientException && !(p is PolicyConfigProviderTransientException));
				if (num > 0)
				{
					this.perfCounterProvider.IncrementBy("Policy Sync Ws Call Transient Error Number Per Second", (long)num);
					this.perfCounterProvider.IncrementBy("Policy Sync Ws Call Transient Error Number", (long)num);
				}
				int num2 = this.errors.Count((SyncAgentExceptionBase p) => p is PolicyConfigProviderTransientException);
				if (num2 > 0)
				{
					this.perfCounterProvider.IncrementBy("Policy Sync CrudMgr Transient Error Number Per Second", (long)num2);
					this.perfCounterProvider.IncrementBy("Policy Sync CrudMgr Transient Error Number", (long)num2);
				}
				int num3 = this.errors.Count((SyncAgentExceptionBase p) => p is SyncAgentPermanentException && !(p.InnerException is GrayException) && !(p is PolicyConfigProviderPermanentException));
				if (num3 > 0)
				{
					this.perfCounterProvider.IncrementBy("Policy Sync Ws Call Permanent Error Number Per Second", (long)num3);
					this.perfCounterProvider.IncrementBy("Policy Sync Ws Call Permanent Error Number", (long)num3);
				}
				int num4 = this.errors.Count((SyncAgentExceptionBase p) => p is PolicyConfigProviderPermanentException);
				if (num4 > 0)
				{
					this.perfCounterProvider.IncrementBy("Policy Sync CrudMgr Permanent Error Number Per Second", (long)num4);
					this.perfCounterProvider.IncrementBy("Policy Sync CrudMgr Permanent Error Number", (long)num4);
				}
			}
			if (this.tryCount > 1)
			{
				this.perfCounterProvider.Increment("Sync Request Retry Number Per Second");
				this.perfCounterProvider.Increment("Sync Request Retry Number");
			}
			this.perfCounterProvider.IncrementBy("Ws Call Number Per Sync Request", this.WsCallNumber, "Ws Call Number Per Sync Request Base");
			this.perfCounterProvider.Increment("Tenant Number Per Sync Request", "Tenant Number Per Sync Request Base");
			long incrementValue3 = (long)this.ObjectSyncLatencyTable.Values.Sum((PolicySyncLatencyInformation p) => p.Count);
			this.perfCounterProvider.IncrementBy("Object Number Per Sync Request", incrementValue3, "Object Number Per Sync Request Base");
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		private void InitializeForType(ConfigurationObjectType objectType)
		{
			if (!this.ObjectSyncStartTimeTable.ContainsKey(objectType))
			{
				this.ObjectSyncStartTimeTable[objectType] = new Dictionary<LatencyType, DateTime>();
				this.ObjectSyncLatencyTable[objectType] = new PolicySyncLatencyInformation(objectType, 0, this.getLatencyValueDelegate);
				this.ObjectSyncLatencyTable[objectType].Latencies[LatencyType.FfoWsCall] = TimeSpan.Zero;
				this.ObjectSyncLatencyTable[objectType].Latencies[LatencyType.CrudMgr] = TimeSpan.Zero;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001CB64 File Offset: 0x0001AD64
		private void MarkSyncOperationStart(LatencyType latencyType, ConfigurationObjectType objectType)
		{
			DateTime utcNow = DateTime.UtcNow;
			switch (latencyType)
			{
			case LatencyType.Initialization:
			case LatencyType.TenantInfo:
			case LatencyType.PersistentQueue:
				this.NonObjectSyncLatencyTable[latencyType] = new KeyValuePair<DateTime, TimeSpan>(utcNow, this.NonObjectSyncLatencyTable[latencyType].Value);
				return;
			case LatencyType.FfoWsCall:
			case LatencyType.CrudMgr:
				this.InitializeForType(objectType);
				this.ObjectSyncStartTimeTable[objectType][latencyType] = utcNow;
				if (latencyType == LatencyType.FfoWsCall)
				{
					this.WsCallNumber += 1L;
					return;
				}
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		private void MarkSyncOperationEnd(LatencyType latencyType, ConfigurationObjectType objectType, int deltaObjectCount = 0)
		{
			DateTime utcNow = DateTime.UtcNow;
			switch (latencyType)
			{
			case LatencyType.Initialization:
			case LatencyType.TenantInfo:
			case LatencyType.PersistentQueue:
			{
				TimeSpan value = this.NonObjectSyncLatencyTable[latencyType].Value + this.GetLatencyValue(utcNow - this.NonObjectSyncLatencyTable[latencyType].Key);
				this.NonObjectSyncLatencyTable[latencyType] = new KeyValuePair<DateTime, TimeSpan>(this.NonObjectSyncLatencyTable[latencyType].Key, value);
				return;
			}
			case LatencyType.FfoWsCall:
			case LatencyType.CrudMgr:
			{
				this.ObjectSyncLatencyTable[objectType].Count += deltaObjectCount;
				Dictionary<LatencyType, TimeSpan> latencies;
				(latencies = this.ObjectSyncLatencyTable[objectType].Latencies)[latencyType] = latencies[latencyType] + this.GetLatencyValue(utcNow - this.ObjectSyncStartTimeTable[objectType][latencyType]);
				return;
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		private string GetLatencyContext()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PolicySyncLatencyInformation policySyncLatencyInformation in this.ObjectSyncLatencyTable.Values)
			{
				stringBuilder.Append(policySyncLatencyInformation.ToString());
			}
			return string.Format("NotificationId={0};Timestamp={1}\r\nLatencies:\r\nTotalProcessTime={2},TryCount={3}\r\nCurrentCycle:NotifyPickUpDelay={4};Initialization={5};{6}", new object[]
			{
				this.notificationId,
				DateTime.UtcNow,
				(int)this.TotalProcessTime.TotalSeconds,
				this.tryCount,
				(int)this.NotificationPickUpDelay.TotalSeconds,
				(int)this.NonObjectSyncLatencyTable[LatencyType.Initialization].Value.TotalSeconds,
				stringBuilder.ToString()
			});
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		private string GetFailureContext(PolicySyncFailureInformation info)
		{
			return string.Format("NotificationId={0}\r\nTimestamp={1}\r\n{2}", this.notificationId, DateTime.UtcNow, info.ToString());
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001CE28 File Offset: 0x0001B028
		private void InternalReportFailure(Exception exception, Guid policyId, ConfigurationObjectType? objectType)
		{
			if (!this.FailureTable.ContainsKey(policyId))
			{
				this.FailureTable[policyId] = new PolicySyncFailureInformation(policyId);
			}
			if (objectType != null)
			{
				this.FailureTable[policyId].ObjectTypes.Add(objectType.Value);
			}
			this.FailureTable[policyId].LastException = exception;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001CE8E File Offset: 0x0001B08E
		private TimeSpan GetLatencyValue(TimeSpan latency)
		{
			if (this.getLatencyValueDelegate == null)
			{
				return latency;
			}
			return this.getLatencyValueDelegate(latency);
		}

		// Token: 0x04000488 RID: 1160
		private readonly int tryCount;

		// Token: 0x04000489 RID: 1161
		private readonly TimeSpan policySyncLSA;

		// Token: 0x0400048A RID: 1162
		private readonly string tenantId;

		// Token: 0x0400048B RID: 1163
		private readonly string notificationId;

		// Token: 0x0400048C RID: 1164
		private readonly DateTime firstNotificationArriveUtc;

		// Token: 0x0400048D RID: 1165
		private readonly DateTime lastNotificationArriveUtc;

		// Token: 0x0400048E RID: 1166
		private readonly DateTime currentWorkItemScheduleUtc;

		// Token: 0x0400048F RID: 1167
		private readonly IMonitoringNotification monitorProvider;

		// Token: 0x04000490 RID: 1168
		private readonly PerfCounterProvider perfCounterProvider;

		// Token: 0x04000491 RID: 1169
		private readonly IEnumerable<SyncAgentExceptionBase> errors;

		// Token: 0x04000492 RID: 1170
		private readonly Func<TimeSpan, TimeSpan> getLatencyValueDelegate;
	}
}
