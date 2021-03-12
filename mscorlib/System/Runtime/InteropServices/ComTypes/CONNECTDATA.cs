using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FD RID: 2557
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002C8E RID: 11406
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002C8F RID: 11407
		[__DynamicallyInvokable]
		public int dwCookie;
	}
}
