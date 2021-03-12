using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DD RID: 1757
	[StructLayout(LayoutKind.Sequential)]
	internal class DependentOSMetadataEntry
	{
		// Token: 0x040022EF RID: 8943
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040022F0 RID: 8944
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040022F1 RID: 8945
		public ushort MajorVersion;

		// Token: 0x040022F2 RID: 8946
		public ushort MinorVersion;

		// Token: 0x040022F3 RID: 8947
		public ushort BuildNumber;

		// Token: 0x040022F4 RID: 8948
		public byte ServicePackMajor;

		// Token: 0x040022F5 RID: 8949
		public byte ServicePackMinor;
	}
}
