using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A1B RID: 2587
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002D0B RID: 11531
		[__DynamicallyInvokable]
		public short wCode;

		// Token: 0x04002D0C RID: 11532
		[__DynamicallyInvokable]
		public short wReserved;

		// Token: 0x04002D0D RID: 11533
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002D0E RID: 11534
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002D0F RID: 11535
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002D10 RID: 11536
		[__DynamicallyInvokable]
		public int dwHelpContext;

		// Token: 0x04002D11 RID: 11537
		public IntPtr pvReserved;

		// Token: 0x04002D12 RID: 11538
		public IntPtr pfnDeferredFillIn;

		// Token: 0x04002D13 RID: 11539
		[__DynamicallyInvokable]
		public int scode;
	}
}
