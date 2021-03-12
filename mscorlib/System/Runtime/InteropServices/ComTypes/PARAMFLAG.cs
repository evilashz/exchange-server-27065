using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A14 RID: 2580
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002CEE RID: 11502
		[__DynamicallyInvokable]
		PARAMFLAG_NONE = 0,
		// Token: 0x04002CEF RID: 11503
		[__DynamicallyInvokable]
		PARAMFLAG_FIN = 1,
		// Token: 0x04002CF0 RID: 11504
		[__DynamicallyInvokable]
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002CF1 RID: 11505
		[__DynamicallyInvokable]
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002CF2 RID: 11506
		[__DynamicallyInvokable]
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002CF3 RID: 11507
		[__DynamicallyInvokable]
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002CF4 RID: 11508
		[__DynamicallyInvokable]
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002CF5 RID: 11509
		[__DynamicallyInvokable]
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
