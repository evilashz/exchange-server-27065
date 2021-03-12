using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A13 RID: 2579
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002CEB RID: 11499
		public IntPtr dwReserved;

		// Token: 0x04002CEC RID: 11500
		[__DynamicallyInvokable]
		public IDLFLAG wIDLFlags;
	}
}
