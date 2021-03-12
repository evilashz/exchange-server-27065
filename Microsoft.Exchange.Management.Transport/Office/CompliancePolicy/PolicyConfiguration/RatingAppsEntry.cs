using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000149 RID: 329
	[DataContract]
	public enum RatingAppsEntry
	{
		// Token: 0x0400053F RID: 1343
		[EnumMember]
		DontAllow,
		// Token: 0x04000540 RID: 1344
		[EnumMember]
		Rating4plus = 100,
		// Token: 0x04000541 RID: 1345
		[EnumMember]
		Rating9plus = 200,
		// Token: 0x04000542 RID: 1346
		[EnumMember]
		Rating12plus = 300,
		// Token: 0x04000543 RID: 1347
		[EnumMember]
		Rating17plus = 600,
		// Token: 0x04000544 RID: 1348
		[EnumMember]
		AllowAll = 1000
	}
}
