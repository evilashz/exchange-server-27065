using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B3 RID: 1715
	[StructLayout(LayoutKind.Sequential)]
	internal class SubcategoryMembershipEntry
	{
		// Token: 0x0400227C RID: 8828
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Subcategory;

		// Token: 0x0400227D RID: 8829
		public ISection CategoryMembershipData;
	}
}
