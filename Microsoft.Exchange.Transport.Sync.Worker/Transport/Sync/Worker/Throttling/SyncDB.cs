using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncDB : SyncResource
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000C478 File Offset: 0x0000A678
		protected SyncDB(Guid databaseGuid, SyncLogSession syncLogSession) : base(syncLogSession, databaseGuid.ToString())
		{
			this.databaseGuid = databaseGuid;
			this.averageRPCLatency = new RunningAverageFloat(SyncDB.NumberOfRPCLatencySamples);
			this.averageDelay = new RunningAverageFloat(SyncDB.NumberOfDelaySamples);
			base.Initialize();
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		protected override int MaxConcurrentWorkInUnknownState
		{
			get
			{
				return AggregationConfiguration.Instance.MaxItemsForDBInUnknownHealthState;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		protected override SubscriptionSubmissionResult ResourceHealthUnknownResult
		{
			get
			{
				return SubscriptionSubmissionResult.DatabaseHealthUnknown;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		protected override SubscriptionSubmissionResult MaxConcurrentWorkAgainstResourceLimitReachedResult
		{
			get
			{
				return SubscriptionSubmissionResult.MaxConcurrentMailboxSubmissions;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		private float RawRpcLatency
		{
			get
			{
				return this.rpcLatencyMonitor.GetRawRpcLatency();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C4E5 File Offset: 0x0000A6E5
		private float RawRpcLatencyAverage
		{
			get
			{
				return this.rpcLatencyMonitor.GetRawRpcLatencyAverage();
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		protected override SyncResourceMonitor[] InitializeHealthMonitoring()
		{
			ResourceKey databaseResourceKey = this.CreateDatabaseRPCResourceKey();
			ResourceKey resourceKey = this.CreateDatabaseReplicationResourceKey();
			this.rpcLatencyMonitor = this.CreateSyncRPCMonitor(databaseResourceKey);
			return new SyncResourceMonitor[]
			{
				this.rpcLatencyMonitor,
				this.CreateSyncResourceMonitor(resourceKey, SyncResourceMonitorType.DatabaseReplicationLog)
			};
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C538 File Offset: 0x0000A738
		internal static SyncDB CreateSyncDB(Guid databaseGuid, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			return new SyncDB(databaseGuid, syncLogSession);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C564 File Offset: 0x0000A764
		internal override void UpdateDelay(int delay)
		{
			base.UpdateDelay(delay);
			this.averageDelay.Update((float)delay);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C57C File Offset: 0x0000A77C
		internal void NotifyStoreRoundtripComplete(string callMethodName, EventHandler<RoundtripCompleteEventArgs> roundtripComplete, RoundtripCompleteEventArgs eventArgs)
		{
			base.SyncLogSession.LogDebugging((TSLID)1339UL, ExTraceGlobals.SchedulerTracer, "StoreRoundtrip on database {0}. Backoff:{1} Latency:{2} RHMLatency:{3} RHMAverageLatency: {4} MethodName:{5}", new object[]
			{
				base.ResourceId,
				eventArgs.ThrottlingInfo.BackOffTime,
				eventArgs.RoundtripTime,
				this.RawRpcLatency,
				this.RawRpcLatencyAverage,
				callMethodName
			});
			this.averageRPCLatency.Update((float)eventArgs.RoundtripTime.TotalMilliseconds);
			roundtripComplete(null, eventArgs);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C61E File Offset: 0x0000A81E
		protected override SubscriptionSubmissionResult GetResultForResourceUnhealthy(SyncResourceMonitorType syncResourceMonitorType)
		{
			if (syncResourceMonitorType == SyncResourceMonitorType.DatabaseReplicationLog)
			{
				return SubscriptionSubmissionResult.MailboxServerHAUnhealthy;
			}
			return SubscriptionSubmissionResult.DatabaseRpcLatencyUnhealthy;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C628 File Offset: 0x0000A828
		protected override bool CanAcceptWorkBasedOnResourceSpecificChecks(out SubscriptionSubmissionResult result)
		{
			result = SubscriptionSubmissionResult.Success;
			if (AggregationConfiguration.Instance.MaxItemsForDBInManualConcurrencyMode > 0)
			{
				if (base.WorkItemsCount >= AggregationConfiguration.Instance.MaxItemsForDBInManualConcurrencyMode)
				{
					base.SyncLogSession.LogError((TSLID)1343UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: Cannot accept WI. Reached the cap on work items per DB for database {0}. WI Count: {1}.", new object[]
					{
						base.ResourceId,
						base.WorkItemsCount
					});
					result = SubscriptionSubmissionResult.DatabaseOverloaded;
					return false;
				}
				base.SyncLogSession.LogVerbose((TSLID)1344UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI can be accepted on database {0} since we have not reached the WI cap per DB. WI Count: {1}.", new object[]
				{
					base.ResourceId,
					base.WorkItemsCount
				});
				return true;
			}
			else
			{
				float effectiveRPCConcurrency = this.GetEffectiveRPCConcurrency();
				float value = this.averageDelay.Value;
				float rawRpcLatency = this.RawRpcLatency;
				float rawRpcLatencyAverage = this.RawRpcLatencyAverage;
				if (base.WorkItemsCount == 0)
				{
					base.SyncLogSession.LogVerbose((TSLID)1345UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI can be accepted on database {0} since it is the first item. WI Count: {1}.", new object[]
					{
						base.ResourceId,
						base.WorkItemsCount
					});
					return true;
				}
				if (rawRpcLatency > (float)SyncDB.MaxAcceptedRawRPCLatency || rawRpcLatencyAverage > (float)SyncDB.MaxAcceptedRawRPCLatency)
				{
					base.SyncLogSession.LogError((TSLID)1347UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: Cannot accept WI. Database {0} overloaded. RPC Latency exceeds maximum allowed. Raw Latency: {1} Average Latency: {2} WI Count: {3}.", new object[]
					{
						base.ResourceId,
						rawRpcLatency,
						rawRpcLatencyAverage,
						base.WorkItemsCount
					});
					result = SubscriptionSubmissionResult.DatabaseOverloaded;
					return false;
				}
				if (value <= (float)AggregationConfiguration.Instance.DelayTresholdForAcceptingNewWorkItems + SyncDB.MarginOfError)
				{
					base.SyncLogSession.LogVerbose((TSLID)1348UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI can be accepted on database {0} since RHM indicates that the DB health is good (average delay {1}). WI Count: {2}.", new object[]
					{
						base.ResourceId,
						value,
						base.WorkItemsCount
					});
					return true;
				}
				float storeLatencyAverage = SyncStoreLoadManager.StoreLatencyAverage;
				float cloudLatencyAverage = SyncStoreLoadManager.CloudLatencyAverage;
				float storeCloudRatioAverage = SyncStoreLoadManager.StoreCloudRatioAverage;
				float num = (value + storeLatencyAverage) / (value + storeLatencyAverage + cloudLatencyAverage / storeCloudRatioAverage);
				float num2 = (float)base.WorkItemsCount * num;
				base.SyncLogSession.LogVerbose((TSLID)1349UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: Database {0} Snapshot. Actual threads:{1} Effective threads:{2} WI Count:{3} Percent WI in store:{4} RPCLatency:{5} RHMLatency:{6} AverageDelay:{7}", new object[]
				{
					base.ResourceId,
					num2,
					effectiveRPCConcurrency,
					base.WorkItemsCount,
					num,
					this.averageRPCLatency,
					rawRpcLatency,
					value
				});
				if (!base.CanAddOneMoreConcurrentRequestToResource())
				{
					result = this.MaxConcurrentWorkAgainstResourceLimitReachedResult;
					return false;
				}
				return true;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		protected virtual float GetEffectiveRPCConcurrency()
		{
			float storeLatencyAverage = SyncStoreLoadManager.StoreLatencyAverage;
			float cloudLatencyAverage = SyncStoreLoadManager.CloudLatencyAverage;
			float storeCloudRatioAverage = SyncStoreLoadManager.StoreCloudRatioAverage;
			float value = this.averageDelay.Value;
			float num = (value + storeLatencyAverage) / (value + storeLatencyAverage + cloudLatencyAverage / storeCloudRatioAverage);
			return (float)base.WorkItemsCount * num;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C932 File Offset: 0x0000AB32
		protected virtual SyncRPCResourceMonitor CreateSyncRPCMonitor(ResourceKey databaseResourceKey)
		{
			return new SyncRPCResourceMonitor(base.SyncLogSession, databaseResourceKey, SyncResourceMonitorType.DatabaseRPCLatency);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C941 File Offset: 0x0000AB41
		protected virtual SyncResourceMonitor CreateSyncResourceMonitor(ResourceKey resourceKey, SyncResourceMonitorType syncResourceMonitorType)
		{
			return new SyncResourceMonitor(base.SyncLogSession, resourceKey, syncResourceMonitorType);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C950 File Offset: 0x0000AB50
		private ResourceKey CreateDatabaseRPCResourceKey()
		{
			return new MdbResourceHealthMonitorKey(this.databaseGuid);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C95D File Offset: 0x0000AB5D
		private ResourceKey CreateDatabaseReplicationResourceKey()
		{
			return new MdbReplicationResourceHealthMonitorKey(this.databaseGuid);
		}

		// Token: 0x04000170 RID: 368
		private static readonly int MaxAcceptedRawRPCLatency = 50;

		// Token: 0x04000171 RID: 369
		private static readonly ushort NumberOfDelaySamples = 100;

		// Token: 0x04000172 RID: 370
		private static readonly ushort NumberOfRPCLatencySamples = 5;

		// Token: 0x04000173 RID: 371
		private static readonly float MarginOfError = 1E-09f;

		// Token: 0x04000174 RID: 372
		private readonly Guid databaseGuid;

		// Token: 0x04000175 RID: 373
		private RunningAverageFloat averageDelay;

		// Token: 0x04000176 RID: 374
		private RunningAverageFloat averageRPCLatency;

		// Token: 0x04000177 RID: 375
		private SyncRPCResourceMonitor rpcLatencyMonitor;
	}
}
