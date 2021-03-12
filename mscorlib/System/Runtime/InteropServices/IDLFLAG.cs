using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000969 RID: 2409
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002B82 RID: 11138
		IDLFLAG_NONE = 0,
		// Token: 0x04002B83 RID: 11139
		IDLFLAG_FIN = 1,
		// Token: 0x04002B84 RID: 11140
		IDLFLAG_FOUT = 2,
		// Token: 0x04002B85 RID: 11141
		IDLFLAG_FLCID = 4,
		// Token: 0x04002B86 RID: 11142
		IDLFLAG_FRETVAL = 8
	}
}
