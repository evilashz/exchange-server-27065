using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200008B RID: 139
	public enum UnifiedPolicyErrorCode
	{
		// Token: 0x04000256 RID: 598
		[EnumMember]
		Unknown = -1,
		// Token: 0x04000257 RID: 599
		[EnumMember]
		Success,
		// Token: 0x04000258 RID: 600
		[EnumMember]
		InternalError,
		// Token: 0x04000259 RID: 601
		[EnumMember]
		FailedToOpenContainer = 16777217,
		// Token: 0x0400025A RID: 602
		[EnumMember]
		SiteInReadonlyOrNotAccessible,
		// Token: 0x0400025B RID: 603
		[EnumMember]
		SiteOutOfQuota,
		// Token: 0x0400025C RID: 604
		[EnumMember]
		PolicyNotifyError = 16777223,
		// Token: 0x0400025D RID: 605
		[EnumMember]
		PolicySyncTimeout
	}
}
