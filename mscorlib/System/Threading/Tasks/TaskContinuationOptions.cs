using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000538 RID: 1336
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TaskContinuationOptions
	{
		// Token: 0x04001A7E RID: 6782
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001A7F RID: 6783
		[__DynamicallyInvokable]
		PreferFairness = 1,
		// Token: 0x04001A80 RID: 6784
		[__DynamicallyInvokable]
		LongRunning = 2,
		// Token: 0x04001A81 RID: 6785
		[__DynamicallyInvokable]
		AttachedToParent = 4,
		// Token: 0x04001A82 RID: 6786
		[__DynamicallyInvokable]
		DenyChildAttach = 8,
		// Token: 0x04001A83 RID: 6787
		[__DynamicallyInvokable]
		HideScheduler = 16,
		// Token: 0x04001A84 RID: 6788
		[__DynamicallyInvokable]
		LazyCancellation = 32,
		// Token: 0x04001A85 RID: 6789
		[__DynamicallyInvokable]
		RunContinuationsAsynchronously = 64,
		// Token: 0x04001A86 RID: 6790
		[__DynamicallyInvokable]
		NotOnRanToCompletion = 65536,
		// Token: 0x04001A87 RID: 6791
		[__DynamicallyInvokable]
		NotOnFaulted = 131072,
		// Token: 0x04001A88 RID: 6792
		[__DynamicallyInvokable]
		NotOnCanceled = 262144,
		// Token: 0x04001A89 RID: 6793
		[__DynamicallyInvokable]
		OnlyOnRanToCompletion = 393216,
		// Token: 0x04001A8A RID: 6794
		[__DynamicallyInvokable]
		OnlyOnFaulted = 327680,
		// Token: 0x04001A8B RID: 6795
		[__DynamicallyInvokable]
		OnlyOnCanceled = 196608,
		// Token: 0x04001A8C RID: 6796
		[__DynamicallyInvokable]
		ExecuteSynchronously = 524288
	}
}
