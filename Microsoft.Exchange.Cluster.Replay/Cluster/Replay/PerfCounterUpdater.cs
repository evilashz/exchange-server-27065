using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002EE RID: 750
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PerfCounterUpdater : TimerComponent
	{
		// Token: 0x06001E1E RID: 7710 RVA: 0x00089A8C File Offset: 0x00087C8C
		public PerfCounterUpdater(IPerfmonCounters counters, ReplayConfiguration config) : base(TimeSpan.FromMilliseconds((double)RegistryParameters.PerfCounterUpdateIntervalInMSec), TimeSpan.FromMilliseconds((double)RegistryParameters.PerfCounterUpdateIntervalInMSec), "PerfCounterUpdater")
		{
			this.m_counters = counters;
			this.m_config = config;
			this.m_amCounters = ActiveManagerPerfmon.GetInstance(config.Name);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00089B09 File Offset: 0x00087D09
		protected override void TimerCallbackInternal()
		{
			this.UpdateQueueAlertPerfCounters();
			this.ClearDatabaseMountedCounter();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00089B18 File Offset: 0x00087D18
		private void UpdateQueueAlertPerfCounters()
		{
			if (this.m_counters.Suspended != 0L || this.m_counters.Initializing != 0L || this.m_counters.Failed != 0L || this.m_counters.FailedSuspended != 0L || this.m_counters.Resynchronizing != 0L)
			{
				this.m_counters.CopyQueueNotKeepingUp = 0L;
				this.m_counters.ReplayQueueNotKeepingUp = 0L;
				this.m_firstTime = true;
				return;
			}
			if (this.m_counters.Disconnected != 0L)
			{
				Exception ex = null;
				try
				{
					ReplayState replayState = this.m_config.ReplayState;
					long lastLogCommittedGenerationNumberFromCluster = replayState.GetLastLogCommittedGenerationNumberFromCluster();
					if (this.m_counters.CopyNotificationGenerationNumber < lastLogCommittedGenerationNumberFromCluster)
					{
						this.m_counters.CopyNotificationGenerationNumber = lastLogCommittedGenerationNumberFromCluster;
					}
				}
				catch (ClusterException ex2)
				{
					ex = ex2;
				}
				catch (TransientException ex3)
				{
					ex = ex3;
				}
				catch (AmServerException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "PerfCounterUpdater.UpdateQueueAlertPerfCounters({0}): Exception trying to update disconnected generation: {1}", this.m_config.DisplayName, ex);
				}
			}
			long copyQueueLength = this.m_counters.CopyQueueLength;
			long replayQueueLength = this.m_counters.ReplayQueueLength;
			long inspectorGenerationNumber = this.m_counters.InspectorGenerationNumber;
			long replayGenerationNumber = this.m_counters.ReplayGenerationNumber;
			DateTime utcNow = DateTime.UtcNow;
			if (this.m_firstTime)
			{
				this.m_baselineCopyQueueLength = copyQueueLength;
				this.m_baselineReplayQueueLength = replayQueueLength;
				this.m_firstTime = false;
			}
			else
			{
				if (copyQueueLength > this.m_baselineCopyQueueLength + this.CopyQueueAlertThreshold)
				{
					this.m_counters.CopyQueueNotKeepingUp = 1L;
				}
				else if (this.m_lastCopyQueueLength > 0L && inspectorGenerationNumber == this.m_lastCopiedGeneration)
				{
					if (++this.m_copyNotMakingProgressIntervals >= 4)
					{
						this.m_counters.CopyQueueNotKeepingUp = 1L;
					}
				}
				else
				{
					this.m_copyNotMakingProgressIntervals = 0;
					if ((double)copyQueueLength < (double)this.m_baselineCopyQueueLength + (double)this.CopyQueueAlertThreshold * 0.8)
					{
						this.m_counters.CopyQueueNotKeepingUp = 0L;
						if (copyQueueLength < this.m_baselineCopyQueueLength)
						{
							this.m_baselineReplayQueueLength += this.m_baselineCopyQueueLength - copyQueueLength;
							this.m_baselineCopyQueueLength = copyQueueLength;
						}
					}
				}
				if (utcNow - this.m_config.ReplayState.CurrentReplayTime > this.m_config.ReplayLagTime + this.ExtraReplayLagAllowed)
				{
					if (replayQueueLength > this.m_baselineReplayQueueLength + this.ReplayQueueAlertThreshold)
					{
						if (this.m_counters.PassiveSeedingSource == 0L && !this.m_config.ReplayState.ReplaySuspended)
						{
							this.m_counters.ReplayQueueNotKeepingUp = 1L;
						}
					}
					else if (this.m_lastReplayQueueLength > 0L && replayGenerationNumber == this.m_lastReplayedGeneration)
					{
						if (++this.m_replayNotMakingProgressIntervals >= 4 && this.m_counters.PassiveSeedingSource == 0L && !this.m_config.ReplayState.ReplaySuspended)
						{
							this.m_counters.ReplayQueueNotKeepingUp = 1L;
						}
					}
					else
					{
						this.m_replayNotMakingProgressIntervals = 0;
						if ((double)replayQueueLength < (double)this.m_baselineReplayQueueLength + (double)this.ReplayQueueAlertThreshold * 0.8)
						{
							this.m_counters.ReplayQueueNotKeepingUp = 0L;
							if (replayQueueLength < this.m_baselineReplayQueueLength)
							{
								this.m_baselineReplayQueueLength = replayQueueLength;
							}
						}
					}
				}
				else
				{
					this.m_counters.ReplayQueueNotKeepingUp = 0L;
					if (replayQueueLength < this.m_baselineReplayQueueLength)
					{
						this.m_baselineReplayQueueLength = replayQueueLength;
					}
				}
			}
			this.m_lastCopyQueueLength = copyQueueLength;
			this.m_lastReplayQueueLength = replayQueueLength;
			this.m_lastCopiedGeneration = inspectorGenerationNumber;
			this.m_lastReplayedGeneration = replayGenerationNumber;
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "PerfCounterUpdater.UpdateQueueAlertPerfCounters(): Config '{0}': CopyQueueLength={1}, CopyQueueLengthBaseline={2}, CopyQueueNotKeepingUp={3}", new object[]
			{
				this.m_config.DisplayName,
				copyQueueLength,
				this.m_baselineCopyQueueLength,
				this.m_counters.CopyQueueNotKeepingUp
			});
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "PerfCounterUpdater.UpdateQueueAlertPerfCounters(): Config '{0}': ReplayQueueLength={1}, ReplayQueueLengthBaseline={2}, ReplayQueueNotKeepingUp={3}", new object[]
			{
				this.m_config.DisplayName,
				replayQueueLength,
				this.m_baselineReplayQueueLength,
				this.m_counters.ReplayQueueNotKeepingUp
			});
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00089F68 File Offset: 0x00088168
		private void ClearDatabaseMountedCounter()
		{
			this.m_amCounters.IsMounted.RawValue = 0L;
		}

		// Token: 0x04000CAD RID: 3245
		private readonly long CopyQueueAlertThreshold = (long)RegistryParameters.CopyQueueAlertThreshold;

		// Token: 0x04000CAE RID: 3246
		private readonly long ReplayQueueAlertThreshold = (long)RegistryParameters.ReplayQueueAlertThreshold;

		// Token: 0x04000CAF RID: 3247
		private readonly TimeSpan ExtraReplayLagAllowed = TimeSpan.FromMinutes((double)RegistryParameters.ExtraReplayLagAllowedMinutes);

		// Token: 0x04000CB0 RID: 3248
		private IPerfmonCounters m_counters;

		// Token: 0x04000CB1 RID: 3249
		private ActiveManagerPerfmonInstance m_amCounters;

		// Token: 0x04000CB2 RID: 3250
		private ReplayConfiguration m_config;

		// Token: 0x04000CB3 RID: 3251
		private bool m_firstTime = true;

		// Token: 0x04000CB4 RID: 3252
		private long m_baselineCopyQueueLength;

		// Token: 0x04000CB5 RID: 3253
		private long m_baselineReplayQueueLength;

		// Token: 0x04000CB6 RID: 3254
		private long m_lastCopyQueueLength;

		// Token: 0x04000CB7 RID: 3255
		private long m_lastReplayQueueLength;

		// Token: 0x04000CB8 RID: 3256
		private long m_lastCopiedGeneration;

		// Token: 0x04000CB9 RID: 3257
		private long m_lastReplayedGeneration;

		// Token: 0x04000CBA RID: 3258
		private int m_copyNotMakingProgressIntervals;

		// Token: 0x04000CBB RID: 3259
		private int m_replayNotMakingProgressIntervals;
	}
}
