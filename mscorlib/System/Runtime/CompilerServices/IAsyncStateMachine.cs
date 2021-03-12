using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C6 RID: 2246
	[__DynamicallyInvokable]
	public interface IAsyncStateMachine
	{
		// Token: 0x06005D1B RID: 23835
		[__DynamicallyInvokable]
		void MoveNext();

		// Token: 0x06005D1C RID: 23836
		[__DynamicallyInvokable]
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
