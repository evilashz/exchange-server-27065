using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A0E RID: 2574
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		// Token: 0x04002CB2 RID: 11442
		[__DynamicallyInvokable]
		TYPEFLAG_FAPPOBJECT = 1,
		// Token: 0x04002CB3 RID: 11443
		[__DynamicallyInvokable]
		TYPEFLAG_FCANCREATE = 2,
		// Token: 0x04002CB4 RID: 11444
		[__DynamicallyInvokable]
		TYPEFLAG_FLICENSED = 4,
		// Token: 0x04002CB5 RID: 11445
		[__DynamicallyInvokable]
		TYPEFLAG_FPREDECLID = 8,
		// Token: 0x04002CB6 RID: 11446
		[__DynamicallyInvokable]
		TYPEFLAG_FHIDDEN = 16,
		// Token: 0x04002CB7 RID: 11447
		[__DynamicallyInvokable]
		TYPEFLAG_FCONTROL = 32,
		// Token: 0x04002CB8 RID: 11448
		[__DynamicallyInvokable]
		TYPEFLAG_FDUAL = 64,
		// Token: 0x04002CB9 RID: 11449
		[__DynamicallyInvokable]
		TYPEFLAG_FNONEXTENSIBLE = 128,
		// Token: 0x04002CBA RID: 11450
		[__DynamicallyInvokable]
		TYPEFLAG_FOLEAUTOMATION = 256,
		// Token: 0x04002CBB RID: 11451
		[__DynamicallyInvokable]
		TYPEFLAG_FRESTRICTED = 512,
		// Token: 0x04002CBC RID: 11452
		[__DynamicallyInvokable]
		TYPEFLAG_FAGGREGATABLE = 1024,
		// Token: 0x04002CBD RID: 11453
		[__DynamicallyInvokable]
		TYPEFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002CBE RID: 11454
		[__DynamicallyInvokable]
		TYPEFLAG_FDISPATCHABLE = 4096,
		// Token: 0x04002CBF RID: 11455
		[__DynamicallyInvokable]
		TYPEFLAG_FREVERSEBIND = 8192,
		// Token: 0x04002CC0 RID: 11456
		[__DynamicallyInvokable]
		TYPEFLAG_FPROXY = 16384
	}
}
