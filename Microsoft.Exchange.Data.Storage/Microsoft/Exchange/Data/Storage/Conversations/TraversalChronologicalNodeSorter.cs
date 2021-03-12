using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008DA RID: 2266
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TraversalChronologicalNodeSorter : IConversationTreeNodeSorter
	{
		// Token: 0x060054A0 RID: 21664 RVA: 0x0015EB7C File Offset: 0x0015CD7C
		private void AddNodeAndUnrelatedChildrenRecursively(PriorityQueue<IConversationTreeNode> candidates, IConversationTreeNode targetNode)
		{
			candidates.Enqueue(targetNode);
			foreach (IConversationTreeNode conversationTreeNode in targetNode.ChildNodes)
			{
				if (targetNode.GetRelationTo(conversationTreeNode) == ConversationTreeNodeRelation.Unrelated)
				{
					this.AddNodeAndUnrelatedChildrenRecursively(candidates, conversationTreeNode);
				}
			}
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0015EBDC File Offset: 0x0015CDDC
		public List<IConversationTreeNode> Sort(IConversationTreeNode rootNode, ConversationTreeSortOrder sortOrder)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>();
			PriorityQueue<IConversationTreeNode> priorityQueue = new PriorityQueue<IConversationTreeNode>();
			this.AddNodeAndUnrelatedChildrenRecursively(priorityQueue, rootNode);
			while (priorityQueue.Count() > 0)
			{
				IConversationTreeNode conversationTreeNode = priorityQueue.Dequeue();
				if (conversationTreeNode.HasData)
				{
					conversationTreeNode.SortOrder = sortOrder;
					list.Add(conversationTreeNode);
				}
				foreach (IConversationTreeNode conversationTreeNode2 in conversationTreeNode.ChildNodes)
				{
					if (conversationTreeNode.GetRelationTo(conversationTreeNode2) != ConversationTreeNodeRelation.Unrelated)
					{
						this.AddNodeAndUnrelatedChildrenRecursively(priorityQueue, conversationTreeNode2);
					}
				}
			}
			if (sortOrder == ConversationTreeSortOrder.TraversalChronologicalDescending)
			{
				list.Reverse();
			}
			return list;
		}

		// Token: 0x04002DA3 RID: 11683
		public static readonly IConversationTreeNodeSorter Instance = new TraversalChronologicalNodeSorter();
	}
}
