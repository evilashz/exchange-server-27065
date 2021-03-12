using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6B RID: 3947
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderBasedConversationClutterProcessor : IConversationClutterProcessor
	{
		// Token: 0x060086F7 RID: 34551 RVA: 0x002500DC File Offset: 0x0024E2DC
		public FolderBasedConversationClutterProcessor(MailboxSession mailboxSession)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x060086F8 RID: 34552 RVA: 0x002500F8 File Offset: 0x0024E2F8
		public int Process(bool markAsClutter, IConversationTree conversationTree, List<GroupOperationResult> results)
		{
			ArgumentValidator.ThrowIfNull("results", results);
			int num = 0;
			StoreObjectId storeObjectId = markAsClutter ? this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox) : this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter);
			StoreObjectId storeObjectId2 = markAsClutter ? this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter) : this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			if (storeObjectId == null || storeObjectId2 == null)
			{
				return num;
			}
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in conversationTree)
			{
				for (int i = 0; i < conversationTreeNode.StorePropertyBags.Count; i++)
				{
					StoreId id = conversationTreeNode.StorePropertyBags[i].TryGetProperty(ItemSchema.Id) as StoreId;
					if (storeObjectId.Equals(conversationTreeNode.StorePropertyBags[i].TryGetProperty(StoreObjectSchema.ParentItemId) as StoreObjectId))
					{
						list.Add(StoreId.GetStoreObjectId(id));
					}
				}
			}
			if (list.Count > 0)
			{
				try
				{
					using (Folder folder = Folder.Bind(this.mailboxSession, storeObjectId))
					{
						using (Folder folder2 = Folder.Bind(this.mailboxSession, storeObjectId2))
						{
							GroupOperationResult groupOperationResult = folder.MoveItems(folder2.Id, list.ToArray());
							results.Add(groupOperationResult);
							if (groupOperationResult.OperationResult != OperationResult.Failed)
							{
								num += groupOperationResult.ObjectIds.Count;
							}
						}
					}
				}
				catch (LocalizedException storageException)
				{
					results.Add(new GroupOperationResult(OperationResult.Failed, list, storageException));
				}
			}
			return num;
		}

		// Token: 0x04005A39 RID: 23097
		private readonly MailboxSession mailboxSession;
	}
}
