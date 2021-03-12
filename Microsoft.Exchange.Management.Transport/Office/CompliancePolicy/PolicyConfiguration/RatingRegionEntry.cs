using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000146 RID: 326
	[DataContract]
	public enum RatingRegionEntry
	{
		// Token: 0x040004D7 RID: 1239
		[EnumMember]
		us = 1,
		// Token: 0x040004D8 RID: 1240
		[EnumMember]
		au,
		// Token: 0x040004D9 RID: 1241
		[EnumMember]
		ca,
		// Token: 0x040004DA RID: 1242
		[EnumMember]
		de,
		// Token: 0x040004DB RID: 1243
		[EnumMember]
		fr,
		// Token: 0x040004DC RID: 1244
		[EnumMember]
		ie,
		// Token: 0x040004DD RID: 1245
		[EnumMember]
		jp,
		// Token: 0x040004DE RID: 1246
		[EnumMember]
		nz,
		// Token: 0x040004DF RID: 1247
		[EnumMember]
		gb
	}
}
