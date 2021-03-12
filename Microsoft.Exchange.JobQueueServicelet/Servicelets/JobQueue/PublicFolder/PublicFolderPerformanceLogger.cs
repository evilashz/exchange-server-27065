using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PublicFolderPerformanceLogger
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000053F2 File Offset: 0x000035F2
		public PublicFolderPerformanceLogger(PublicFolderSynchronizerContext syncContext)
		{
			ArgumentValidator.ThrowIfNull("syncContext", syncContext);
			this.syncContext = syncContext;
			this.transientRetryDelayTracker = new PerformanceDataProvider("TransientRetryDelay");
			this.performanceTrackers = new Dictionary<SyncActivity, PublicFolderActivityPerformanceTracker>(30);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005429 File Offset: 0x00003629
		public PerformanceDataProvider TransientRetryDelayTracker
		{
			get
			{
				return this.transientRetryDelayTracker;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005431 File Offset: 0x00003631
		private FolderOperationCounter FolderOperationCounter
		{
			get
			{
				return this.syncContext.FolderOperationCount;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000543E File Offset: 0x0000363E
		private SyncStateCounter SyncStateCounter
		{
			get
			{
				return this.syncContext.SyncStateCounter;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000544B File Offset: 0x0000364B
		private LatencyInfo MRSProxyLatencyInfo
		{
			get
			{
				return this.syncContext.MRSProxyLatencyInfo;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005458 File Offset: 0x00003658
		private Guid? CorrelationId
		{
			get
			{
				return new Guid?(this.syncContext.CorrelationId);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000546C File Offset: 0x0000366C
		public IDisposable GetTaskFrame(SyncActivity activity)
		{
			PublicFolderActivityPerformanceTracker publicFolderActivityPerformanceTracker;
			if (!this.performanceTrackers.TryGetValue(activity, out publicFolderActivityPerformanceTracker))
			{
				publicFolderActivityPerformanceTracker = new PublicFolderActivityPerformanceTracker(activity, this.FolderOperationCounter, this.SyncStateCounter, this.MRSProxyLatencyInfo, this.transientRetryDelayTracker);
				this.performanceTrackers[activity] = publicFolderActivityPerformanceTracker;
			}
			return new PublicFolderPerformanceLogger.TaskFrame(activity, publicFolderActivityPerformanceTracker);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000054BC File Offset: 0x000036BC
		public void InitializeCounters(int batchNumber)
		{
			this.batchNumber = batchNumber;
			this.performanceTrackers.Clear();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000054D0 File Offset: 0x000036D0
		public void WriteActivitiesCountersToLog()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			foreach (PublicFolderActivityPerformanceTracker publicFolderActivityPerformanceTracker in this.performanceTrackers.Values)
			{
				stringBuilder.AppendFormat("Batch={0};", this.batchNumber);
				publicFolderActivityPerformanceTracker.AppendLogData(stringBuilder);
				PublicFolderSynchronizerLogger.LogOnServer(stringBuilder.ToString(), LogEventType.PerfCounters, this.CorrelationId);
				stringBuilder.Clear();
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly Dictionary<SyncActivity, PublicFolderActivityPerformanceTracker> performanceTrackers;

		// Token: 0x0400005F RID: 95
		private readonly PublicFolderSynchronizerContext syncContext;

		// Token: 0x04000060 RID: 96
		private readonly PerformanceDataProvider transientRetryDelayTracker;

		// Token: 0x04000061 RID: 97
		private int batchNumber;

		// Token: 0x02000012 RID: 18
		private class TaskFrame : IDisposable
		{
			// Token: 0x060000A0 RID: 160 RVA: 0x00005564 File Offset: 0x00003764
			public TaskFrame(SyncActivity activity, PublicFolderActivityPerformanceTracker performanceTracker)
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					this.previousActionDescription = currentActivityScope.Action;
					currentActivityScope.Action = activity.ToString();
				}
				this.performanceTracker = performanceTracker;
				this.performanceTracker.Start();
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x000055B0 File Offset: 0x000037B0
			public void Dispose()
			{
				this.performanceTracker.Stop();
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					currentActivityScope.Action = this.previousActionDescription;
				}
			}

			// Token: 0x04000062 RID: 98
			private readonly PublicFolderActivityPerformanceTracker performanceTracker;

			// Token: 0x04000063 RID: 99
			private readonly string previousActionDescription;
		}
	}
}
