using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003FE RID: 1022
	[__DynamicallyInvokable]
	public enum EventCommand
	{
		// Token: 0x040016F1 RID: 5873
		[__DynamicallyInvokable]
		Update,
		// Token: 0x040016F2 RID: 5874
		[__DynamicallyInvokable]
		SendManifest = -1,
		// Token: 0x040016F3 RID: 5875
		[__DynamicallyInvokable]
		Enable = -2,
		// Token: 0x040016F4 RID: 5876
		[__DynamicallyInvokable]
		Disable = -3
	}
}
