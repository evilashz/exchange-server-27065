using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000150 RID: 336
	[DataContract]
	public enum CARatingAppsEntry
	{
		// Token: 0x040005BB RID: 1467
		[EnumMember]
		DontAllow,
		// Token: 0x040005BC RID: 1468
		[EnumMember]
		Rating4plus = 100,
		// Token: 0x040005BD RID: 1469
		[EnumMember]
		Rating9plus = 200,
		// Token: 0x040005BE RID: 1470
		[EnumMember]
		Rating12plus = 300,
		// Token: 0x040005BF RID: 1471
		[EnumMember]
		Rating17plus = 600,
		// Token: 0x040005C0 RID: 1472
		[EnumMember]
		AllowAll = 1000
	}
}
