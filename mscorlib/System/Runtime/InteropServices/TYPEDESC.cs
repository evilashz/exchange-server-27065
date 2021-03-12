using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096D RID: 2413
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002B94 RID: 11156
		public IntPtr lpValue;

		// Token: 0x04002B95 RID: 11157
		public short vt;
	}
}
