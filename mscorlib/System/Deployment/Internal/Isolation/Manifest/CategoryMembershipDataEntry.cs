using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B0 RID: 1712
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipDataEntry
	{
		// Token: 0x04002276 RID: 8822
		public uint index;

		// Token: 0x04002277 RID: 8823
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;

		// Token: 0x04002278 RID: 8824
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;
	}
}
