using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C7 RID: 2247
	[__DynamicallyInvokable]
	public interface INotifyCompletion
	{
		// Token: 0x06005D1D RID: 23837
		[__DynamicallyInvokable]
		void OnCompleted(Action continuation);
	}
}
