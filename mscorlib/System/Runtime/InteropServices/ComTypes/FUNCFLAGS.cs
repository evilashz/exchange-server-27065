using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A1F RID: 2591
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		// Token: 0x04002D2B RID: 11563
		[__DynamicallyInvokable]
		FUNCFLAG_FRESTRICTED = 1,
		// Token: 0x04002D2C RID: 11564
		[__DynamicallyInvokable]
		FUNCFLAG_FSOURCE = 2,
		// Token: 0x04002D2D RID: 11565
		[__DynamicallyInvokable]
		FUNCFLAG_FBINDABLE = 4,
		// Token: 0x04002D2E RID: 11566
		[__DynamicallyInvokable]
		FUNCFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002D2F RID: 11567
		[__DynamicallyInvokable]
		FUNCFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002D30 RID: 11568
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002D31 RID: 11569
		[__DynamicallyInvokable]
		FUNCFLAG_FHIDDEN = 64,
		// Token: 0x04002D32 RID: 11570
		[__DynamicallyInvokable]
		FUNCFLAG_FUSESGETLASTERROR = 128,
		// Token: 0x04002D33 RID: 11571
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002D34 RID: 11572
		[__DynamicallyInvokable]
		FUNCFLAG_FUIDEFAULT = 512,
		// Token: 0x04002D35 RID: 11573
		[__DynamicallyInvokable]
		FUNCFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002D36 RID: 11574
		[__DynamicallyInvokable]
		FUNCFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002D37 RID: 11575
		[__DynamicallyInvokable]
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
