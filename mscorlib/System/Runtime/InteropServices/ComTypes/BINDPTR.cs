using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A0B RID: 2571
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002CA4 RID: 11428
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002CA5 RID: 11429
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04002CA6 RID: 11430
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
