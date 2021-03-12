using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D6 RID: 2262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationFamily : IBreadcrumbsSource, IConversation, ICoreConversation, IConversationData
	{
		// Token: 0x0600542D RID: 21549 RVA: 0x0015DC46 File Offset: 0x0015BE46
		internal ConversationFamily(IMailboxSession mailboxSession, ConversationDataExtractor dataExtractor, ConversationId conversationId, IConversationTree conversationTree, IConversationTreeFactory selectedConversationTreeFactory) : this(mailboxSession, dataExtractor, conversationId, conversationTree, conversationId, conversationTree, selectedConversationTreeFactory)
		{
			this.isSingleConversationFamily = true;
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0015DC5F File Offset: 0x0015BE5F
		internal ConversationFamily(IMailboxSession mailboxSession, ConversationDataExtractor dataExtractor, ConversationId conversationFamilyId, IConversationTree conversationFamilyTree, ConversationId selectedConversationId, IConversationTree selectedConversationTree, IConversationTreeFactory selectedConversationTreeFactory)
		{
			this.conversationFamilyTree = conversationFamilyTree;
			this.selectedConversationTree = selectedConversationTree;
			this.conversationFamilyId = conversationFamilyId;
			this.dataExtractor = dataExtractor;
			this.mailboxSession = mailboxSession;
			this.selectedConversationId = selectedConversationId;
			this.selectedConversationTreeFactory = selectedConversationTreeFactory;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600542F RID: 21551 RVA: 0x0015DC9C File Offset: 0x0015BE9C
		// (remove) Token: 0x06005430 RID: 21552 RVA: 0x0015DCAA File Offset: 0x0015BEAA
		public event OnBeforeItemLoadEventDelegate OnBeforeItemLoad
		{
			add
			{
				this.dataExtractor.OnBeforeItemLoad += value;
			}
			remove
			{
				this.dataExtractor.OnBeforeItemLoad -= value;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0015DCB8 File Offset: 0x0015BEB8
		public IStorePropertyBag BackwardMessagePropertyBag
		{
			get
			{
				if (this.backwardMessagePropertyBag == null)
				{
					this.backwardMessagePropertyBag = this.CalculateBackwardPropertyBag(this.selectedConversationTree.RootMessageNode);
				}
				return this.backwardMessagePropertyBag;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06005432 RID: 21554 RVA: 0x0015DCDF File Offset: 0x0015BEDF
		public Dictionary<StoreObjectId, List<IStorePropertyBag>> ForwardConversationRootMessagePropertyBags
		{
			get
			{
				if (this.forwardConversationRootMessagePropertyBags == null)
				{
					this.forwardConversationRootMessagePropertyBags = this.CalculateForwardMessagePropertyBags(this.selectedConversationTree);
				}
				return this.forwardConversationRootMessagePropertyBags;
			}
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0015DD01 File Offset: 0x0015BF01
		public ItemPart GetItemPart(StoreObjectId itemId)
		{
			return this.dataExtractor.GetItemPart(this.conversationFamilyTree, itemId);
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06005434 RID: 21556 RVA: 0x0015DD15 File Offset: 0x0015BF15
		public IConversationTree ConversationTree
		{
			get
			{
				return this.selectedConversationTree;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x0015DD1D File Offset: 0x0015BF1D
		public StoreObjectId RootMessageId
		{
			get
			{
				return this.ConversationTree.RootMessageId;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x0015DD2A File Offset: 0x0015BF2A
		public int ItemCount
		{
			get
			{
				return this.selectedConversationTree.Count;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x0015DD37 File Offset: 0x0015BF37
		public ConversationId ConversationId
		{
			get
			{
				return this.selectedConversationId;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06005438 RID: 21560 RVA: 0x0015DD3F File Offset: 0x0015BF3F
		public IConversationTree ConversationFamilyTree
		{
			get
			{
				return this.conversationFamilyTree;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x0015DD47 File Offset: 0x0015BF47
		public StoreObjectId ConversationFamilyRootMessageId
		{
			get
			{
				return this.conversationFamilyTree.RootMessageId;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x0015DD54 File Offset: 0x0015BF54
		public int ConversationFamilyItemCount
		{
			get
			{
				return this.conversationFamilyTree.Count;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0015DD61 File Offset: 0x0015BF61
		public ConversationId ConversationFamilyId
		{
			get
			{
				return this.conversationFamilyId;
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x0015DD69 File Offset: 0x0015BF69
		public string Topic
		{
			get
			{
				return this.ConversationTree.Topic;
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x0015DD76 File Offset: 0x0015BF76
		public EffectiveRights EffectiveRights
		{
			get
			{
				return this.ConversationTree.EffectiveRights;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x0600543E RID: 21566 RVA: 0x0015DD83 File Offset: 0x0015BF83
		public byte[] SerializedTreeState
		{
			get
			{
				return this.ConversationState.SerializedState;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x0015DD90 File Offset: 0x0015BF90
		public IConversationStatistics ConversationStatistics
		{
			get
			{
				return this.dataExtractor.OptimizationCounters;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x0015DD9D File Offset: 0x0015BF9D
		protected bool IsSingleConversationFamily
		{
			get
			{
				return this.isSingleConversationFamily;
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x0015DDA5 File Offset: 0x0015BFA5
		private ConversationState ConversationState
		{
			get
			{
				if (this.conversationState == null)
				{
					this.conversationState = new ConversationState(this.mailboxSession, this.selectedConversationTree, null);
				}
				return this.conversationState;
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0015DDD0 File Offset: 0x0015BFD0
		public byte[] GetSerializedTreeStateWithNodesToExclude(ICollection<IConversationTreeNode> nodesToExclude)
		{
			ConversationState conversationState = new ConversationState(this.mailboxSession, this.selectedConversationTree, nodesToExclude);
			return conversationState.SerializedState;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0015DDF6 File Offset: 0x0015BFF6
		public void LoadBodySummaries()
		{
			this.dataExtractor.LoadBodySummaries(this.conversationFamilyTree);
			this.SyncConversationTrees();
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0015DE17 File Offset: 0x0015C017
		public void LoadItemParts(ICollection<IConversationTreeNode> nodes)
		{
			this.LoadItemParts(from n in nodes
			select n.MainStoreObjectId);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0015DE42 File Offset: 0x0015C042
		public ParticipantSet AllParticipants(ICollection<IConversationTreeNode> loadedNodes = null)
		{
			return this.dataExtractor.CalculateAllRecipients(this.ConversationTree, loadedNodes);
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0015DE56 File Offset: 0x0015C056
		public void LoadItemParts(ICollection<StoreObjectId> storeIds)
		{
			this.dataExtractor.LoadItemParts(this.conversationFamilyTree, storeIds);
			this.SyncConversationTrees();
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0015DE70 File Offset: 0x0015C070
		public void TrimToNewest(int count)
		{
			if (this.dataExtractor.ConversationDataLoaded)
			{
				throw new InvalidOperationException("LoadItemParts or LoadBodySummary already called");
			}
			this.selectedConversationTree = this.selectedConversationTreeFactory.GetNewestSubTree(this.selectedConversationTree, count);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0015DEA2 File Offset: 0x0015C0A2
		public KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> CalculateChanges(byte[] olderState)
		{
			return this.ConversationState.CalculateChanges(olderState);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0015DEB0 File Offset: 0x0015C0B0
		public ParticipantTable LoadReplyAllParticipantsPerType()
		{
			return this.dataExtractor.LoadReplyAllParticipantsPerType(this.selectedConversationTree);
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0015DEC3 File Offset: 0x0015C0C3
		public ParticipantSet LoadReplyAllParticipants(IConversationTreeNode node)
		{
			return this.dataExtractor.LoadReplyAllParticipants(this.selectedConversationTree, node);
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x0600544B RID: 21579 RVA: 0x0015DED7 File Offset: 0x0015C0D7
		public IConversationTreeNode RootMessageNode
		{
			get
			{
				return this.selectedConversationTree.RootMessageNode;
			}
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0015DEE4 File Offset: 0x0015C0E4
		public int GetNodeCount(bool includeSubmitted)
		{
			return this.selectedConversationTree.GetNodeCount(includeSubmitted);
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x0015DEF2 File Offset: 0x0015C0F2
		public IConversationTreeNode FirstNode
		{
			get
			{
				return this.RootMessageNode;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x0015DEFA File Offset: 0x0015C0FA
		public byte[] ConversationCreatorSID
		{
			get
			{
				return this.selectedConversationTree.ConversationCreatorSID;
			}
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0015DF07 File Offset: 0x0015C107
		public Dictionary<IConversationTreeNode, ParticipantSet> LoadAddedParticipants()
		{
			return this.dataExtractor.LoadAddedParticipants(this.selectedConversationTree);
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0015DF1A File Offset: 0x0015C11A
		public List<StoreObjectId> GetMessageIdsForPreread()
		{
			return this.dataExtractor.GetMessageIdsForPreread(this.selectedConversationTree);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0015DF30 File Offset: 0x0015C130
		public IStorePropertyBag GetItemPropertyBag(StoreObjectId itemId)
		{
			List<IStorePropertyBag> itemPropertyBags = this.GetItemPropertyBags(itemId);
			if (itemPropertyBags.Count <= 0)
			{
				return null;
			}
			return itemPropertyBags[0];
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0015DF58 File Offset: 0x0015C158
		public List<IStorePropertyBag> GetItemPropertyBags(StoreObjectId itemId)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			IConversationTreeNode conversationTreeNode;
			if (this.conversationFamilyTree.TryGetConversationTreeNode(itemId, out conversationTreeNode))
			{
				foreach (StoreObjectId itemId2 in conversationTreeNode.ToListStoreObjectId())
				{
					IStorePropertyBag item;
					if (conversationTreeNode.TryGetPropertyBag(itemId2, out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0015DFD0 File Offset: 0x0015C1D0
		public StoreObjectId GetParentId(StoreObjectId itemId, bool allowCrossConversation)
		{
			IConversationTree conversationTree = allowCrossConversation ? this.conversationFamilyTree : this.selectedConversationTree;
			if (itemId == conversationTree.RootMessageId)
			{
				return null;
			}
			IConversationTreeNode conversationTreeNode;
			if (conversationTree.TryGetConversationTreeNode(itemId, out conversationTreeNode) && conversationTreeNode.ParentNode != null && conversationTreeNode.ParentNode.HasData)
			{
				return conversationTreeNode.ParentNode.MainStoreObjectId;
			}
			return null;
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0015E027 File Offset: 0x0015C227
		public IEnumerable<StoreObjectId> EnumerateAncestorsOfSelectedConversation()
		{
			return this.EnumerateBackwardInFamilyFromNode(this.selectedConversationTree.RootMessageNode);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0015E03A File Offset: 0x0015C23A
		public Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph()
		{
			return this.selectedConversationTree.BuildPreviousNodeGraph();
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0015E047 File Offset: 0x0015C247
		public IConversationTree GetNewestSubTree(int count)
		{
			return this.selectedConversationTreeFactory.GetNewestSubTree(this.selectedConversationTree, count);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0015E1B4 File Offset: 0x0015C3B4
		private IEnumerable<StoreObjectId> EnumerateBackwardInFamilyFromNode(IConversationTreeNode startNode)
		{
			if (startNode != null)
			{
				IConversationTreeNode nodeOnFamilyTree = this.GetEquivalentNodeFromFamilyTree(startNode);
				if (nodeOnFamilyTree != null && nodeOnFamilyTree != this.conversationFamilyTree.RootMessageNode)
				{
					IConversationTreeNode ancestor = nodeOnFamilyTree.ParentNode;
					while (ancestor != null && ancestor.HasData)
					{
						yield return ancestor.MainStoreObjectId;
						ancestor = ancestor.ParentNode;
					}
				}
			}
			yield break;
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0015E1D8 File Offset: 0x0015C3D8
		private IStorePropertyBag CalculateBackwardPropertyBag(IConversationTreeNode rootNodeFromSelectedConversation)
		{
			if (this.IsSingleConversationFamily)
			{
				return null;
			}
			if (rootNodeFromSelectedConversation == null)
			{
				return null;
			}
			IConversationTreeNode equivalentNodeFromFamilyTree = this.GetEquivalentNodeFromFamilyTree(rootNodeFromSelectedConversation);
			if (equivalentNodeFromFamilyTree != this.conversationFamilyTree.RootMessageNode)
			{
				return equivalentNodeFromFamilyTree.ParentNode.MainPropertyBag;
			}
			return null;
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0015E218 File Offset: 0x0015C418
		private Dictionary<StoreObjectId, List<IStorePropertyBag>> CalculateForwardMessagePropertyBags(IConversationTree selectedConversationTree)
		{
			if (this.IsSingleConversationFamily)
			{
				return null;
			}
			Dictionary<StoreObjectId, List<IStorePropertyBag>> dictionary = new Dictionary<StoreObjectId, List<IStorePropertyBag>>(selectedConversationTree.Count);
			ConversationId conversationId = this.ConversationId;
			foreach (IConversationTreeNode conversationTreeNode in selectedConversationTree)
			{
				IConversationTreeNode equivalentNodeFromFamilyTree = this.GetEquivalentNodeFromFamilyTree(conversationTreeNode);
				List<IStorePropertyBag> childNodesNotOnSelectedConversation = this.GetChildNodesNotOnSelectedConversation(conversationId, equivalentNodeFromFamilyTree);
				if (childNodesNotOnSelectedConversation.Count > 0)
				{
					dictionary.Add(conversationTreeNode.MainStoreObjectId, childNodesNotOnSelectedConversation);
				}
			}
			return dictionary;
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0015E2A8 File Offset: 0x0015C4A8
		private IConversationTreeNode GetEquivalentNodeFromFamilyTree(IConversationTreeNode nodeFromSelectedConversation)
		{
			IConversationTreeNode result = null;
			if (nodeFromSelectedConversation == null || !this.conversationFamilyTree.TryGetConversationTreeNode(nodeFromSelectedConversation.MainStoreObjectId, out result))
			{
				LocalizedString localizedString = ServerStrings.ExItemNotFoundInConversation((nodeFromSelectedConversation != null) ? nodeFromSelectedConversation.MainStoreObjectId : null, this.ConversationFamilyId);
				ExTraceGlobals.ConversationTracer.TraceError((long)this.GetHashCode(), localizedString);
				throw new ObjectNotFoundException(localizedString);
			}
			return result;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0015E308 File Offset: 0x0015C508
		private void SyncConversationTrees()
		{
			if (this.IsSingleConversationFamily)
			{
				return;
			}
			this.backwardMessagePropertyBag = null;
			this.forwardConversationRootMessagePropertyBags = null;
			foreach (IConversationTreeNode conversationTreeNode in this.selectedConversationTree)
			{
				IConversationTreeNode equivalentNodeFromFamilyTree = this.GetEquivalentNodeFromFamilyTree(conversationTreeNode);
				foreach (StoreObjectId itemId in conversationTreeNode.ToListStoreObjectId())
				{
					IStorePropertyBag bag;
					if (equivalentNodeFromFamilyTree.TryGetPropertyBag(itemId, out bag))
					{
						conversationTreeNode.UpdatePropertyBag(itemId, bag);
					}
				}
			}
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0015E3C4 File Offset: 0x0015C5C4
		private List<IStorePropertyBag> GetChildNodesNotOnSelectedConversation(ConversationId selectedConversationId, IConversationTreeNode node)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			foreach (IConversationTreeNode conversationTreeNode in node.ChildNodes)
			{
				if (conversationTreeNode.ConversationId != null && !conversationTreeNode.ConversationId.Equals(selectedConversationId))
				{
					list.Add(conversationTreeNode.MainPropertyBag);
				}
			}
			return list;
		}

		// Token: 0x04002D81 RID: 11649
		private readonly IConversationTree conversationFamilyTree;

		// Token: 0x04002D82 RID: 11650
		private readonly ConversationDataExtractor dataExtractor;

		// Token: 0x04002D83 RID: 11651
		private readonly IMailboxSession mailboxSession;

		// Token: 0x04002D84 RID: 11652
		private readonly ConversationId conversationFamilyId;

		// Token: 0x04002D85 RID: 11653
		private readonly bool isSingleConversationFamily;

		// Token: 0x04002D86 RID: 11654
		private readonly IConversationTreeFactory selectedConversationTreeFactory;

		// Token: 0x04002D87 RID: 11655
		private IStorePropertyBag backwardMessagePropertyBag;

		// Token: 0x04002D88 RID: 11656
		private ConversationState conversationState;

		// Token: 0x04002D89 RID: 11657
		private Dictionary<StoreObjectId, List<IStorePropertyBag>> forwardConversationRootMessagePropertyBags;

		// Token: 0x04002D8A RID: 11658
		private IConversationTree selectedConversationTree;

		// Token: 0x04002D8B RID: 11659
		private ConversationId selectedConversationId;
	}
}
