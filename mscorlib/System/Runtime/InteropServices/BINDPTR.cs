using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000962 RID: 2402
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.BINDPTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002B40 RID: 11072
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002B41 RID: 11073
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04002B42 RID: 11074
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
