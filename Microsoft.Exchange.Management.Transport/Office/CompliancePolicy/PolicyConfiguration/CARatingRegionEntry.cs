using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200014D RID: 333
	[DataContract]
	public enum CARatingRegionEntry
	{
		// Token: 0x04000553 RID: 1363
		[EnumMember]
		us = 1,
		// Token: 0x04000554 RID: 1364
		[EnumMember]
		au,
		// Token: 0x04000555 RID: 1365
		[EnumMember]
		ca,
		// Token: 0x04000556 RID: 1366
		[EnumMember]
		de,
		// Token: 0x04000557 RID: 1367
		[EnumMember]
		fr,
		// Token: 0x04000558 RID: 1368
		[EnumMember]
		ie,
		// Token: 0x04000559 RID: 1369
		[EnumMember]
		jp,
		// Token: 0x0400055A RID: 1370
		[EnumMember]
		nz,
		// Token: 0x0400055B RID: 1371
		[EnumMember]
		gb
	}
}
