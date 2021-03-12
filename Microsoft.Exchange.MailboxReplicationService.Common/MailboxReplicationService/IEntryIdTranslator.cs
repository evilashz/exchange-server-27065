using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012F RID: 303
	internal interface IEntryIdTranslator
	{
		// Token: 0x06000A5D RID: 2653
		byte[] GetSourceFolderIdFromTargetFolderId(byte[] targetFolderId);

		// Token: 0x06000A5E RID: 2654
		byte[] GetSourceMessageIdFromTargetMessageId(byte[] targetMessageId);
	}
}
