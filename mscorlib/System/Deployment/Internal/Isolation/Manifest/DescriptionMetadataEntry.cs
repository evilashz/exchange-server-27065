using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D7 RID: 1751
	[StructLayout(LayoutKind.Sequential)]
	internal class DescriptionMetadataEntry
	{
		// Token: 0x040022D7 RID: 8919
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Publisher;

		// Token: 0x040022D8 RID: 8920
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Product;

		// Token: 0x040022D9 RID: 8921
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040022DA RID: 8922
		[MarshalAs(UnmanagedType.LPWStr)]
		public string IconFile;

		// Token: 0x040022DB RID: 8923
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ErrorReportUrl;

		// Token: 0x040022DC RID: 8924
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SuiteName;
	}
}
