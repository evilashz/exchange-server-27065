using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplayQueuedItemBase : IQueuedCallback
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00036C20 File Offset: 0x00034E20
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00036C28 File Offset: 0x00034E28
		private QueuedItemStatus Status { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00036C31 File Offset: 0x00034E31
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00036C39 File Offset: 0x00034E39
		private bool IsCancelRequested { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00036C42 File Offset: 0x00034E42
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00036C4A File Offset: 0x00034E4A
		private bool IsStarted { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00036C53 File Offset: 0x00034E53
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00036C5B File Offset: 0x00034E5B
		public bool IsDuplicateAllowed { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00036C64 File Offset: 0x00034E64
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00036C6C File Offset: 0x00034E6C
		public Exception LastException { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00036C75 File Offset: 0x00034E75
		public bool IsComplete
		{
			get
			{
				return this.Status == QueuedItemStatus.Cancelled || this.Status == QueuedItemStatus.Completed || this.Status == QueuedItemStatus.Failed;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00036C94 File Offset: 0x00034E94
		public bool IsCancelled
		{
			get
			{
				return (!this.IsStarted && this.IsCancelRequested) || (this.IsStarted && this.Status == QueuedItemStatus.Cancelled);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00036CBB File Offset: 0x00034EBB
		public virtual string Name
		{
			get
			{
				return this.m_operationName;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00036CC3 File Offset: 0x00034EC3
		public ReplayQueuedItemBase()
		{
			this.CreateTimeUtc = DateTime.UtcNow;
			this.IsDuplicateAllowed = true;
			this.Status = QueuedItemStatus.Unknown;
			this.m_operationName = base.GetType().Name;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00036CF5 File Offset: 0x00034EF5
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00036CFD File Offset: 0x00034EFD
		public DateTime CreateTimeUtc { get; private set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00036D06 File Offset: 0x00034F06
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00036D0E File Offset: 0x00034F0E
		public DateTime StartTimeUtc { private get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00036D17 File Offset: 0x00034F17
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00036D1F File Offset: 0x00034F1F
		public DateTime EndTimeUtc { private get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00036D28 File Offset: 0x00034F28
		public TimeSpan StartTimeDuration
		{
			get
			{
				if (this.IsStarted)
				{
					return this.StartTimeUtc.Subtract(this.CreateTimeUtc);
				}
				return TimeSpan.Zero;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00036D58 File Offset: 0x00034F58
		public TimeSpan ExecutionDuration
		{
			get
			{
				if (this.IsComplete && !this.IsCancelled)
				{
					return this.EndTimeUtc.Subtract(this.StartTimeUtc);
				}
				return TimeSpan.Zero;
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00036D8F File Offset: 0x00034F8F
		public virtual bool IsEquivalentOrSuperset(IQueuedCallback otherCallback)
		{
			return object.ReferenceEquals(this, otherCallback);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00036DA0 File Offset: 0x00034FA0
		public void ReportStatus(QueuedItemStatus status)
		{
			lock (this)
			{
				if (this.IsCancelRequested && status == QueuedItemStatus.Started)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceError<string, QueuedItemStatus>((long)this.GetHashCode(), "ReplayQueuedItemBase: ReportStatus for queued item '{0}' and status '{1}' is throwing OperationAbortedException because Cancel() has already been called.", base.GetType().Name, status);
					throw new OperationAbortedException();
				}
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<string, QueuedItemStatus, QueuedItemStatus>((long)this.GetHashCode(), "ReplayQueuedItemBase: ReportStatus for queued item '{0}'. Status changed from {1} to {2}.", base.GetType().Name, this.Status, status);
				this.Status = status;
				if (status == QueuedItemStatus.Started)
				{
					this.IsStarted = true;
				}
			}
			if (status == QueuedItemStatus.Cancelled)
			{
				this.Cancel();
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00036E50 File Offset: 0x00035050
		public void Cancel()
		{
			lock (this)
			{
				if (!this.IsCancelRequested)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayQueuedItemBase: Queued item for '{0}' has been cancelled.", base.GetType().Name);
					this.IsCancelRequested = true;
					this.LastException = this.GetOperationCancelledException();
				}
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00036ECC File Offset: 0x000350CC
		public void Execute()
		{
			Exception lastException = ReplayConfigurationHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.ExecuteInternal();
			});
			this.LastException = lastException;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00036EF2 File Offset: 0x000350F2
		public ReplayQueuedItemCompletionReason Wait()
		{
			return this.Wait(-1);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00036EFC File Offset: 0x000350FC
		public ReplayQueuedItemCompletionReason Wait(int timeoutMs)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ReplayQueuedItemCompletionReason replayQueuedItemCompletionReason;
			while (!this.IsCancelled)
			{
				if (timeoutMs != -1 && stopwatch.ElapsedMilliseconds > (long)timeoutMs)
				{
					replayQueuedItemCompletionReason = ReplayQueuedItemCompletionReason.Timedout;
				}
				else
				{
					if (!this.IsComplete)
					{
						Thread.Sleep(50);
						continue;
					}
					replayQueuedItemCompletionReason = ReplayQueuedItemCompletionReason.Finished;
				}
				IL_41:
				Exception ex = this.LastException;
				if (ex == null && replayQueuedItemCompletionReason == ReplayQueuedItemCompletionReason.Timedout)
				{
					ex = this.GetOperationTimedoutException(TimeSpan.FromMilliseconds((double)timeoutMs));
				}
				if (ex != null)
				{
					if (ex is OperationAbortedException)
					{
						ex = this.GetOperationCancelledException();
					}
					throw ex;
				}
				return replayQueuedItemCompletionReason;
			}
			replayQueuedItemCompletionReason = ReplayQueuedItemCompletionReason.Cancelled;
			goto IL_41;
		}

		// Token: 0x06000C6E RID: 3182
		protected abstract void ExecuteInternal();

		// Token: 0x06000C6F RID: 3183
		protected abstract Exception GetOperationCancelledException();

		// Token: 0x06000C70 RID: 3184
		protected abstract Exception GetOperationTimedoutException(TimeSpan timeout);

		// Token: 0x04000547 RID: 1351
		private string m_operationName;
	}
}
