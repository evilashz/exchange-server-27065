using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000417 RID: 1047
	[__DynamicallyInvokable]
	public enum EventFieldFormat
	{
		// Token: 0x0400176D RID: 5997
		[__DynamicallyInvokable]
		Default,
		// Token: 0x0400176E RID: 5998
		[__DynamicallyInvokable]
		String = 2,
		// Token: 0x0400176F RID: 5999
		[__DynamicallyInvokable]
		Boolean,
		// Token: 0x04001770 RID: 6000
		[__DynamicallyInvokable]
		Hexadecimal,
		// Token: 0x04001771 RID: 6001
		[__DynamicallyInvokable]
		Xml = 11,
		// Token: 0x04001772 RID: 6002
		[__DynamicallyInvokable]
		Json,
		// Token: 0x04001773 RID: 6003
		[__DynamicallyInvokable]
		HResult = 15
	}
}
