using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000403 RID: 1027
	[Flags]
	[__DynamicallyInvokable]
	public enum EventManifestOptions
	{
		// Token: 0x0400170C RID: 5900
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x0400170D RID: 5901
		[__DynamicallyInvokable]
		Strict = 1,
		// Token: 0x0400170E RID: 5902
		[__DynamicallyInvokable]
		AllCultures = 2,
		// Token: 0x0400170F RID: 5903
		[__DynamicallyInvokable]
		OnlyIfNeededForRegistration = 4,
		// Token: 0x04001710 RID: 5904
		[__DynamicallyInvokable]
		AllowEventSourceOverride = 8
	}
}
