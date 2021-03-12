using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000120 RID: 288
	internal class NullConfigurationUpdater : IRunConfigurationUpdater
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x000313AC File Offset: 0x0002F5AC
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, bool forceRestart, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			return null;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000313AF File Offset: 0x0002F5AF
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			return null;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000313B2 File Offset: 0x0002F5B2
		public ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1)
		{
			return null;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000313B5 File Offset: 0x0002F5B5
		public void RunConfigurationUpdater(bool waitForCompletion, ReplayConfigChangeHints changeHint)
		{
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000313B7 File Offset: 0x0002F5B7
		public void Start()
		{
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000313B9 File Offset: 0x0002F5B9
		public void PrepareToStop()
		{
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000313BB File Offset: 0x0002F5BB
		public void Stop()
		{
		}
	}
}
