using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096A RID: 2410
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002B87 RID: 11143
		public int dwReserved;

		// Token: 0x04002B88 RID: 11144
		public IDLFLAG wIDLFlags;
	}
}
