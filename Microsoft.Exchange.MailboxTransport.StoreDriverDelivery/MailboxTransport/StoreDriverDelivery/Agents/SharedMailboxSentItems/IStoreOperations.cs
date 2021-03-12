using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B4 RID: 180
	internal interface IStoreOperations
	{
		// Token: 0x060005BE RID: 1470
		bool MessageExistsInSentItems(string internetMessageId);

		// Token: 0x060005BF RID: 1471
		void CopyAttachmentToSentItemsFolder(MessageItem attachedMessageItem);
	}
}
