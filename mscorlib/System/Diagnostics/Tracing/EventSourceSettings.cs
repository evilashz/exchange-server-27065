using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F4 RID: 1012
	[Flags]
	[__DynamicallyInvokable]
	public enum EventSourceSettings
	{
		// Token: 0x040016B6 RID: 5814
		[__DynamicallyInvokable]
		Default = 0,
		// Token: 0x040016B7 RID: 5815
		[__DynamicallyInvokable]
		ThrowOnEventWriteErrors = 1,
		// Token: 0x040016B8 RID: 5816
		[__DynamicallyInvokable]
		EtwManifestEventFormat = 4,
		// Token: 0x040016B9 RID: 5817
		[__DynamicallyInvokable]
		EtwSelfDescribingEventFormat = 8
	}
}
