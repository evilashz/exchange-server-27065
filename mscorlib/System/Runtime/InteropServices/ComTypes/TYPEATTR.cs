using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A10 RID: 2576
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002CC6 RID: 11462
		[__DynamicallyInvokable]
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002CC7 RID: 11463
		[__DynamicallyInvokable]
		public Guid guid;

		// Token: 0x04002CC8 RID: 11464
		[__DynamicallyInvokable]
		public int lcid;

		// Token: 0x04002CC9 RID: 11465
		[__DynamicallyInvokable]
		public int dwReserved;

		// Token: 0x04002CCA RID: 11466
		[__DynamicallyInvokable]
		public int memidConstructor;

		// Token: 0x04002CCB RID: 11467
		[__DynamicallyInvokable]
		public int memidDestructor;

		// Token: 0x04002CCC RID: 11468
		public IntPtr lpstrSchema;

		// Token: 0x04002CCD RID: 11469
		[__DynamicallyInvokable]
		public int cbSizeInstance;

		// Token: 0x04002CCE RID: 11470
		[__DynamicallyInvokable]
		public TYPEKIND typekind;

		// Token: 0x04002CCF RID: 11471
		[__DynamicallyInvokable]
		public short cFuncs;

		// Token: 0x04002CD0 RID: 11472
		[__DynamicallyInvokable]
		public short cVars;

		// Token: 0x04002CD1 RID: 11473
		[__DynamicallyInvokable]
		public short cImplTypes;

		// Token: 0x04002CD2 RID: 11474
		[__DynamicallyInvokable]
		public short cbSizeVft;

		// Token: 0x04002CD3 RID: 11475
		[__DynamicallyInvokable]
		public short cbAlignment;

		// Token: 0x04002CD4 RID: 11476
		[__DynamicallyInvokable]
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002CD5 RID: 11477
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		// Token: 0x04002CD6 RID: 11478
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		// Token: 0x04002CD7 RID: 11479
		[__DynamicallyInvokable]
		public TYPEDESC tdescAlias;

		// Token: 0x04002CD8 RID: 11480
		[__DynamicallyInvokable]
		public IDLDESC idldescType;
	}
}
