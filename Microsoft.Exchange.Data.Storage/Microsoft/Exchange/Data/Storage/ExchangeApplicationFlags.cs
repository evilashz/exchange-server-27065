using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200025B RID: 603
	[Flags]
	internal enum ExchangeApplicationFlags
	{
		// Token: 0x04001201 RID: 4609
		IsFromFavoriteSender = 1,
		// Token: 0x04001202 RID: 4610
		IsSpecificMessageReply = 2,
		// Token: 0x04001203 RID: 4611
		IsSpecificMessageReplyStamped = 4,
		// Token: 0x04001204 RID: 4612
		RelyOnConversationIndex = 8,
		// Token: 0x04001205 RID: 4613
		IsClutterOverridden = 16,
		// Token: 0x04001206 RID: 4614
		SupportsSideConversation = 32,
		// Token: 0x04001207 RID: 4615
		IsGroupEscalationMessage = 64,
		// Token: 0x04001208 RID: 4616
		FromAddressBookContact = 128,
		// Token: 0x04001209 RID: 4617
		JunkedByBlockListMessageFilter = 256,
		// Token: 0x0400120A RID: 4618
		SenderPRAEmailPresent = 512,
		// Token: 0x0400120B RID: 4619
		FromFirstTimeSender = 1024,
		// Token: 0x0400120C RID: 4620
		IsFromPerson = 2048
	}
}
