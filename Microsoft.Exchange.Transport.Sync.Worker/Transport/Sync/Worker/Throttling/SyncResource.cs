using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SyncResource
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000BF63 File Offset: 0x0000A163
		public SyncResource(SyncLogSession syncLogSession, string resourceId)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("resourceId", resourceId);
			this.SyncLogSession = syncLogSession;
			this.ResourceId = resourceId;
			this.CreateSlidingWindows();
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		internal string ResourceId { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000BFB1 File Offset: 0x0000A1B1
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000BFB9 File Offset: 0x0000A1B9
		private protected SyncLogSession SyncLogSession { protected get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		protected int WorkItemsCount
		{
			get
			{
				return this.workItemsCount;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000279 RID: 633
		protected abstract int MaxConcurrentWorkInUnknownState { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600027A RID: 634
		protected abstract SubscriptionSubmissionResult ResourceHealthUnknownResult { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600027B RID: 635
		protected abstract SubscriptionSubmissionResult MaxConcurrentWorkAgainstResourceLimitReachedResult { get; }

		// Token: 0x0600027C RID: 636 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		internal bool TryAcceptWorkItem(AggregationWorkItem workItem, out SubscriptionSubmissionResult result)
		{
			lock (this.syncObject)
			{
				if (this.CanAcceptWorkItem(workItem, out result))
				{
					this.AddWorkItem(workItem);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C020 File Offset: 0x0000A220
		internal SyncResourceMonitor[] GetResourceMonitors()
		{
			return this.resourceHealthMonitors;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C028 File Offset: 0x0000A228
		internal virtual void RemoveWorkItem(AggregationWorkItem workItem)
		{
			lock (this.syncObject)
			{
				this.workItemsCount--;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C070 File Offset: 0x0000A270
		internal virtual void UpdateDelay(int delay)
		{
			this.slidingDelayCount.Add((uint)delay);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C080 File Offset: 0x0000A280
		internal int GetSuggestedDelay(int originalDelay)
		{
			int num = Math.Max(1, this.workItemsCount);
			return Math.Max(1, originalDelay / num);
		}

		// Token: 0x06000281 RID: 641
		protected abstract SyncResourceMonitor[] InitializeHealthMonitoring();

		// Token: 0x06000282 RID: 642
		protected abstract SubscriptionSubmissionResult GetResultForResourceUnhealthy(SyncResourceMonitorType syncResourceMonitorType);

		// Token: 0x06000283 RID: 643
		protected abstract bool CanAcceptWorkBasedOnResourceSpecificChecks(out SubscriptionSubmissionResult result);

		// Token: 0x06000284 RID: 644 RVA: 0x0000C0A3 File Offset: 0x0000A2A3
		protected virtual void AddWorkItem(AggregationWorkItem workItem)
		{
			this.workItemsCount++;
			this.UpdateConcurrency(1);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C0BA File Offset: 0x0000A2BA
		protected void Initialize()
		{
			this.resourceHealthMonitors = this.InitializeHealthMonitoring();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		protected bool CanAddOneMoreConcurrentRequestToResource()
		{
			int suggestedConcurrency = this.GetSuggestedConcurrency();
			int num = this.WorkItemsCount;
			uint delaySum = this.GetDelaySum();
			if (delaySum > 0U && num >= suggestedConcurrency && num > this.MaxConcurrentWorkInUnknownState)
			{
				this.SyncLogSession.LogError((TSLID)1529UL, ExTraceGlobals.SchedulerTracer, "CanAddOneMoreConcurrentRequestToResource: Cannot accept WI. {0} has concurrency {1} and suggested concurrency {2}.", new object[]
				{
					this.ResourceId,
					num,
					suggestedConcurrency
				});
				return false;
			}
			this.SyncLogSession.LogVerbose((TSLID)330UL, ExTraceGlobals.SchedulerTracer, "CanAddOneMoreConcurrentRequestToResource: For resource {0}, actual concurrency {1}, effective concurrency: {2}", new object[]
			{
				this.ResourceId,
				num,
				suggestedConcurrency
			});
			return true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C18C File Offset: 0x0000A38C
		private bool CanAcceptWorkItem(AggregationWorkItem workItem, out SubscriptionSubmissionResult result)
		{
			bool flag;
			bool flag2;
			SyncResourceMonitorType syncResourceMonitorType = this.IsAnyResourceUnhealthyOrUnknown(workItem, out flag, out flag2);
			if (flag)
			{
				result = this.GetResultForResourceUnhealthy(syncResourceMonitorType);
				this.SyncLogSession.LogError((TSLID)1441UL, ExTraceGlobals.SchedulerTracer, "CanAcceptWorkItem: Cannot accept WI. {0} Unhealthy. WI Count: {1}. Result {2}", new object[]
				{
					this.ResourceId,
					this.WorkItemsCount,
					result
				});
				return false;
			}
			if (flag2)
			{
				if (this.WorkItemsCount >= this.MaxConcurrentWorkInUnknownState)
				{
					result = this.ResourceHealthUnknownResult;
					this.SyncLogSession.LogError((TSLID)1528UL, ExTraceGlobals.SchedulerTracer, "CanAcceptWorkItem: Cannot accept WI. {0} has unknown health. WI Count: {1}. Result {2}", new object[]
					{
						this.ResourceId,
						this.WorkItemsCount,
						result
					});
					return false;
				}
				this.SyncLogSession.LogVerbose((TSLID)1466UL, ExTraceGlobals.SchedulerTracer, "CanAcceptWorkItem: WI can be accepted on {0} since we have not reached MaxConcurrentWorkInUnknownState {1}. WI Count: {2}.", new object[]
				{
					this.ResourceId,
					this.MaxConcurrentWorkInUnknownState,
					this.WorkItemsCount
				});
				result = SubscriptionSubmissionResult.Success;
				return true;
			}
			else
			{
				if (!this.CanAcceptWorkBasedOnResourceSpecificChecks(out result))
				{
					return false;
				}
				this.SyncLogSession.LogVerbose((TSLID)962UL, ExTraceGlobals.SchedulerTracer, "CanAcceptWorkItem: WI can be accepted on {0}. All checks passed.", new object[]
				{
					this.ResourceId
				});
				result = SubscriptionSubmissionResult.Success;
				return true;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C304 File Offset: 0x0000A504
		private SyncResourceMonitorType IsAnyResourceUnhealthyOrUnknown(AggregationWorkItem workItem, out bool isAnyResourceUnhealthy, out bool isAnyResourceUnknown)
		{
			SyncResourceMonitor[] resourceMonitors = this.GetResourceMonitors();
			return SyncResourceMonitor.IsAnyResourceUnhealthyOrUnknown(workItem, resourceMonitors, out isAnyResourceUnhealthy, out isAnyResourceUnknown);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C321 File Offset: 0x0000A521
		private void CreateSlidingWindows()
		{
			this.slidingConcurrencyAverage = new FixedTimeAverage(1000, 60, Environment.TickCount);
			this.slidingDelayCount = new FixedTimeSum(1000, 60);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C34C File Offset: 0x0000A54C
		private int GetSuggestedConcurrency()
		{
			if (AggregationConfiguration.Instance.SuggestedConcurrencyOverride != null)
			{
				return AggregationConfiguration.Instance.SuggestedConcurrencyOverride.Value;
			}
			double num = (double)this.slidingConcurrencyAverage.GetValue();
			uint value = this.slidingDelayCount.GetValue();
			if (num == 0.0)
			{
				num = (double)this.WorkItemsCount;
			}
			double num2 = 60000.0;
			double num3 = num2 * num - value;
			int num4 = (int)(num3 / num2 + 1.5);
			this.SyncLogSession.LogVerbose((TSLID)1530UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: For {0}. Current concurrency {1} and suggested concurrency {2} (greed). Calculated average {3}. DelaySum: {4}. Suggested concurrency before truncation: {5}.", new object[]
			{
				base.GetType().Name,
				this.WorkItemsCount,
				num4,
				num,
				value,
				num3 / num2 + 1.5
			});
			return num4;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C450 File Offset: 0x0000A650
		private uint GetDelaySum()
		{
			return this.slidingDelayCount.GetValue();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C45D File Offset: 0x0000A65D
		private void UpdateConcurrency(int increment)
		{
			this.slidingConcurrencyAverage.Add(Environment.TickCount, (uint)(this.WorkItemsCount + increment));
		}

		// Token: 0x04000167 RID: 359
		private const ushort SlidingWindowSize = 60;

		// Token: 0x04000168 RID: 360
		private const ushort SlidingWindowBucketSize = 1000;

		// Token: 0x04000169 RID: 361
		private readonly object syncObject = new object();

		// Token: 0x0400016A RID: 362
		private SyncResourceMonitor[] resourceHealthMonitors;

		// Token: 0x0400016B RID: 363
		private FixedTimeAverage slidingConcurrencyAverage;

		// Token: 0x0400016C RID: 364
		private FixedTimeSum slidingDelayCount;

		// Token: 0x0400016D RID: 365
		private int workItemsCount;
	}
}
