using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D9 RID: 2265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThreadedConversation : IThreadedConversation, ICoreConversation
	{
		// Token: 0x0600548A RID: 21642 RVA: 0x0015E999 File Offset: 0x0015CB99
		internal ThreadedConversation(ConversationStateFactory stateFactory, ConversationDataExtractor dataExtractor, ConversationId conversationId, IConversationTree conversationTree, IList<IConversationThread> conversationThreads)
		{
			this.conversationTree = conversationTree;
			this.conversationId = conversationId;
			this.dataExtractor = dataExtractor;
			this.conversationThreads = conversationThreads;
			this.stateFactory = stateFactory;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600548B RID: 21643 RVA: 0x0015E9C6 File Offset: 0x0015CBC6
		// (remove) Token: 0x0600548C RID: 21644 RVA: 0x0015E9D4 File Offset: 0x0015CBD4
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

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x0015E9E2 File Offset: 0x0015CBE2
		public ConversationId ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x0600548E RID: 21646 RVA: 0x0015E9EA File Offset: 0x0015CBEA
		public IConversationTreeNode RootMessageNode
		{
			get
			{
				return this.conversationTree.RootMessageNode;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x0015E9F7 File Offset: 0x0015CBF7
		public IConversationTree ConversationTree
		{
			get
			{
				return this.conversationTree;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x0015E9FF File Offset: 0x0015CBFF
		public StoreObjectId RootMessageId
		{
			get
			{
				return this.conversationTree.RootMessageId;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06005491 RID: 21649 RVA: 0x0015EA0C File Offset: 0x0015CC0C
		public string Topic
		{
			get
			{
				return this.ConversationTree.Topic;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x0015EA19 File Offset: 0x0015CC19
		public byte[] SerializedTreeState
		{
			get
			{
				return this.ConversationState.SerializedState;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x0015EA26 File Offset: 0x0015CC26
		public IEnumerable<IConversationThread> Threads
		{
			get
			{
				return this.conversationThreads;
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06005494 RID: 21652 RVA: 0x0015EA2E File Offset: 0x0015CC2E
		private ConversationState ConversationState
		{
			get
			{
				if (this.conversationState == null)
				{
					this.conversationState = this.stateFactory.Create(null);
				}
				return this.conversationState;
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x0015EA50 File Offset: 0x0015CC50
		public IConversationStatistics ConversationStatistics
		{
			get
			{
				return this.dataExtractor.OptimizationCounters;
			}
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0015EA5D File Offset: 0x0015CC5D
		public ItemPart GetItemPart(StoreObjectId itemId)
		{
			return this.dataExtractor.GetItemPart(this.conversationTree, itemId);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0015EA71 File Offset: 0x0015CC71
		public ParticipantSet AllParticipants(ICollection<IConversationTreeNode> loadedNodes = null)
		{
			return this.dataExtractor.CalculateAllRecipients(this.ConversationTree, loadedNodes);
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0015EA88 File Offset: 0x0015CC88
		public byte[] GetSerializedTreeStateWithNodesToExclude(ICollection<IConversationTreeNode> nodesToExclude)
		{
			ConversationState conversationState = this.stateFactory.Create(nodesToExclude);
			return conversationState.SerializedState;
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0015EAA8 File Offset: 0x0015CCA8
		public void LoadBodySummaries()
		{
			this.dataExtractor.LoadBodySummaries(this.conversationTree);
			this.SyncThreads();
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0015EAC9 File Offset: 0x0015CCC9
		public void LoadItemParts(ICollection<IConversationTreeNode> nodes)
		{
			this.LoadItemParts(from n in nodes
			select n.MainStoreObjectId);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0015EAF4 File Offset: 0x0015CCF4
		public List<StoreObjectId> GetMessageIdsForPreread()
		{
			return this.dataExtractor.GetMessageIdsForPreread(this.ConversationTree);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x0015EB07 File Offset: 0x0015CD07
		public void LoadItemParts(ICollection<StoreObjectId> storeIds)
		{
			this.dataExtractor.LoadItemParts(this.conversationTree, storeIds);
			this.SyncThreads();
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0015EB21 File Offset: 0x0015CD21
		public KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> CalculateChanges(byte[] olderState)
		{
			return this.ConversationState.CalculateChanges(olderState);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0015EB30 File Offset: 0x0015CD30
		private void SyncThreads()
		{
			foreach (IConversationThread conversationThread in this.conversationThreads)
			{
				conversationThread.SyncThread();
			}
		}

		// Token: 0x04002D9C RID: 11676
		private readonly ConversationDataExtractor dataExtractor;

		// Token: 0x04002D9D RID: 11677
		private readonly ConversationStateFactory stateFactory;

		// Token: 0x04002D9E RID: 11678
		private readonly ConversationId conversationId;

		// Token: 0x04002D9F RID: 11679
		private ConversationState conversationState;

		// Token: 0x04002DA0 RID: 11680
		private IConversationTree conversationTree;

		// Token: 0x04002DA1 RID: 11681
		private IList<IConversationThread> conversationThreads;
	}
}
