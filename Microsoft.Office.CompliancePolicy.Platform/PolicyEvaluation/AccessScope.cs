using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000B3 RID: 179
	[Flags]
	public enum AccessScope
	{
		// Token: 0x040002DD RID: 733
		[EnumMember]
		Internal = 1,
		// Token: 0x040002DE RID: 734
		[EnumMember]
		External = 2
	}
}
