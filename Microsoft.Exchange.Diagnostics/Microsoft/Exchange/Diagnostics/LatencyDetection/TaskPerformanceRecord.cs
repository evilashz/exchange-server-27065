using System;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000180 RID: 384
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskPerformanceRecord : IDisposable
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00028526 File Offset: 0x00026726
		private static ulong NextCorrelationId
		{
			get
			{
				return (ulong)Interlocked.Increment(ref TaskPerformanceRecord.correlationIdSequence);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00028534 File Offset: 0x00026734
		public TaskPerformanceRecord(string taskName, LatencyDetectionContextFactory latencyDetectionContextFactory, ExEventLog.EventTuple startTuple, ExEventLog.EventTuple endTuple, ExEventLog eventLog)
		{
			this.TaskName = taskName;
			this.latencyDetectionContextFactory = latencyDetectionContextFactory;
			this.startTuple = startTuple;
			this.endTuple = endTuple;
			this.eventLog = eventLog;
			this.correlationId = TaskPerformanceRecord.NextCorrelationId.ToString();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002857F File Offset: 0x0002677F
		public TaskPerformanceRecord(string taskName, LatencyDetectionContextFactory latencyDetectionContextFactory, ExEventLog.EventTuple startTuple, ExEventLog.EventTuple endTuple, ExEventLog eventLog, params IPerformanceDataProvider[] performanceDataProviders) : this(taskName, latencyDetectionContextFactory, startTuple, endTuple, eventLog)
		{
			this.Start(performanceDataProviders);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00028596 File Offset: 0x00026796
		void IDisposable.Dispose()
		{
			this.Stop();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000285A0 File Offset: 0x000267A0
		public void Start(params IPerformanceDataProvider[] performanceDataProviders)
		{
			this.eventLog.LogEvent(this.startTuple, null, new object[]
			{
				this.TaskName,
				this.correlationId
			});
			this.latencyDetectionContext = this.latencyDetectionContextFactory.CreateContext("15.00.1497.010", this.TaskName, performanceDataProviders);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000285F8 File Offset: 0x000267F8
		public TimeSpan Stop()
		{
			if (this.IsCollecting)
			{
				this.performanceData = this.latencyDetectionContext.StopAndFinalizeCollection();
				object obj = string.Empty;
				if (this.eventLog.IsEventCategoryEnabled(this.endTuple.CategoryId, ExEventLog.EventLevel.High))
				{
					string text = this.latencyDetectionContext.ToString("s");
					if (text.Length > 32766)
					{
						text = text.Substring(0, 32766);
					}
					obj = text;
				}
				this.eventLog.LogEvent(this.endTuple, null, new object[]
				{
					this.TaskName,
					this.latencyDetectionContext.Elapsed,
					obj,
					this.correlationId
				});
			}
			return this.latencyDetectionContext.Elapsed;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000286BC File Offset: 0x000268BC
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x000286C4 File Offset: 0x000268C4
		public string TaskName { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000286CD File Offset: 0x000268CD
		public bool IsCollecting
		{
			get
			{
				return this.latencyDetectionContext != null && this.performanceData == null;
			}
		}

		// Token: 0x1700023F RID: 575
		public PerformanceData this[int providerIndex]
		{
			get
			{
				if (this.performanceData != null)
				{
					return this.performanceData[providerIndex].Difference;
				}
				return PerformanceData.Zero;
			}
		}

		// Token: 0x04000787 RID: 1927
		private static long correlationIdSequence;

		// Token: 0x04000788 RID: 1928
		private LatencyDetectionContextFactory latencyDetectionContextFactory;

		// Token: 0x04000789 RID: 1929
		private LatencyDetectionContext latencyDetectionContext;

		// Token: 0x0400078A RID: 1930
		private TaskPerformanceData[] performanceData;

		// Token: 0x0400078B RID: 1931
		private ExEventLog.EventTuple startTuple;

		// Token: 0x0400078C RID: 1932
		private ExEventLog.EventTuple endTuple;

		// Token: 0x0400078D RID: 1933
		private ExEventLog eventLog;

		// Token: 0x0400078E RID: 1934
		private string correlationId;
	}
}
