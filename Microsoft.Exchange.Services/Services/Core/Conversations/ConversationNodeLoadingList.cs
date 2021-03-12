using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Conversations
{
	// Token: 0x020003A0 RID: 928
	internal sealed class ConversationNodeLoadingList
	{
		// Token: 0x06001A19 RID: 6681 RVA: 0x00096480 File Offset: 0x00094680
		public ConversationNodeLoadingList(IEnumerable<IConversationTreeNode> allNodes)
		{
			this.nodes = new Dictionary<IConversationTreeNode, ConversationNodeLoadingList.NodeStatus>(ConversationTreeNodeBase.EqualityComparer);
			foreach (IConversationTreeNode conversationTreeNode in allNodes)
			{
				if (this.nodes.ContainsKey(conversationTreeNode))
				{
					ExTraceGlobals.GetConversationItemsTracer.TraceDebug<ConversationId>((long)this.GetHashCode(), "ConversationNodeLoadingList.ctr: Duplicate node present in allNodes: ConversationId: {0}", conversationTreeNode.ConversationId);
				}
				this.nodes[conversationTreeNode] = ConversationNodeLoadingList.NodeStatus.NotToLoad;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00096528 File Offset: 0x00094728
		public IEnumerable<IConversationTreeNode> ToBeLoaded
		{
			get
			{
				return from pair in this.nodes
				where pair.Value == ConversationNodeLoadingList.NodeStatus.ToBeLoaded
				select pair.Key;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00096594 File Offset: 0x00094794
		public IEnumerable<IConversationTreeNode> NotToBeLoaded
		{
			get
			{
				return from pair in this.nodes
				where pair.Value == ConversationNodeLoadingList.NodeStatus.NotToLoad
				select pair.Key;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00096600 File Offset: 0x00094800
		public IEnumerable<IConversationTreeNode> ToBeIgnored
		{
			get
			{
				return from pair in this.nodes
				where pair.Value == ConversationNodeLoadingList.NodeStatus.ToBeIgnored
				select pair.Key;
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00096657 File Offset: 0x00094857
		public void MarkToBeLoaded(IConversationTreeNode treeNode)
		{
			this.MarkAs(treeNode, ConversationNodeLoadingList.NodeStatus.ToBeLoaded);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00096661 File Offset: 0x00094861
		public void MarkToBeIgnored(IConversationTreeNode treeNode)
		{
			this.MarkAs(treeNode, ConversationNodeLoadingList.NodeStatus.ToBeIgnored);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0009666B File Offset: 0x0009486B
		private void MarkAs(IConversationTreeNode treeNode, ConversationNodeLoadingList.NodeStatus status)
		{
			this.nodes[treeNode] = status;
		}

		// Token: 0x04001150 RID: 4432
		private readonly Dictionary<IConversationTreeNode, ConversationNodeLoadingList.NodeStatus> nodes;

		// Token: 0x020003A1 RID: 929
		private enum NodeStatus
		{
			// Token: 0x04001158 RID: 4440
			NotToLoad,
			// Token: 0x04001159 RID: 4441
			ToBeLoaded,
			// Token: 0x0400115A RID: 4442
			ToBeIgnored
		}
	}
}
