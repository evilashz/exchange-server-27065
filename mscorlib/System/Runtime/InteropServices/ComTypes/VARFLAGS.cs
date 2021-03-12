using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A20 RID: 2592
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum VARFLAGS : short
	{
		// Token: 0x04002D39 RID: 11577
		[__DynamicallyInvokable]
		VARFLAG_FREADONLY = 1,
		// Token: 0x04002D3A RID: 11578
		[__DynamicallyInvokable]
		VARFLAG_FSOURCE = 2,
		// Token: 0x04002D3B RID: 11579
		[__DynamicallyInvokable]
		VARFLAG_FBINDABLE = 4,
		// Token: 0x04002D3C RID: 11580
		[__DynamicallyInvokable]
		VARFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002D3D RID: 11581
		[__DynamicallyInvokable]
		VARFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002D3E RID: 11582
		[__DynamicallyInvokable]
		VARFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002D3F RID: 11583
		[__DynamicallyInvokable]
		VARFLAG_FHIDDEN = 64,
		// Token: 0x04002D40 RID: 11584
		[__DynamicallyInvokable]
		VARFLAG_FRESTRICTED = 128,
		// Token: 0x04002D41 RID: 11585
		[__DynamicallyInvokable]
		VARFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002D42 RID: 11586
		[__DynamicallyInvokable]
		VARFLAG_FUIDEFAULT = 512,
		// Token: 0x04002D43 RID: 11587
		[__DynamicallyInvokable]
		VARFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002D44 RID: 11588
		[__DynamicallyInvokable]
		VARFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002D45 RID: 11589
		[__DynamicallyInvokable]
		VARFLAG_FIMMEDIATEBIND = 4096
	}
}
