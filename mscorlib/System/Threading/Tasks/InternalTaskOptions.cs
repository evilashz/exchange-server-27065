using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000537 RID: 1335
	[Flags]
	[Serializable]
	internal enum InternalTaskOptions
	{
		// Token: 0x04001A74 RID: 6772
		None = 0,
		// Token: 0x04001A75 RID: 6773
		InternalOptionsMask = 65280,
		// Token: 0x04001A76 RID: 6774
		ChildReplica = 256,
		// Token: 0x04001A77 RID: 6775
		ContinuationTask = 512,
		// Token: 0x04001A78 RID: 6776
		PromiseTask = 1024,
		// Token: 0x04001A79 RID: 6777
		SelfReplicating = 2048,
		// Token: 0x04001A7A RID: 6778
		LazyCancellation = 4096,
		// Token: 0x04001A7B RID: 6779
		QueuedByRuntime = 8192,
		// Token: 0x04001A7C RID: 6780
		DoNotDispose = 16384
	}
}
