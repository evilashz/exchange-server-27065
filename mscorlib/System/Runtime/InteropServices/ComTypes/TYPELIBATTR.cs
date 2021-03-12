using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A24 RID: 2596
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002D50 RID: 11600
		[__DynamicallyInvokable]
		public Guid guid;

		// Token: 0x04002D51 RID: 11601
		[__DynamicallyInvokable]
		public int lcid;

		// Token: 0x04002D52 RID: 11602
		[__DynamicallyInvokable]
		public SYSKIND syskind;

		// Token: 0x04002D53 RID: 11603
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		// Token: 0x04002D54 RID: 11604
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		// Token: 0x04002D55 RID: 11605
		[__DynamicallyInvokable]
		public LIBFLAGS wLibFlags;
	}
}
