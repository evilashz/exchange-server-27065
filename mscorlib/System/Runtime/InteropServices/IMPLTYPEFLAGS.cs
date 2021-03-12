using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000966 RID: 2406
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		// Token: 0x04002B5E RID: 11102
		IMPLTYPEFLAG_FDEFAULT = 1,
		// Token: 0x04002B5F RID: 11103
		IMPLTYPEFLAG_FSOURCE = 2,
		// Token: 0x04002B60 RID: 11104
		IMPLTYPEFLAG_FRESTRICTED = 4,
		// Token: 0x04002B61 RID: 11105
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
