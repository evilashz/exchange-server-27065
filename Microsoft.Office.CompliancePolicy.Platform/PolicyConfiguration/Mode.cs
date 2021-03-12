using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000085 RID: 133
	[DataContract]
	public enum Mode
	{
		// Token: 0x0400023C RID: 572
		[EnumMember]
		Enforce,
		// Token: 0x0400023D RID: 573
		[EnumMember]
		Audit,
		// Token: 0x0400023E RID: 574
		[EnumMember]
		PendingDeletion,
		// Token: 0x0400023F RID: 575
		[EnumMember]
		Deleted
	}
}
