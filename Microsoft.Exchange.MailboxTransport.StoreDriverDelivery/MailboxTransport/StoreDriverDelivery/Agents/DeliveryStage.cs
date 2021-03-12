using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000074 RID: 116
	internal enum DeliveryStage
	{
		// Token: 0x0400025B RID: 603
		None,
		// Token: 0x0400025C RID: 604
		InitializedMessageEventHandled,
		// Token: 0x0400025D RID: 605
		PromotedMessageEventHandled,
		// Token: 0x0400025E RID: 606
		CreatedMessageEventHandled,
		// Token: 0x0400025F RID: 607
		DeliveredMessageEventHandled,
		// Token: 0x04000260 RID: 608
		CompletedMessageEventHandled
	}
}
