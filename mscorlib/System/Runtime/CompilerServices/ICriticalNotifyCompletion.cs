using System;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C8 RID: 2248
	[__DynamicallyInvokable]
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		// Token: 0x06005D1E RID: 23838
		[SecurityCritical]
		[__DynamicallyInvokable]
		void UnsafeOnCompleted(Action continuation);
	}
}
