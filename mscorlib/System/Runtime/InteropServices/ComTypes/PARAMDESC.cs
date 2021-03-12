using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A15 RID: 2581
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002CF6 RID: 11510
		public IntPtr lpVarValue;

		// Token: 0x04002CF7 RID: 11511
		[__DynamicallyInvokable]
		public PARAMFLAG wParamFlags;
	}
}
