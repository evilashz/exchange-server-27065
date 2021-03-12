using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000971 RID: 2417
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002BA1 RID: 11169
		public short wCode;

		// Token: 0x04002BA2 RID: 11170
		public short wReserved;

		// Token: 0x04002BA3 RID: 11171
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002BA4 RID: 11172
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002BA5 RID: 11173
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002BA6 RID: 11174
		public int dwHelpContext;

		// Token: 0x04002BA7 RID: 11175
		public IntPtr pvReserved;

		// Token: 0x04002BA8 RID: 11176
		public IntPtr pfnDeferredFillIn;
	}
}
