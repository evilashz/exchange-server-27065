using System;

namespace Microsoft.Exchange.Services.Core.Types.Conversations
{
	// Token: 0x0200068B RID: 1675
	internal interface IConversationDataResponse
	{
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003334 RID: 13108
		// (set) Token: 0x06003335 RID: 13109
		EmailAddressWrapper[] ToRecipients { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003336 RID: 13110
		// (set) Token: 0x06003337 RID: 13111
		EmailAddressWrapper[] CcRecipients { get; set; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003338 RID: 13112
		// (set) Token: 0x06003339 RID: 13113
		ConversationNode[] ConversationNodes { get; set; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600333A RID: 13114
		// (set) Token: 0x0600333B RID: 13115
		int TotalConversationNodesCount { get; set; }
	}
}
