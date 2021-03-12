using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001F1 RID: 497
	public enum RequestedAction
	{
		// Token: 0x04000DDD RID: 3549
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionAny)]
		Any,
		// Token: 0x04000DDE RID: 3550
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionCall)]
		Call,
		// Token: 0x04000DDF RID: 3551
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionDoNotForward)]
		DoNotForward,
		// Token: 0x04000DE0 RID: 3552
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionFollowUp)]
		FollowUp,
		// Token: 0x04000DE1 RID: 3553
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionForYourInformation)]
		ForYourInformation,
		// Token: 0x04000DE2 RID: 3554
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionForward)]
		Forward,
		// Token: 0x04000DE3 RID: 3555
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionNoResponseNecessary)]
		NoResponseNecessary,
		// Token: 0x04000DE4 RID: 3556
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionRead)]
		Read,
		// Token: 0x04000DE5 RID: 3557
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionReply)]
		Reply,
		// Token: 0x04000DE6 RID: 3558
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionReplyToAll)]
		ReplyToAll,
		// Token: 0x04000DE7 RID: 3559
		[ClientStringsLocDescription(ClientStrings.IDs.RequestedActionReview)]
		Review
	}
}
