using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A16 RID: 2582
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002CF8 RID: 11512
		public IntPtr lpValue;

		// Token: 0x04002CF9 RID: 11513
		[__DynamicallyInvokable]
		public short vt;
	}
}
