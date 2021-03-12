using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006BC RID: 1724
	[StructLayout(LayoutKind.Sequential)]
	internal class ProgIdRedirectionEntry
	{
		// Token: 0x04002294 RID: 8852
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgId;

		// Token: 0x04002295 RID: 8853
		public Guid RedirectedGuid;
	}
}
