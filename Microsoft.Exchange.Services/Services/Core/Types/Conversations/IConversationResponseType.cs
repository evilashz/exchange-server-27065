using System;

namespace Microsoft.Exchange.Services.Core.Types.Conversations
{
	// Token: 0x0200068C RID: 1676
	public interface IConversationResponseType
	{
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600333C RID: 13116
		// (set) Token: 0x0600333D RID: 13117
		ItemId ConversationId { get; set; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600333E RID: 13118
		// (set) Token: 0x0600333F RID: 13119
		byte[] SyncState { get; set; }
	}
}
