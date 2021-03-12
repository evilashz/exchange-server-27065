using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000954 RID: 2388
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CONNECTDATA instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002B2A RID: 11050
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002B2B RID: 11051
		public int dwCookie;
	}
}
