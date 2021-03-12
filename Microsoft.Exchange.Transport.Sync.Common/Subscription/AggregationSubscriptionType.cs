using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B4 RID: 180
	[Flags]
	[Serializable]
	public enum AggregationSubscriptionType
	{
		// Token: 0x040002CD RID: 717
		Unknown = 0,
		// Token: 0x040002CE RID: 718
		Pop = 2,
		// Token: 0x040002CF RID: 719
		DeltaSyncMail = 4,
		// Token: 0x040002D0 RID: 720
		IMAP = 16,
		// Token: 0x040002D1 RID: 721
		AllEMail = 22,
		// Token: 0x040002D2 RID: 722
		Facebook = 32,
		// Token: 0x040002D3 RID: 723
		LinkedIn = 64,
		// Token: 0x040002D4 RID: 724
		AllThatSupportSendAs = 22,
		// Token: 0x040002D5 RID: 725
		AllThatSupportPolicyInducedDeletion = 96,
		// Token: 0x040002D6 RID: 726
		AllThatSupportSendAsAndPeopleConnect = 118,
		// Token: 0x040002D7 RID: 727
		All = 255
	}
}
