using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000024 RID: 36
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RULE_PACKAGE_DETAILS
	{
		// Token: 0x0400003C RID: 60
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageSetID;

		// Token: 0x0400003D RID: 61
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageID;

		// Token: 0x0400003E RID: 62
		[MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		public string[] RuleIDs;
	}
}
