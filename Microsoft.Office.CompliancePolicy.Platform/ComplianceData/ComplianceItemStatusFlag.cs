using System;

namespace Microsoft.Office.CompliancePolicy.ComplianceData
{
	// Token: 0x02000056 RID: 86
	[Flags]
	public enum ComplianceItemStatusFlag : uint
	{
		// Token: 0x04000106 RID: 262
		None = 0U,
		// Token: 0x04000107 RID: 263
		Preserved = 1U,
		// Token: 0x04000108 RID: 264
		Archived = 2U,
		// Token: 0x04000109 RID: 265
		Recycled = 4U
	}
}
