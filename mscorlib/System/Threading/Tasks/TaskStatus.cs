using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000530 RID: 1328
	[__DynamicallyInvokable]
	public enum TaskStatus
	{
		// Token: 0x04001A34 RID: 6708
		[__DynamicallyInvokable]
		Created,
		// Token: 0x04001A35 RID: 6709
		[__DynamicallyInvokable]
		WaitingForActivation,
		// Token: 0x04001A36 RID: 6710
		[__DynamicallyInvokable]
		WaitingToRun,
		// Token: 0x04001A37 RID: 6711
		[__DynamicallyInvokable]
		Running,
		// Token: 0x04001A38 RID: 6712
		[__DynamicallyInvokable]
		WaitingForChildrenToComplete,
		// Token: 0x04001A39 RID: 6713
		[__DynamicallyInvokable]
		RanToCompletion,
		// Token: 0x04001A3A RID: 6714
		[__DynamicallyInvokable]
		Canceled,
		// Token: 0x04001A3B RID: 6715
		[__DynamicallyInvokable]
		Faulted
	}
}
