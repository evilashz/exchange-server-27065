using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200062D RID: 1581
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FlowControl
	{
		// Token: 0x040020C6 RID: 8390
		[__DynamicallyInvokable]
		Branch,
		// Token: 0x040020C7 RID: 8391
		[__DynamicallyInvokable]
		Break,
		// Token: 0x040020C8 RID: 8392
		[__DynamicallyInvokable]
		Call,
		// Token: 0x040020C9 RID: 8393
		[__DynamicallyInvokable]
		Cond_Branch,
		// Token: 0x040020CA RID: 8394
		[__DynamicallyInvokable]
		Meta,
		// Token: 0x040020CB RID: 8395
		[__DynamicallyInvokable]
		Next,
		// Token: 0x040020CC RID: 8396
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Phi,
		// Token: 0x040020CD RID: 8397
		[__DynamicallyInvokable]
		Return,
		// Token: 0x040020CE RID: 8398
		[__DynamicallyInvokable]
		Throw
	}
}
