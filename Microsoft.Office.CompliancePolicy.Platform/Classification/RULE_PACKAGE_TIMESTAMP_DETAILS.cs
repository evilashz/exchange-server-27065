using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000026 RID: 38
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RULE_PACKAGE_TIMESTAMP_DETAILS
	{
		// Token: 0x04000043 RID: 67
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageSetID;

		// Token: 0x04000044 RID: 68
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageID;

		// Token: 0x04000045 RID: 69
		[MarshalAs(UnmanagedType.BStr)]
		public string LastUpdatedTime;

		// Token: 0x04000046 RID: 70
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool RulePackageChanged;
	}
}
