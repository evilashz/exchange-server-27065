using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B1 RID: 945
	internal class PopulateInReplyTo : IConversationNodeProcessor
	{
		// Token: 0x06001A9A RID: 6810 RVA: 0x00098404 File Offset: 0x00096604
		public PopulateInReplyTo(IModernConversationNodeFactory conversationNodeFactory, Dictionary<IConversationTreeNode, IConversationTreeNode> previousNodeMap, Dictionary<IConversationTreeNode, ConversationNode> serviceNodesMap)
		{
			this.conversationNodeFactory = conversationNodeFactory;
			this.previousNodeMap = previousNodeMap;
			this.serviceNodesMap = serviceNodesMap;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00098424 File Offset: 0x00096624
		public void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode)
		{
			if (this.ShouldGenerateInReplyToData(node))
			{
				IRelatedItemInfo relatedItemInfo = null;
				if (!this.TryLoadReplyToInformation(node.ParentNode, out relatedItemInfo))
				{
					this.conversationNodeFactory.TryLoadRelatedItemInfo(node.ParentNode, out relatedItemInfo);
				}
				if (relatedItemInfo != null)
				{
					serviceNode.InReplyToItem = InReplyToAdapterType.FromRelatedItemInfo(relatedItemInfo);
				}
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00098470 File Offset: 0x00096670
		private bool ShouldGenerateInReplyToData(IConversationTreeNode node)
		{
			IConversationTreeNode y;
			if (!this.previousNodeMap.TryGetValue(node, out y))
			{
				return false;
			}
			if (node.IsSpecificMessageReplyStamped)
			{
				return node.IsSpecificMessageReply;
			}
			return !ConversationTreeNodeBase.EqualityComparer.Equals(node.ParentNode, y);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000984B4 File Offset: 0x000966B4
		private bool TryLoadReplyToInformation(IConversationTreeNode parentNode, out IRelatedItemInfo relatedItemInfo)
		{
			relatedItemInfo = null;
			ConversationNode conversationNode;
			if (parentNode.HasData && this.serviceNodesMap.TryGetValue(parentNode, out conversationNode))
			{
				if (conversationNode.ItemCount > 0 && !conversationNode.Items[0].ContainsOnlyMandatoryProperties && conversationNode.Items[0] is IRelatedItemInfo)
				{
					relatedItemInfo = (conversationNode.Items[0] as IRelatedItemInfo);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001186 RID: 4486
		private readonly IModernConversationNodeFactory conversationNodeFactory;

		// Token: 0x04001187 RID: 4487
		private readonly Dictionary<IConversationTreeNode, IConversationTreeNode> previousNodeMap;

		// Token: 0x04001188 RID: 4488
		private readonly Dictionary<IConversationTreeNode, ConversationNode> serviceNodesMap;
	}
}
