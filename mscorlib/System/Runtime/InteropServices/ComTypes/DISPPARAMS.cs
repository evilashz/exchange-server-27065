using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A1A RID: 2586
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002D07 RID: 11527
		[__DynamicallyInvokable]
		public IntPtr rgvarg;

		// Token: 0x04002D08 RID: 11528
		[__DynamicallyInvokable]
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002D09 RID: 11529
		[__DynamicallyInvokable]
		public int cArgs;

		// Token: 0x04002D0A RID: 11530
		[__DynamicallyInvokable]
		public int cNamedArgs;
	}
}
