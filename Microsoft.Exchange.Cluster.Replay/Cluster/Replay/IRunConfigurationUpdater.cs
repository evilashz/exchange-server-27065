using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRunConfigurationUpdater
	{
		// Token: 0x06000A4A RID: 2634
		void Start();

		// Token: 0x06000A4B RID: 2635
		void PrepareToStop();

		// Token: 0x06000A4C RID: 2636
		void Stop();

		// Token: 0x06000A4D RID: 2637
		void RunConfigurationUpdater(bool waitForCompletion, ReplayConfigChangeHints changeHint);

		// Token: 0x06000A4E RID: 2638
		ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1);

		// Token: 0x06000A4F RID: 2639
		ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1);

		// Token: 0x06000A50 RID: 2640
		ReplayQueuedItemBase NotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, bool forceRestart, ReplayConfigChangeHints changeHint, int waitTimeoutMs = -1);
	}
}
