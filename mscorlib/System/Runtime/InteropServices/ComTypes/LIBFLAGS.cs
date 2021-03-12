using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A23 RID: 2595
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002D4C RID: 11596
		[__DynamicallyInvokable]
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002D4D RID: 11597
		[__DynamicallyInvokable]
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002D4E RID: 11598
		[__DynamicallyInvokable]
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002D4F RID: 11599
		[__DynamicallyInvokable]
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
