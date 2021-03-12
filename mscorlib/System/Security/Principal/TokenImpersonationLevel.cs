using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020002FB RID: 763
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TokenImpersonationLevel
	{
		// Token: 0x04000F66 RID: 3942
		[__DynamicallyInvokable]
		None,
		// Token: 0x04000F67 RID: 3943
		[__DynamicallyInvokable]
		Anonymous,
		// Token: 0x04000F68 RID: 3944
		[__DynamicallyInvokable]
		Identification,
		// Token: 0x04000F69 RID: 3945
		[__DynamicallyInvokable]
		Impersonation,
		// Token: 0x04000F6A RID: 3946
		[__DynamicallyInvokable]
		Delegation
	}
}
