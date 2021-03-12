using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000022 RID: 34
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CLASSIFICATION_DEFINITION_DETAILS
	{
		// Token: 0x04000033 RID: 51
		[MarshalAs(UnmanagedType.BStr)]
		public string PublisherName;

		// Token: 0x04000034 RID: 52
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageName;

		// Token: 0x04000035 RID: 53
		[MarshalAs(UnmanagedType.BStr)]
		public string RulePackageDesc;

		// Token: 0x04000036 RID: 54
		[MarshalAs(UnmanagedType.BStr)]
		public string DefinitionName;

		// Token: 0x04000037 RID: 55
		[MarshalAs(UnmanagedType.BStr)]
		public string Description;
	}
}
