using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000025 RID: 37
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RULE_PACKAGE_REQUEST_DETAILS
	{
		// Token: 0x0400003F RID: 63
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageSetID;

		// Token: 0x04000040 RID: 64
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageID;

		// Token: 0x04000041 RID: 65
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackage;

		// Token: 0x04000042 RID: 66
		[MarshalAs(UnmanagedType.BStr)]
		public string LastUpdatedTime;
	}
}
