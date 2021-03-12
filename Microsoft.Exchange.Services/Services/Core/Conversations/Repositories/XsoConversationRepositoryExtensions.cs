using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Conversations.Repositories
{
	// Token: 0x020003A8 RID: 936
	internal static class XsoConversationRepositoryExtensions
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x00097194 File Offset: 0x00095394
		public static List<IConversationTreeNode> GetTreeNodes(ICoreConversation conversation, byte[] syncState)
		{
			return XsoConversationRepositoryExtensions.CalculateTreeNodes(conversation, conversation.ConversationTree, syncState);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000971A4 File Offset: 0x000953A4
		public static List<StoreObjectId> GetListStoreObjectIds(ICollection<IConversationTreeNode> treeNodes)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in treeNodes)
			{
				list.AddRange(conversationTreeNode.ToListStoreObjectId());
			}
			return list;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000971F8 File Offset: 0x000953F8
		public static bool IsValidSyncState(byte[] syncState)
		{
			return syncState != null && syncState.Length > 0;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00097208 File Offset: 0x00095408
		public static IdAndSession GetSessionFromConversationId(IdConverter idConverter, BaseItemId conversationId, MailboxSearchLocation mailboxSearchLocation)
		{
			IdAndSession idAndSession = idConverter.ConvertConversationIdToIdAndSession(conversationId, mailboxSearchLocation == MailboxSearchLocation.ArchiveOnly);
			if (!(idAndSession.Session is MailboxSession))
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ConversationSupportedOnlyForMailboxSession);
			}
			return idAndSession;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00097244 File Offset: 0x00095444
		private static List<IConversationTreeNode> CalculateTreeNodes(ICoreConversation conversation, IConversationTree conversationTreeBase, byte[] syncState)
		{
			if (XsoConversationRepositoryExtensions.IsValidSyncState(syncState))
			{
				List<StoreObjectId> syncStateIds = XsoConversationRepositoryExtensions.GetSyncStateIds(conversation, syncState);
				return XsoConversationRepositoryExtensions.GetConversationTreeNodes(conversationTreeBase, syncStateIds);
			}
			return new List<IConversationTreeNode>(conversationTreeBase.ToList<IConversationTreeNode>());
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00097274 File Offset: 0x00095474
		private static List<StoreObjectId> GetSyncStateIds(ICoreConversation conversation, byte[] syncState)
		{
			KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> keyValuePair = conversation.CalculateChanges(syncState);
			List<StoreObjectId> list = new List<StoreObjectId>();
			list.AddRange(keyValuePair.Key);
			list.AddRange(keyValuePair.Value);
			return list;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000972AC File Offset: 0x000954AC
		private static List<IConversationTreeNode> GetConversationTreeNodes(IConversationTree conversationTree, List<StoreObjectId> itemsToKeep)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>();
			foreach (StoreObjectId storeObjectId in itemsToKeep)
			{
				IConversationTreeNode item;
				if (conversationTree.TryGetConversationTreeNode(storeObjectId, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
}
