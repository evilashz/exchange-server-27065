using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000970 RID: 2416
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002B9D RID: 11165
		public IntPtr rgvarg;

		// Token: 0x04002B9E RID: 11166
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002B9F RID: 11167
		public int cArgs;

		// Token: 0x04002BA0 RID: 11168
		public int cNamedArgs;
	}
}
