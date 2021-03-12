using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C3 RID: 2243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationThreadDataExtractor
	{
		// Token: 0x06005343 RID: 21315 RVA: 0x0015AB90 File Offset: 0x00158D90
		internal ConversationThreadDataExtractor(ICollection<PropertyDefinition> propertiesToAggregate, IConversationTree conversationTree, bool isSingleThreadConversation)
		{
			this.conversationTree = conversationTree;
			this.isSingleThreadConversation = isSingleThreadConversation;
			this.propertiesToAggregate = propertiesToAggregate;
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0015ABB0 File Offset: 0x00158DB0
		public IStorePropertyBag ConstructThreadAggregatedProperties(IConversationTree threadTree)
		{
			PropertyAggregationContext context = new PropertyAggregationContext(threadTree.StorePropertyBags.ToList<IStorePropertyBag>());
			return ApplicationAggregatedProperty.Aggregate(context, this.propertiesToAggregate);
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0015ABDC File Offset: 0x00158DDC
		public IStorePropertyBag CalculateBackwardPropertyBag(StoreObjectId rootNodeId)
		{
			if (this.isSingleThreadConversation)
			{
				return null;
			}
			IConversationTreeNode equivalentNodeFromConversationTree = this.GetEquivalentNodeFromConversationTree(rootNodeId);
			if (equivalentNodeFromConversationTree != this.conversationTree.RootMessageNode)
			{
				return equivalentNodeFromConversationTree.ParentNode.MainPropertyBag;
			}
			return null;
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0015AC18 File Offset: 0x00158E18
		public Dictionary<StoreObjectId, List<IStorePropertyBag>> CalculateForwardMessagePropertyBags(ConversationId threadId, IEnumerable<StoreObjectId> threadNodesIds)
		{
			if (this.isSingleThreadConversation)
			{
				return null;
			}
			Dictionary<StoreObjectId, List<IStorePropertyBag>> dictionary = new Dictionary<StoreObjectId, List<IStorePropertyBag>>(threadNodesIds.Count<StoreObjectId>());
			foreach (StoreObjectId storeObjectId in threadNodesIds)
			{
				List<IStorePropertyBag> childNodesNotOnThreadTree = this.GetChildNodesNotOnThreadTree(threadId, storeObjectId);
				if (childNodesNotOnThreadTree.Count > 0)
				{
					dictionary.Add(storeObjectId, childNodesNotOnThreadTree);
				}
			}
			return dictionary;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0015AC8C File Offset: 0x00158E8C
		public void SyncConversationTreeNodesContent(IEnumerable<IConversationTreeNode> threadNodes)
		{
			foreach (IConversationTreeNode conversationTreeNode in threadNodes)
			{
				IConversationTreeNode equivalentNodeFromConversationTree = this.GetEquivalentNodeFromConversationTree(conversationTreeNode.MainStoreObjectId);
				foreach (StoreObjectId itemId in conversationTreeNode.ToListStoreObjectId())
				{
					IStorePropertyBag bag;
					if (equivalentNodeFromConversationTree.TryGetPropertyBag(itemId, out bag))
					{
						conversationTreeNode.UpdatePropertyBag(itemId, bag);
					}
				}
			}
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0015AD30 File Offset: 0x00158F30
		private IConversationTreeNode GetEquivalentNodeFromConversationTree(StoreObjectId itemId)
		{
			IConversationTreeNode result = null;
			if (!this.conversationTree.TryGetConversationTreeNode(itemId, out result))
			{
				LocalizedString localizedString = ServerStrings.ExItemNotFoundInConversation(itemId, this.conversationTree.RootMessageNode.ConversationId);
				ExTraceGlobals.ConversationTracer.TraceError((long)this.GetHashCode(), localizedString);
				throw new ObjectNotFoundException(localizedString);
			}
			return result;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0015AD88 File Offset: 0x00158F88
		private List<IStorePropertyBag> GetChildNodesNotOnThreadTree(ConversationId conversationThreadId, StoreObjectId itemId)
		{
			IConversationTreeNode equivalentNodeFromConversationTree = this.GetEquivalentNodeFromConversationTree(itemId);
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			foreach (IConversationTreeNode conversationTreeNode in equivalentNodeFromConversationTree.ChildNodes)
			{
				if (!conversationTreeNode.ConversationThreadId.Equals(conversationThreadId))
				{
					list.Add(conversationTreeNode.MainPropertyBag);
				}
			}
			return list;
		}

		// Token: 0x04002D4B RID: 11595
		private readonly bool isSingleThreadConversation;

		// Token: 0x04002D4C RID: 11596
		private readonly IConversationTree conversationTree;

		// Token: 0x04002D4D RID: 11597
		private readonly ICollection<PropertyDefinition> propertiesToAggregate;
	}
}
