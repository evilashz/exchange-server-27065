using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002CD RID: 717
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigurationUpdater : TimerComponent, IRunConfigurationUpdater
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x00078FC6 File Offset: 0x000771C6
		public ConfigurationUpdater(ReplicaInstanceManager riManager, ReplaySystemQueue systemQueue) : base(TimeSpan.Zero, TimeSpan.FromMilliseconds((double)RegistryParameters.ConfigUpdaterTimerIntervalSlow), "PeriodicConfigurationChecker")
		{
			this.m_riManager = riManager;
			this.m_systemQueue = systemQueue;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00078FF1 File Offset: 0x000771F1
		public void RunConfigurationUpdater(bool waitForCompletion, ReplayConfigChangeHints changeHint)
		{
			Dependencies.ADConfig.Refresh(string.Format("ConfigurationUpdater.RunConfigurationUpdater {0}", changeHint));
			this.RunConfigurationUpdaterImpl(waitForCompletion, false, changeHint);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00079016 File Offset: 0x00077216
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			return this.NotifyChangedReplayConfiguration(dbGuid, waitForCompletion, !waitForCompletion, false, changeHint, waitTimeoutMs);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00079028 File Offset: 0x00077228
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			return this.NotifyChangedReplayConfiguration(dbGuid, waitForCompletion, exitAfterEnqueueing, isHighPriority, false, changeHint, waitTimeoutMs);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0007903C File Offset: 0x0007723C
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, bool forceRestart, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration() called: dbGuid='{0}', waitForCompletion='{1}', exitAfterEnqueueing='{2}', isHighPriority='{3}', changeHint='{4}'", new object[]
			{
				dbGuid,
				waitForCompletion,
				exitAfterEnqueueing,
				isHighPriority,
				changeHint
			});
			ReplaySystemQueuedItem replaySystemQueuedItem = new ReplaySystemRunConfigurationUpdaterSingleConfig(this.m_riManager, dbGuid, changeHint);
			ReplaySystemRunConfigurationUpdaterSingleConfig replaySystemRunConfigurationUpdaterSingleConfig = (ReplaySystemRunConfigurationUpdaterSingleConfig)replaySystemQueuedItem;
			replaySystemRunConfigurationUpdaterSingleConfig.WaitForCompletion = waitForCompletion;
			replaySystemRunConfigurationUpdaterSingleConfig.IsHighPriority = isHighPriority;
			replaySystemRunConfigurationUpdaterSingleConfig.ForceRestart = forceRestart;
			ReplaySystemQueuedItem replaySystemQueuedItem2;
			EventWaitHandle eventWaitHandle;
			if (isHighPriority)
			{
				if (this.m_systemQueue.EnqueueHighPriority(replaySystemQueuedItem, null))
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration successfully enqueued a *HIGH* priority ReplaySystemRunConfigurationUpdaterSingleConfig operation for DB '{0}'.", dbGuid);
				}
				else
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration failed to enqueue a *HIGH* priority ReplaySystemRunConfigurationUpdaterSingleConfig operation for DB '{0}'.", dbGuid);
				}
			}
			else if (!this.m_systemQueue.EnqueueUniqueItem(replaySystemQueuedItem, null, false, out replaySystemQueuedItem2, out eventWaitHandle))
			{
				if (replaySystemQueuedItem2 != null)
				{
					replaySystemQueuedItem = replaySystemQueuedItem2;
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration did not enqueue a ReplaySystemRunConfigurationUpdaterSingleConfig operation for DB '{0}' because there is already an equivalent/superset one in the queue.", dbGuid);
				}
				else
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration failed to enqueue a ReplaySystemRunConfigurationUpdaterSingleConfig operation for DB '{0}'.", dbGuid);
				}
			}
			else
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ConfigurationUpdater: NotifyChangedReplayConfiguration successfully enqueued a ReplaySystemRunConfigurationUpdaterSingleConfig operation for DB '{0}'.", dbGuid);
			}
			if (!exitAfterEnqueueing && waitForCompletion)
			{
				replaySystemQueuedItem.Wait(waitTimeoutMs);
			}
			return replaySystemQueuedItem;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00079181 File Offset: 0x00077381
		protected override void TimerCallbackInternal()
		{
			this.RunConfigurationUpdaterImpl(false, true, ReplayConfigChangeHints.PeriodicFullScan);
			this.CheckHealth();
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00079194 File Offset: 0x00077394
		private void CheckHealth()
		{
			List<ReplicaInstance> allReplicaInstances = this.m_riManager.GetAllReplicaInstances();
			foreach (ReplicaInstance replicaInstance in allReplicaInstances)
			{
				replicaInstance.CheckHealth();
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000791F0 File Offset: 0x000773F0
		private void RunConfigurationUpdaterImpl(bool waitForCompletion, bool includeInProgressItem, ReplayConfigChangeHints changeHint)
		{
			ExTraceGlobals.ReplayManagerTracer.TraceDebug<bool, bool, ReplayConfigChangeHints>((long)this.GetHashCode(), "ConfigurationUpdater: RunConfigurationUpdater() called for a full scan: waitForCompletion='{0}', includeInProgressItem='{1}', changeHint='{2}'", waitForCompletion, includeInProgressItem, changeHint);
			ReplaySystemQueuedItem replaySystemQueuedItem = new ReplaySystemRunConfigurationUpdaterFullScan(this.m_riManager, changeHint);
			((ReplaySystemRunConfigurationUpdaterFullScan)replaySystemQueuedItem).WaitForCompletion = waitForCompletion;
			ReplaySystemQueuedItem replaySystemQueuedItem2;
			EventWaitHandle eventWaitHandle;
			if (!this.m_systemQueue.EnqueueUniqueItem(replaySystemQueuedItem, null, includeInProgressItem, out replaySystemQueuedItem2, out eventWaitHandle))
			{
				if (replaySystemQueuedItem2 != null)
				{
					replaySystemQueuedItem = replaySystemQueuedItem2;
					ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ConfigurationUpdater: RunConfigurationUpdater did not enqueue a ReplaySystemRunConfigurationUpdaterFullScan operation because there is already one in the queue.");
				}
				else
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ConfigurationUpdater: RunConfigurationUpdater failed to enqueue a ReplaySystemRunConfigurationUpdaterFullScan operation.");
				}
			}
			else
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ConfigurationUpdater: RunConfigurationUpdater successfully enqueued a ReplaySystemRunConfigurationUpdaterFullScan operation.");
			}
			if (waitForCompletion)
			{
				replaySystemQueuedItem.Wait();
			}
		}

		// Token: 0x04000BB1 RID: 2993
		private ReplicaInstanceManager m_riManager;

		// Token: 0x04000BB2 RID: 2994
		private ReplaySystemQueue m_systemQueue;
	}
}
