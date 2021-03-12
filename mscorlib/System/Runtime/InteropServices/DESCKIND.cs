using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000961 RID: 2401
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DESCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum DESCKIND
	{
		// Token: 0x04002B3A RID: 11066
		DESCKIND_NONE,
		// Token: 0x04002B3B RID: 11067
		DESCKIND_FUNCDESC,
		// Token: 0x04002B3C RID: 11068
		DESCKIND_VARDESC,
		// Token: 0x04002B3D RID: 11069
		DESCKIND_TYPECOMP,
		// Token: 0x04002B3E RID: 11070
		DESCKIND_IMPLICITAPPOBJ,
		// Token: 0x04002B3F RID: 11071
		DESCKIND_MAX
	}
}
