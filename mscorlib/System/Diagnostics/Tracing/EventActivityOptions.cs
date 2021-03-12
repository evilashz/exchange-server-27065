using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003EF RID: 1007
	[Flags]
	[__DynamicallyInvokable]
	public enum EventActivityOptions
	{
		// Token: 0x04001672 RID: 5746
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001673 RID: 5747
		[__DynamicallyInvokable]
		Disable = 2,
		// Token: 0x04001674 RID: 5748
		[__DynamicallyInvokable]
		Recursive = 4,
		// Token: 0x04001675 RID: 5749
		[__DynamicallyInvokable]
		Detachable = 8
	}
}
