using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DA RID: 1754
	[StructLayout(LayoutKind.Sequential)]
	internal class DeploymentMetadataEntry
	{
		// Token: 0x040022E4 RID: 8932
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DeploymentProviderCodebase;

		// Token: 0x040022E5 RID: 8933
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MinimumRequiredVersion;

		// Token: 0x040022E6 RID: 8934
		public ushort MaximumAge;

		// Token: 0x040022E7 RID: 8935
		public byte MaximumAge_Unit;

		// Token: 0x040022E8 RID: 8936
		public uint DeploymentFlags;
	}
}
