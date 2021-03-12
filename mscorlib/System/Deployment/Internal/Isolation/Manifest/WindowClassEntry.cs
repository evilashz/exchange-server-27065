using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C8 RID: 1736
	[StructLayout(LayoutKind.Sequential)]
	internal class WindowClassEntry
	{
		// Token: 0x040022BB RID: 8891
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;

		// Token: 0x040022BC RID: 8892
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostDll;

		// Token: 0x040022BD RID: 8893
		public bool fVersioned;
	}
}
