using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006BF RID: 1727
	[StructLayout(LayoutKind.Sequential)]
	internal class CLRSurrogateEntry
	{
		// Token: 0x04002298 RID: 8856
		public Guid Clsid;

		// Token: 0x04002299 RID: 8857
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400229A RID: 8858
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;
	}
}
