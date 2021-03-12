using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D1 RID: 1745
	[StructLayout(LayoutKind.Sequential)]
	internal class PermissionSetEntry
	{
		// Token: 0x040022CF RID: 8911
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;

		// Token: 0x040022D0 RID: 8912
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XmlSegment;
	}
}
