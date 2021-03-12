using System;
using System.Diagnostics;
using Microsoft.Exchange.DxStore.HA.Events;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A8 RID: 168
	public class PerformanceTracker
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x00017373 File Offset: 0x00015573
		public PerformanceTracker()
		{
			this.apiExecutionPeriodicLogDuration = TimeSpan.FromMilliseconds((double)RegistryParameters.DistributedStoreApiExecutionPeriodicLogDurationInMs);
			this.CurrentProcessName = Process.GetCurrentProcess().ProcessName;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000173B2 File Offset: 0x000155B2
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000173BA File Offset: 0x000155BA
		public string CurrentProcessName { get; private set; }

		// Token: 0x0600064B RID: 1611 RVA: 0x000173C4 File Offset: 0x000155C4
		public void UpdateStart(DistributedStoreKey key, StoreKind storeKind, bool isPrimary)
		{
			lock (this.locker)
			{
				PerformanceEntry orAdd = this.GetOrAdd(storeKind, isPrimary);
				orAdd.RecordStart();
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00017410 File Offset: 0x00015610
		public void LogExecution(DistributedStoreKey key, StoreKind storeKind, bool isPrimary, RequestInfo req, long latencyInMs, Exception exception, bool isSkipped)
		{
			if (!isPrimary && !RegistryParameters.DistributedStoreIsLogShadowApiResult)
			{
				return;
			}
			bool flag = exception == null && !isSkipped;
			string text = req.OperationCategory.ToString();
			string text2 = (key != null) ? key.FullKeyName : string.Empty;
			string debugStr = req.DebugStr;
			string text3 = (key != null) ? key.InstanceId.ToString() : string.Empty;
			string text4 = string.Empty;
			if (RegistryParameters.DistributedStoreIsLogApiExecutionCallstack)
			{
				text4 = new StackTrace(3, true).ToString();
			}
			if (flag)
			{
				if (RegistryParameters.DistributedStoreIsLogApiSuccess)
				{
					if (this.apiExecutionPeriodicLogDuration != TimeSpan.Zero)
					{
						DxStoreHACrimsonEvents.ApiExecutionSuccess.LogPeriodic<string, bool, StoreKind, bool, string, OperationType, string, long, string, bool, string, string, string, string>(text, this.apiExecutionPeriodicLogDuration, text, isPrimary, storeKind, true, text2, req.OperationType, req.InitiatedTime.ToString("o"), latencyInMs, string.Empty, false, debugStr, text3, text4, this.CurrentProcessName);
						return;
					}
					DxStoreHACrimsonEvents.ApiExecutionSuccess.Log<string, bool, StoreKind, bool, string, OperationType, string, long, string, bool, string, string, string, string>(text, isPrimary, storeKind, true, text2, req.OperationType, req.InitiatedTime.ToString("o"), latencyInMs, string.Empty, false, debugStr, text3, text4, this.CurrentProcessName);
				}
				return;
			}
			string text5 = isSkipped ? "<ApiSkipped>" : exception.ToString();
			string text6 = text + ((exception == null) ? string.Empty : exception.GetType().Name);
			if (this.apiExecutionPeriodicLogDuration != TimeSpan.Zero)
			{
				DxStoreHACrimsonEvents.ApiExecutionFailed.LogPeriodic<string, bool, StoreKind, bool, string, OperationType, string, long, string, bool, string, string, string, string>(text6, this.apiExecutionPeriodicLogDuration, text, isPrimary, storeKind, false, text2, req.OperationType, req.InitiatedTime.ToString("o"), latencyInMs, text5.Substring(0, Math.Min(text5.Length, 5000)), isSkipped, debugStr, text3, text4, this.CurrentProcessName);
				return;
			}
			DxStoreHACrimsonEvents.ApiExecutionFailed.Log<string, bool, StoreKind, bool, string, OperationType, string, long, string, bool, string, string, string, string>(text, isPrimary, storeKind, false, text2, req.OperationType, req.InitiatedTime.ToString("o"), latencyInMs, text5.Substring(0, Math.Min(text5.Length, 5000)), isSkipped, debugStr, text3, text4, this.CurrentProcessName);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001763C File Offset: 0x0001583C
		public void UpdateFinish(DistributedStoreKey key, StoreKind storeKind, bool isPrimary, RequestInfo req, long latencyInMs, Exception exception, bool isSkipped)
		{
			this.LogExecution(key, storeKind, isPrimary, req, latencyInMs, exception, isSkipped);
			lock (this.locker)
			{
				PerformanceEntry orAdd = this.GetOrAdd(storeKind, isPrimary);
				orAdd.RecordFinish(req, latencyInMs, exception, isSkipped);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000176A8 File Offset: 0x000158A8
		public void Start()
		{
			lock (this.timerLock)
			{
				if (this.timer == null)
				{
					this.timer = new GuardedTimer(delegate(object o)
					{
						this.PublishConsolidatedPerformanceEvents();
					}, null, 0L, (long)RegistryParameters.DistributedStorePerfTrackerFlushInMs);
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00017714 File Offset: 0x00015914
		public void Stop()
		{
			this.PublishConsolidatedPerformanceEvents();
			lock (this.timerLock)
			{
				if (this.timer != null)
				{
					this.timer.Dispose(true);
					this.timer = null;
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00017770 File Offset: 0x00015970
		public void PublishConsolidatedPerformanceEvents()
		{
			try
			{
				this.PublishConsolidatedPerformanceEventsInternal();
			}
			catch (Exception ex)
			{
				DxStoreHACrimsonEvents.FailedToPublishPerfStats.Log<string>(ex.ToString());
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000177A8 File Offset: 0x000159A8
		private PerformanceEntry GetOrAdd(StoreKind storeKind, bool isPrimary)
		{
			PerformanceEntry performanceEntry = (storeKind == StoreKind.Clusdb) ? this.clusdbPerfEntry : this.dxstorePerfEntry;
			if (performanceEntry == null)
			{
				performanceEntry = new PerformanceEntry(storeKind, isPrimary);
				if (storeKind == StoreKind.Clusdb)
				{
					this.clusdbPerfEntry = performanceEntry;
				}
				else
				{
					this.dxstorePerfEntry = performanceEntry;
				}
			}
			return performanceEntry;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000177E8 File Offset: 0x000159E8
		private void PublishConsolidatedPerformanceEventsInternal()
		{
			PerformanceEntry performanceEntry;
			PerformanceEntry performanceEntry2;
			lock (this.locker)
			{
				performanceEntry = this.clusdbPerfEntry;
				performanceEntry2 = this.dxstorePerfEntry;
				this.clusdbPerfEntry = null;
				this.dxstorePerfEntry = null;
			}
			if (performanceEntry != null)
			{
				performanceEntry.PublishEvent(this.CurrentProcessName);
			}
			if (performanceEntry2 != null)
			{
				performanceEntry2.PublishEvent(this.CurrentProcessName);
			}
		}

		// Token: 0x0400034D RID: 845
		private readonly object timerLock = new object();

		// Token: 0x0400034E RID: 846
		private readonly object locker = new object();

		// Token: 0x0400034F RID: 847
		private readonly TimeSpan apiExecutionPeriodicLogDuration;

		// Token: 0x04000350 RID: 848
		private GuardedTimer timer;

		// Token: 0x04000351 RID: 849
		private PerformanceEntry clusdbPerfEntry;

		// Token: 0x04000352 RID: 850
		private PerformanceEntry dxstorePerfEntry;
	}
}
