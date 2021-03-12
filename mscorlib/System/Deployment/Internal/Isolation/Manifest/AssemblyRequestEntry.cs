using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D4 RID: 1748
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyRequestEntry
	{
		// Token: 0x040022D3 RID: 8915
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040022D4 RID: 8916
		[MarshalAs(UnmanagedType.LPWStr)]
		public string permissionSetID;
	}
}
