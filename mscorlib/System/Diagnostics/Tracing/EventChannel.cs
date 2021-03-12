using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200040B RID: 1035
	[FriendAccessAllowed]
	[__DynamicallyInvokable]
	public enum EventChannel : byte
	{
		// Token: 0x04001742 RID: 5954
		[__DynamicallyInvokable]
		None,
		// Token: 0x04001743 RID: 5955
		[__DynamicallyInvokable]
		Admin = 16,
		// Token: 0x04001744 RID: 5956
		[__DynamicallyInvokable]
		Operational,
		// Token: 0x04001745 RID: 5957
		[__DynamicallyInvokable]
		Analytic,
		// Token: 0x04001746 RID: 5958
		[__DynamicallyInvokable]
		Debug
	}
}
