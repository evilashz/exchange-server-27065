using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000536 RID: 1334
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TaskCreationOptions
	{
		// Token: 0x04001A6C RID: 6764
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001A6D RID: 6765
		[__DynamicallyInvokable]
		PreferFairness = 1,
		// Token: 0x04001A6E RID: 6766
		[__DynamicallyInvokable]
		LongRunning = 2,
		// Token: 0x04001A6F RID: 6767
		[__DynamicallyInvokable]
		AttachedToParent = 4,
		// Token: 0x04001A70 RID: 6768
		[__DynamicallyInvokable]
		DenyChildAttach = 8,
		// Token: 0x04001A71 RID: 6769
		[__DynamicallyInvokable]
		HideScheduler = 16,
		// Token: 0x04001A72 RID: 6770
		[__DynamicallyInvokable]
		RunContinuationsAsynchronously = 64
	}
}
