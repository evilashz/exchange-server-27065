using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000967 RID: 2407
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002B62 RID: 11106
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002B63 RID: 11107
		public Guid guid;

		// Token: 0x04002B64 RID: 11108
		public int lcid;

		// Token: 0x04002B65 RID: 11109
		public int dwReserved;

		// Token: 0x04002B66 RID: 11110
		public int memidConstructor;

		// Token: 0x04002B67 RID: 11111
		public int memidDestructor;

		// Token: 0x04002B68 RID: 11112
		public IntPtr lpstrSchema;

		// Token: 0x04002B69 RID: 11113
		public int cbSizeInstance;

		// Token: 0x04002B6A RID: 11114
		public TYPEKIND typekind;

		// Token: 0x04002B6B RID: 11115
		public short cFuncs;

		// Token: 0x04002B6C RID: 11116
		public short cVars;

		// Token: 0x04002B6D RID: 11117
		public short cImplTypes;

		// Token: 0x04002B6E RID: 11118
		public short cbSizeVft;

		// Token: 0x04002B6F RID: 11119
		public short cbAlignment;

		// Token: 0x04002B70 RID: 11120
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002B71 RID: 11121
		public short wMajorVerNum;

		// Token: 0x04002B72 RID: 11122
		public short wMinorVerNum;

		// Token: 0x04002B73 RID: 11123
		public TYPEDESC tdescAlias;

		// Token: 0x04002B74 RID: 11124
		public IDLDESC idldescType;
	}
}
