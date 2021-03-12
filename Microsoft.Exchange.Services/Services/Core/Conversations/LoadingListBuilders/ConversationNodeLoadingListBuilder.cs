using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders
{
	// Token: 0x020003A4 RID: 932
	internal class ConversationNodeLoadingListBuilder : ConversationNodeLoadingListBuilderBase
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x00096744 File Offset: 0x00094944
		public ConversationNodeLoadingListBuilder(IEnumerable<IConversationTreeNode> allNodes, IConversationTreeNode rootTreeNode, ConversationRequestArguments requestArguments) : base(allNodes)
		{
			this.rootTreeNode = rootTreeNode;
			this.requestArguments = requestArguments;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0009675C File Offset: 0x0009495C
		protected override void MarkDependentNodesToBeLoaded()
		{
			List<IConversationTreeNode> list = base.LoadingList.NotToBeLoaded.ToList<IConversationTreeNode>();
			List<IConversationTreeNode> newestConversationNodesFrom = this.GetNewestConversationNodesFrom(list);
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int, int>((long)this.GetHashCode(), "ConversationNodeLoadingListBuilder.MarkDependentNodesToBeLoaded: Unloaded nodes: {0}, nodes to load: {1}", list.Count, newestConversationNodesFrom.Count);
			if (this.rootTreeNode != null && this.rootTreeNode.HasData && list.Contains(this.rootTreeNode))
			{
				ExTraceGlobals.GetConversationItemsTracer.TraceDebug((long)this.GetHashCode(), "ConversationNodeLoadingListBuilder.MarkDependentNodesToBeLoaded: Adding root node to loading list");
				newestConversationNodesFrom.Add(this.rootTreeNode);
			}
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int>((long)this.GetHashCode(), "ConversationNodeLoadingListBuilder.MarkDependentNodesToBeLoaded: Marking {0} nodes to be loaded", newestConversationNodesFrom.Count);
			foreach (IConversationTreeNode treeNode in newestConversationNodesFrom)
			{
				base.LoadingList.MarkToBeLoaded(treeNode);
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0009684C File Offset: 0x00094A4C
		protected override void MarkNodesToBeIgnored()
		{
			IConversationTreeNode[] array = base.LoadingList.ToBeLoaded.ToArray<IConversationTreeNode>();
			foreach (IConversationTreeNode conversationTreeNode in array)
			{
				if (base.ShouldIgnore(conversationTreeNode, this.requestArguments.ReturnSubmittedItems))
				{
					base.LoadingList.MarkToBeIgnored(conversationTreeNode);
				}
			}
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int>((long)this.GetHashCode(), "ConversationNodeLoadingListBuilder.MarkNodesToBeIgnored: Marking {0} nodes to be ignored", base.LoadingList.ToBeIgnored.Count<IConversationTreeNode>());
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000968C4 File Offset: 0x00094AC4
		protected override void MarkNodesForProcessorsToBeLoaded()
		{
			this.MarkInReplyToNodesToBeLoaded();
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000968CC File Offset: 0x00094ACC
		protected virtual List<IConversationTreeNode> GetNewestConversationNodesFrom(List<IConversationTreeNode> unloadedNodes)
		{
			return ConversationTreeNodeBase.TrimToNewest(unloadedNodes, this.requestArguments.MaxItemsToReturn);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00096900 File Offset: 0x00094B00
		private void MarkInReplyToNodesToBeLoaded()
		{
			IConversationTreeNode[] array = (from node in base.LoadingList.ToBeLoaded
			where node.ParentNode != null && node.ParentNode.HasData
			select node.ParentNode).ToArray<IConversationTreeNode>();
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int>((long)this.GetHashCode(), "ConversationNodeLoadingListBuilder.MarkInReplyToNodesToBeLoaded: Adding in-reply-to nodes to loading list. Number of in-reply-to nodes: {0}", array.Length);
			foreach (IConversationTreeNode treeNode in array)
			{
				base.LoadingList.MarkToBeLoaded(treeNode);
			}
		}

		// Token: 0x0400115C RID: 4444
		private readonly IConversationTreeNode rootTreeNode;

		// Token: 0x0400115D RID: 4445
		private readonly ConversationRequestArguments requestArguments;
	}
}
