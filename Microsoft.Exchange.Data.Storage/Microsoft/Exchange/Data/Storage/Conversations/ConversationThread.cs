using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D7 RID: 2263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationThread : IConversationThread, IBreadcrumbsSource, IConversationData, IThreadAggregatedProperties
	{
		// Token: 0x0600545E RID: 21598 RVA: 0x0015E434 File Offset: 0x0015C634
		internal ConversationThread(ConversationDataExtractor dataExtractor, ConversationThreadDataExtractor threadDataExtractor, IConversationTree threadTree, IConversationTreeFactory factory)
		{
			this.threadTree = threadTree;
			this.dataExtractor = dataExtractor;
			this.threadDataExtractor = threadDataExtractor;
			this.factory = factory;
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x0015E459 File Offset: 0x0015C659
		public IStorePropertyBag BackwardMessagePropertyBag
		{
			get
			{
				if (this.backwardMessagePropertyBag == null)
				{
					this.backwardMessagePropertyBag = this.threadDataExtractor.CalculateBackwardPropertyBag(this.Tree.RootMessageId);
				}
				return this.backwardMessagePropertyBag;
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x0015E490 File Offset: 0x0015C690
		public Dictionary<StoreObjectId, List<IStorePropertyBag>> ForwardConversationRootMessagePropertyBags
		{
			get
			{
				if (this.forwardConversationRootMessagePropertyBags == null)
				{
					this.forwardConversationRootMessagePropertyBags = this.threadDataExtractor.CalculateForwardMessagePropertyBags(this.ThreadId, from node in this.Tree
					select node.MainStoreObjectId);
				}
				return this.forwardConversationRootMessagePropertyBags;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x0015E4EA File Offset: 0x0015C6EA
		public StoreObjectId RootMessageId
		{
			get
			{
				return this.Tree.RootMessageId;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0015E4F7 File Offset: 0x0015C6F7
		public IConversationTree Tree
		{
			get
			{
				return this.threadTree;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x0015E4FF File Offset: 0x0015C6FF
		public StoreObjectId[] ItemIds
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<StoreObjectId[]>(AggregatedConversationSchema.ItemIds, Array<StoreObjectId>.Empty);
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x0015E516 File Offset: 0x0015C716
		public StoreObjectId[] DraftItemIds
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<StoreObjectId[]>(AggregatedConversationSchema.DraftItemIds, Array<StoreObjectId>.Empty);
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x0015E52D File Offset: 0x0015C72D
		public int ItemCount
		{
			get
			{
				return this.Tree.Count;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06005466 RID: 21606 RVA: 0x0015E53A File Offset: 0x0015C73A
		public bool HasAttachments
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<bool>(AggregatedConversationSchema.HasAttachments, false);
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x0015E54D File Offset: 0x0015C74D
		public ConversationId ThreadId
		{
			get
			{
				return this.Tree.RootMessageNode.ConversationThreadId;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x0015E560 File Offset: 0x0015C760
		public ExDateTime? LastDeliveryTime
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<ExDateTime?>(AggregatedConversationSchema.LastDeliveryTime, null);
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06005469 RID: 21609 RVA: 0x0015E586 File Offset: 0x0015C786
		public Participant[] UniqueSenders
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<Participant[]>(AggregatedConversationSchema.DirectParticipants, Array<Participant>.Empty);
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x0600546A RID: 21610 RVA: 0x0015E59D File Offset: 0x0015C79D
		public string Preview
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<string>(AggregatedConversationSchema.Preview, string.Empty);
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x0015E5B4 File Offset: 0x0015C7B4
		public bool HasIrm
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<bool>(AggregatedConversationSchema.HasIrm, false);
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x0015E5C7 File Offset: 0x0015C7C7
		public Importance Importance
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<Importance>(AggregatedConversationSchema.Importance, Importance.Normal);
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x0015E5DA File Offset: 0x0015C7DA
		public IconIndex IconIndex
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<IconIndex>(AggregatedConversationSchema.IconIndex, IconIndex.Default);
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x0015E5ED File Offset: 0x0015C7ED
		public FlagStatus FlagStatus
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<FlagStatus>(AggregatedConversationSchema.FlagStatus, FlagStatus.NotFlagged);
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0015E600 File Offset: 0x0015C800
		public int UnreadCount
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<int>(AggregatedConversationSchema.UnreadCount, 0);
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x0015E613 File Offset: 0x0015C813
		public short[] RichContent
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<short[]>(AggregatedConversationSchema.RichContent, Array<short>.Empty);
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x0015E62A File Offset: 0x0015C82A
		public string[] ItemClasses
		{
			get
			{
				return this.AggregatedProperties.GetValueOrDefault<string[]>(AggregatedConversationSchema.ItemClasses, Array<string>.Empty);
			}
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0015E641 File Offset: 0x0015C841
		public void SyncThread()
		{
			this.threadDataExtractor.SyncConversationTreeNodesContent(this.Tree);
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0015E654 File Offset: 0x0015C854
		public ParticipantTable LoadReplyAllParticipantsPerType()
		{
			return this.dataExtractor.LoadReplyAllParticipantsPerType(this.Tree);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0015E667 File Offset: 0x0015C867
		public ParticipantSet LoadReplyAllParticipants(IConversationTreeNode node)
		{
			return this.dataExtractor.LoadReplyAllParticipants(this.Tree, node);
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0015E67B File Offset: 0x0015C87B
		public int GetNodeCount(bool includeSubmitted)
		{
			return this.Tree.GetNodeCount(includeSubmitted);
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x0015E689 File Offset: 0x0015C889
		public IConversationTreeNode FirstNode
		{
			get
			{
				return this.threadTree.RootMessageNode;
			}
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0015E696 File Offset: 0x0015C896
		public Dictionary<IConversationTreeNode, ParticipantSet> LoadAddedParticipants()
		{
			return this.dataExtractor.LoadAddedParticipants(this.Tree);
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0015E6A9 File Offset: 0x0015C8A9
		public Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph()
		{
			return this.Tree.BuildPreviousNodeGraph();
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0015E6B6 File Offset: 0x0015C8B6
		public IConversationTree GetNewestSubTree(int count)
		{
			return this.factory.GetNewestSubTree(this.threadTree, count);
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x0600547A RID: 21626 RVA: 0x0015E6CA File Offset: 0x0015C8CA
		private IStorePropertyBag AggregatedProperties
		{
			get
			{
				if (this.aggregatedProperties == null)
				{
					this.aggregatedProperties = this.threadDataExtractor.ConstructThreadAggregatedProperties(this.Tree);
				}
				return this.aggregatedProperties;
			}
		}

		// Token: 0x04002D8D RID: 11661
		private readonly ConversationDataExtractor dataExtractor;

		// Token: 0x04002D8E RID: 11662
		private readonly ConversationThreadDataExtractor threadDataExtractor;

		// Token: 0x04002D8F RID: 11663
		private readonly IConversationTree threadTree;

		// Token: 0x04002D90 RID: 11664
		private readonly IConversationTreeFactory factory;

		// Token: 0x04002D91 RID: 11665
		private IStorePropertyBag backwardMessagePropertyBag;

		// Token: 0x04002D92 RID: 11666
		private IStorePropertyBag aggregatedProperties;

		// Token: 0x04002D93 RID: 11667
		private Dictionary<StoreObjectId, List<IStorePropertyBag>> forwardConversationRootMessagePropertyBags;
	}
}
