using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupJoinRequestMessage
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		internal static void SendMessage(MailboxSession mailboxSession, ADUser groupAdUser, string attachedMessageBody)
		{
			ArgumentValidator.ThrowIfNull("groupMailbox", mailboxSession);
			ArgumentValidator.ThrowIfNull("groupAdUser", groupAdUser);
			ArgumentValidator.ThrowIfNull("attachedMessageBody", attachedMessageBody);
			StoreObjectId storeObjectId = mailboxSession.GetDefaultFolderId(DefaultFolderType.TemporarySaves);
			if (storeObjectId == null)
			{
				storeObjectId = mailboxSession.CreateDefaultFolder(DefaultFolderType.TemporarySaves);
			}
			using (GroupMailboxJoinRequestMessageItem groupMailboxJoinRequestMessageItem = GroupMailboxJoinRequestMessageItem.Create(mailboxSession, storeObjectId))
			{
				new GroupJoinRequestMessageComposer(mailboxSession, groupAdUser, attachedMessageBody).WriteToMessage(groupMailboxJoinRequestMessageItem);
				groupMailboxJoinRequestMessageItem.Send();
			}
		}
	}
}
