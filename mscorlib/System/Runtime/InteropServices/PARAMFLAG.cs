using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096B RID: 2411
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002B8A RID: 11146
		PARAMFLAG_NONE = 0,
		// Token: 0x04002B8B RID: 11147
		PARAMFLAG_FIN = 1,
		// Token: 0x04002B8C RID: 11148
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002B8D RID: 11149
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002B8E RID: 11150
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002B8F RID: 11151
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002B90 RID: 11152
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002B91 RID: 11153
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
