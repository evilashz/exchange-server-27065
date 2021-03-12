using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E0 RID: 1760
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworksMetadataEntry
	{
		// Token: 0x040022FE RID: 8958
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;
	}
}
