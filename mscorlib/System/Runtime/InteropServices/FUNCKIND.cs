using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000972 RID: 2418
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCKIND
	{
		// Token: 0x04002BAA RID: 11178
		FUNC_VIRTUAL,
		// Token: 0x04002BAB RID: 11179
		FUNC_PUREVIRTUAL,
		// Token: 0x04002BAC RID: 11180
		FUNC_NONVIRTUAL,
		// Token: 0x04002BAD RID: 11181
		FUNC_STATIC,
		// Token: 0x04002BAE RID: 11182
		FUNC_DISPATCH
	}
}
