using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096C RID: 2412
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002B92 RID: 11154
		public IntPtr lpVarValue;

		// Token: 0x04002B93 RID: 11155
		public PARAMFLAG wParamFlags;
	}
}
