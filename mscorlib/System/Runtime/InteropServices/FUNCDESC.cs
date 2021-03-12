using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000968 RID: 2408
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FUNCDESC
	{
		// Token: 0x04002B75 RID: 11125
		public int memid;

		// Token: 0x04002B76 RID: 11126
		public IntPtr lprgscode;

		// Token: 0x04002B77 RID: 11127
		public IntPtr lprgelemdescParam;

		// Token: 0x04002B78 RID: 11128
		public FUNCKIND funckind;

		// Token: 0x04002B79 RID: 11129
		public INVOKEKIND invkind;

		// Token: 0x04002B7A RID: 11130
		public CALLCONV callconv;

		// Token: 0x04002B7B RID: 11131
		public short cParams;

		// Token: 0x04002B7C RID: 11132
		public short cParamsOpt;

		// Token: 0x04002B7D RID: 11133
		public short oVft;

		// Token: 0x04002B7E RID: 11134
		public short cScodes;

		// Token: 0x04002B7F RID: 11135
		public ELEMDESC elemdescFunc;

		// Token: 0x04002B80 RID: 11136
		public short wFuncFlags;
	}
}
