using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F2 RID: 498
	public enum RuleStatusType : byte
	{
		// Token: 0x04000A6F RID: 2671
		PreApproved,
		// Token: 0x04000A70 RID: 2672
		Approved,
		// Token: 0x04000A71 RID: 2673
		Publishing,
		// Token: 0x04000A72 RID: 2674
		Published,
		// Token: 0x04000A73 RID: 2675
		PublishingFailed,
		// Token: 0x04000A74 RID: 2676
		ValidationFailed,
		// Token: 0x04000A75 RID: 2677
		Submitted,
		// Token: 0x04000A76 RID: 2678
		PeerReviewFailed
	}
}
