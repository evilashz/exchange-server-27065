using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000964 RID: 2404
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum TYPEKIND
	{
		// Token: 0x04002B44 RID: 11076
		TKIND_ENUM,
		// Token: 0x04002B45 RID: 11077
		TKIND_RECORD,
		// Token: 0x04002B46 RID: 11078
		TKIND_MODULE,
		// Token: 0x04002B47 RID: 11079
		TKIND_INTERFACE,
		// Token: 0x04002B48 RID: 11080
		TKIND_DISPATCH,
		// Token: 0x04002B49 RID: 11081
		TKIND_COCLASS,
		// Token: 0x04002B4A RID: 11082
		TKIND_ALIAS,
		// Token: 0x04002B4B RID: 11083
		TKIND_UNION,
		// Token: 0x04002B4C RID: 11084
		TKIND_MAX
	}
}
