using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000088 RID: 136
	[DataContract]
	public enum ChangeType
	{
		// Token: 0x0400024A RID: 586
		[EnumMember]
		None,
		// Token: 0x0400024B RID: 587
		[EnumMember]
		Update,
		// Token: 0x0400024C RID: 588
		[EnumMember]
		Delete,
		// Token: 0x0400024D RID: 589
		[EnumMember]
		Add
	}
}
