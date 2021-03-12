using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.StructuredQuery;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008DE RID: 2270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Conversation : IConversation, ICoreConversation, IConversationData
	{
		// Token: 0x060054DC RID: 21724 RVA: 0x00160712 File Offset: 0x0015E912
		internal Conversation(ConversationId conversationId, IConversationTree conversationTree, MailboxSession session, ConversationDataExtractor conversationDataExtractor, IConversationTreeFactory conversationTreeFactory, ConversationStateFactory stateFactory)
		{
			this.conversationId = conversationId;
			this.conversationTree = conversationTree;
			this.session = session;
			this.conversationDataExtractor = conversationDataExtractor;
			this.conversationTreeFactory = conversationTreeFactory;
			this.stateFactory = stateFactory;
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00160748 File Offset: 0x0015E948
		public static Conversation Load(MailboxSession session, ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] propertyDefinitions)
		{
			ConversationFactory conversationFactory = new ConversationFactory(session);
			return conversationFactory.CreateConversation(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, propertyDefinitions);
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00160769 File Offset: 0x0015E969
		internal static Conversation Load(MailboxSession session, ConversationId conversationId, IList<StoreObjectId> foldersToIgnore, bool isIrmEnabled, params PropertyDefinition[] propertyDefinitions)
		{
			return Conversation.Load(session, conversationId, foldersToIgnore, true, isIrmEnabled, propertyDefinitions);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x00160777 File Offset: 0x0015E977
		public static Conversation Load(MailboxSession session, ConversationId conversationId, IList<StoreObjectId> foldersToIgnore, params PropertyDefinition[] propertyDefinitions)
		{
			return Conversation.Load(session, conversationId, foldersToIgnore, false, propertyDefinitions);
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00160783 File Offset: 0x0015E983
		public static Conversation Load(MailboxSession session, ConversationId conversationId, params PropertyDefinition[] propertyDefinitions)
		{
			return Conversation.Load(session, conversationId, null, false, propertyDefinitions);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0016078F File Offset: 0x0015E98F
		public static Conversation Load(MailboxSession session, ConversationId conversationId, bool isIrmEnabled, params PropertyDefinition[] propertyDefinitions)
		{
			return Conversation.Load(session, conversationId, null, isIrmEnabled, propertyDefinitions);
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x060054E2 RID: 21730 RVA: 0x0016079B File Offset: 0x0015E99B
		// (set) Token: 0x060054E3 RID: 21731 RVA: 0x001607A2 File Offset: 0x0015E9A2
		public static long MaxBytesForConversation
		{
			get
			{
				return ConversationDataExtractor.MaxBytesForConversation;
			}
			set
			{
				ConversationDataExtractor.MaxBytesForConversation = value;
			}
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x001607AA File Offset: 0x0015E9AA
		public ItemPart GetItemPart(StoreObjectId itemId)
		{
			return this.ConversationDataExtractor.GetItemPart(this.ConversationTree, itemId);
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x001607BE File Offset: 0x0015E9BE
		public StoreObjectId RootMessageId
		{
			get
			{
				if (this.ConversationTree.RootMessageNode != null)
				{
					return this.ConversationTree.RootMessageNode.MainStoreObjectId;
				}
				return null;
			}
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x001607DF File Offset: 0x0015E9DF
		public void LoadBodySummaries()
		{
			this.ConversationDataExtractor.LoadBodySummaries(this.ConversationTree);
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x001607FC File Offset: 0x0015E9FC
		public void LoadItemParts(ICollection<IConversationTreeNode> nodes)
		{
			Util.ThrowOnNullOrEmptyArgument(nodes, "nodes");
			this.ConversationDataExtractor.LoadItemParts(this.ConversationTree, from node in nodes
			select node.MainStoreObjectId);
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00160848 File Offset: 0x0015EA48
		public void LoadItemParts(ICollection<StoreObjectId> storeIds)
		{
			Util.ThrowOnNullOrEmptyArgument(storeIds, "storeIds");
			this.ConversationDataExtractor.LoadItemParts(this.ConversationTree, storeIds);
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x00160867 File Offset: 0x0015EA67
		public void TrimToNewest(int count)
		{
			if (this.ConversationDataExtractor.ConversationDataLoaded)
			{
				throw new InvalidOperationException("LoadItemParts or LoadBodySummary already called");
			}
			this.conversationTree = this.conversationTreeFactory.GetNewestSubTree(this.conversationTree, count);
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x00160899 File Offset: 0x0015EA99
		public void LoadItemParts(IList<StoreObjectId> storeIds, string searchString, CultureInfo cultureinfo, out List<IConversationTreeNode> nodes)
		{
			Util.ThrowOnNullOrEmptyArgument(storeIds, "storeIds");
			this.LoadItemParts(this.conversationTree, storeIds, searchString, cultureinfo, out nodes);
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x001608B8 File Offset: 0x0015EAB8
		public void LoadItemParts(IConversationTree conversationTree, IList<StoreObjectId> storeIds, string searchString, CultureInfo cultureinfo, out List<IConversationTreeNode> nodes)
		{
			Util.ThrowOnNullOrEmptyArgument(storeIds, "storeIds");
			nodes = new List<IConversationTreeNode>(0);
			this.ConversationDataExtractor.LoadItemParts(conversationTree, new Collection<StoreObjectId>(storeIds));
			if (string.IsNullOrEmpty(searchString))
			{
				nodes = this.MatchAllNodes(storeIds);
				return;
			}
			IList<string> words = null;
			if (this.InternalTryAqsMatch(storeIds, searchString, cultureinfo, out words, out nodes))
			{
				return;
			}
			bool flag = false;
			foreach (StoreObjectId storeObjectId in storeIds)
			{
				ItemPart itemPart = this.ConversationDataExtractor.GetItemPart(conversationTree, storeObjectId);
				if (itemPart.UniqueFragmentInfo != null && itemPart.UniqueFragmentInfo.IsMatchFound(words))
				{
					IConversationTreeNode item = null;
					if (conversationTree.TryGetConversationTreeNode(storeObjectId, out item))
					{
						nodes.Add(item);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				nodes = this.MatchAllNodes(storeIds);
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x00160998 File Offset: 0x0015EB98
		public KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> CalculateChanges(byte[] olderState)
		{
			return this.ConversationState.CalculateChanges(olderState);
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x001609A8 File Offset: 0x0015EBA8
		public AggregateOperationResult AlwaysDelete(bool enable, bool processItems)
		{
			AggregateOperationResult result = null;
			using (ConversationActionItem associatedActionItem = this.GetAssociatedActionItem())
			{
				associatedActionItem.AlwaysDeleteValue = enable;
				associatedActionItem.ConversationActionMaxDeliveryTime = ExDateTime.MinValue;
				if (enable && processItems)
				{
					result = associatedActionItem.ProcessItems(ConversationAction.AlwaysDelete, this);
				}
				associatedActionItem.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00160A04 File Offset: 0x0015EC04
		public AggregateOperationResult AlwaysMove(StoreObjectId folderId, bool processItems)
		{
			AggregateOperationResult result = null;
			using (ConversationActionItem associatedActionItem = this.GetAssociatedActionItem())
			{
				associatedActionItem.AlwaysMoveValue = folderId;
				if (folderId != null && processItems)
				{
					result = associatedActionItem.ProcessItems(ConversationAction.AlwaysMove, this);
				}
				associatedActionItem.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00160A58 File Offset: 0x0015EC58
		public AggregateOperationResult AlwaysCategorize(string[] categories, bool processItems)
		{
			AggregateOperationResult result = null;
			using (ConversationActionItem associatedActionItem = this.GetAssociatedActionItem())
			{
				associatedActionItem.AlwaysCategorizeValue = categories;
				if (categories != null && processItems)
				{
					result = associatedActionItem.ProcessItems(ConversationAction.AlwaysCategorize, this);
				}
				associatedActionItem.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00160AAC File Offset: 0x0015ECAC
		public AggregateOperationResult AlwaysClutterOrUnclutter(bool? clutterOrUnclutter, bool processItems)
		{
			AggregateOperationResult result = null;
			using (ConversationActionItem associatedActionItem = this.GetAssociatedActionItem())
			{
				associatedActionItem.AlwaysClutterOrUnclutterValue = clutterOrUnclutter;
				if (clutterOrUnclutter != null && processItems)
				{
					result = associatedActionItem.ProcessItems(ConversationAction.AlwaysClutterOrUnclutter, this);
				}
				associatedActionItem.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00160B04 File Offset: 0x0015ED04
		public AggregateOperationResult Delete(StoreObjectId contextFolderId, DateTime? actionTime, DeleteItemFlags deleteFlags)
		{
			return this.Delete(contextFolderId, actionTime, deleteFlags, false);
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00160B10 File Offset: 0x0015ED10
		public AggregateOperationResult Delete(StoreObjectId contextFolderId, DateTime? actionTime, DeleteItemFlags deleteFlags, bool returnNewItemIds)
		{
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteFlags, "deleteFlags");
			List<StoreObjectId> filteredItemIds = this.GetFilteredItemIds(contextFolderId, actionTime, null);
			return this.session.Delete(deleteFlags, returnNewItemIds, filteredItemIds.ToArray());
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00160BA4 File Offset: 0x0015EDA4
		public AggregateOperationResult ClearFlags(StoreObjectId contextFolderId, DateTime? actionTime)
		{
			StoreObjectId[] array = this.GetFilteredItemIds(contextFolderId, actionTime, (IStorePropertyBag propertyBag) => !propertyBag.TryGetProperty(ItemSchema.FlagStatus).Equals(FlagStatus.NotFlagged)).ToArray();
			List<GroupOperationResult> groupOperationResults = new List<GroupOperationResult>(array.Length);
			StoreObjectId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				StoreObjectId itemId = array2[i];
				Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
				{
					itemId
				}, () => this.PerformFlaggingOperation(itemId, delegate(Item item)
				{
					item.ClearFlag();
				}));
			}
			return Folder.CreateAggregateOperationResult(groupOperationResults);
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x00160C88 File Offset: 0x0015EE88
		public AggregateOperationResult SetFlags(StoreObjectId contextFolderId, DateTime? actionTime, string flagRequest, ExDateTime? startDate, ExDateTime? dueDate)
		{
			List<StoreObjectId> filteredItemIds = this.GetFilteredItemIds(contextFolderId, actionTime, null);
			StoreObjectId itemIdToFlag = this.GetMostRecentItem(filteredItemIds);
			List<GroupOperationResult> groupOperationResults = new List<GroupOperationResult>(1);
			if (itemIdToFlag != null)
			{
				Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
				{
					itemIdToFlag
				}, () => this.PerformFlaggingOperation(itemIdToFlag, delegate(Item item)
				{
					item.SetFlag(flagRequest, startDate, dueDate);
				}));
			}
			return Folder.CreateAggregateOperationResult(groupOperationResults);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x00160DB0 File Offset: 0x0015EFB0
		public AggregateOperationResult CompleteFlags(StoreObjectId contextFolderId, DateTime? actionTime, ExDateTime? completeDate)
		{
			Conversation.<>c__DisplayClass15 CS$<>8__locals1 = new Conversation.<>c__DisplayClass15();
			CS$<>8__locals1.completeDate = completeDate;
			CS$<>8__locals1.<>4__this = this;
			List<StoreObjectId> filteredItemIds = this.GetFilteredItemIds(contextFolderId, actionTime, (IStorePropertyBag propertyBag) => propertyBag.TryGetProperty(ItemSchema.FlagStatus).Equals(FlagStatus.Flagged));
			List<GroupOperationResult> groupOperationResults = null;
			if (filteredItemIds.Count > 0)
			{
				groupOperationResults = new List<GroupOperationResult>(filteredItemIds.Count);
				using (List<StoreObjectId>.Enumerator enumerator = filteredItemIds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StoreObjectId itemId = enumerator.Current;
						Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
						{
							itemId
						}, () => CS$<>8__locals1.<>4__this.PerformFlaggingOperation(itemId, delegate(Item item)
						{
							item.CompleteFlag(CS$<>8__locals1.completeDate);
						}));
					}
					goto IL_12F;
				}
			}
			filteredItemIds = this.GetFilteredItemIds(contextFolderId, actionTime, null);
			groupOperationResults = new List<GroupOperationResult>(filteredItemIds.Count);
			StoreObjectId itemIdToFlag = this.GetMostRecentItem(filteredItemIds);
			if (itemIdToFlag != null)
			{
				Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
				{
					itemIdToFlag
				}, () => CS$<>8__locals1.<>4__this.PerformFlaggingOperation(itemIdToFlag, delegate(Item item)
				{
					item.CompleteFlag(CS$<>8__locals1.completeDate);
				}));
			}
			IL_12F:
			return Folder.CreateAggregateOperationResult(groupOperationResults);
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x00160F04 File Offset: 0x0015F104
		public AggregateOperationResult Copy(StoreObjectId contextFolderId, DateTime? actionTime, StoreSession destinationSession, StoreObjectId destinationFolderId)
		{
			StoreObjectId[] ids = this.GetFilteredItemIds(contextFolderId, actionTime, null).ToArray();
			if (destinationSession != null)
			{
				return this.session.Copy(destinationSession, destinationFolderId, ids);
			}
			return this.session.Copy(destinationFolderId, ids);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x00160F41 File Offset: 0x0015F141
		public AggregateOperationResult Move(StoreObjectId contextFolderId, DateTime? actionTime, StoreSession destinationSession, StoreObjectId destinationFolderId)
		{
			return this.Move(contextFolderId, actionTime, destinationSession, destinationFolderId, false);
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x00160F50 File Offset: 0x0015F150
		public AggregateOperationResult Move(StoreObjectId contextFolderId, DateTime? actionTime, StoreSession destinationSession, StoreObjectId destinationFolderId, bool returnNewItemIds)
		{
			StoreObjectId[] ids = this.GetFilteredItemIds(contextFolderId, actionTime, null).ToArray();
			if (destinationSession != null)
			{
				return this.session.Move(destinationSession, destinationFolderId, returnNewItemIds, ids);
			}
			return this.session.Move(destinationFolderId, ids);
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x00160FB8 File Offset: 0x0015F1B8
		public AggregateOperationResult SetIsReadState(StoreObjectId contextFolderId, DateTime? actionTime, bool isRead, bool suppressReadReceipts)
		{
			StoreObjectId[] array = this.GetFilteredItemIds(contextFolderId, actionTime, (IStorePropertyBag propertyBag) => propertyBag.TryGetProperty(MessageItemSchema.IsRead).Equals(!isRead)).ToArray();
			if (isRead)
			{
				this.session.MarkAsRead(suppressReadReceipts, array);
			}
			else
			{
				this.session.MarkAsUnread(suppressReadReceipts, array);
			}
			return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[]
			{
				new GroupOperationResult(OperationResult.Succeeded, array, null)
			});
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0016106C File Offset: 0x0015F26C
		public AggregateOperationResult SetRetentionPolicy(StoreObjectId contextFolderId, DateTime? actionTime, PolicyTag policyTag, bool isArchiveAction)
		{
			StoreObjectId[] array = this.GetFilteredItemIds(contextFolderId, actionTime, null).ToArray();
			List<GroupOperationResult> groupOperationResults = new List<GroupOperationResult>(array.Length);
			if (array != null)
			{
				StoreObjectId[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					StoreObjectId itemId = array2[i];
					Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
					{
						itemId
					}, () => this.SetRetentionPolicyInternal(itemId, policyTag, isArchiveAction));
				}
			}
			return Folder.CreateAggregateOperationResult(groupOperationResults);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x00161116 File Offset: 0x0015F316
		public ParticipantTable LoadReplyAllParticipantsPerType()
		{
			return this.ConversationDataExtractor.LoadReplyAllParticipantsPerType(this.conversationTree);
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00161129 File Offset: 0x0015F329
		public ParticipantSet LoadReplyAllParticipants(IConversationTreeNode node)
		{
			return this.ConversationDataExtractor.LoadReplyAllParticipants(this.conversationTree, node);
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x0016113D File Offset: 0x0015F33D
		public IConversationTreeNode RootMessageNode
		{
			get
			{
				return this.conversationTree.RootMessageNode;
			}
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0016114A File Offset: 0x0015F34A
		public int GetNodeCount(bool includeSubmitted)
		{
			return this.conversationTree.GetNodeCount(includeSubmitted);
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x00161158 File Offset: 0x0015F358
		public IConversationTreeNode FirstNode
		{
			get
			{
				return this.RootMessageNode;
			}
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00161160 File Offset: 0x0015F360
		public Dictionary<IConversationTreeNode, ParticipantSet> LoadAddedParticipants()
		{
			return this.ConversationDataExtractor.LoadAddedParticipants(this.conversationTree);
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x00161173 File Offset: 0x0015F373
		public Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph()
		{
			return this.conversationTree.BuildPreviousNodeGraph();
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00161180 File Offset: 0x0015F380
		public IConversationTree GetNewestSubTree(int count)
		{
			return this.conversationTreeFactory.GetNewestSubTree(this.conversationTree, count);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x00161194 File Offset: 0x0015F394
		public List<StoreObjectId> GetMessageIdsForPreread()
		{
			return this.ConversationDataExtractor.GetMessageIdsForPreread(this.ConversationTree);
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x001611A7 File Offset: 0x0015F3A7
		public bool ConversationNodeContainedInChildren(IConversationTreeNode node)
		{
			Util.ThrowOnNullOrEmptyArgument(node, "node");
			return this.ConversationDataExtractor.ConversationNodeContainedInChildren(this.conversationTree, node);
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x001611C6 File Offset: 0x0015F3C6
		public byte[] ConversationCreatorSID
		{
			get
			{
				return this.conversationTree.ConversationCreatorSID;
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x001611D3 File Offset: 0x0015F3D3
		public ConversationId ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x001611DB File Offset: 0x0015F3DB
		public IConversationTree ConversationTree
		{
			get
			{
				return this.conversationTree;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x001611E3 File Offset: 0x0015F3E3
		public string Topic
		{
			get
			{
				return this.ConversationTree.Topic;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x001611F0 File Offset: 0x0015F3F0
		public EffectiveRights EffectiveRights
		{
			get
			{
				return this.ConversationTree.EffectiveRights;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600550A RID: 21770 RVA: 0x001611FD File Offset: 0x0015F3FD
		// (remove) Token: 0x0600550B RID: 21771 RVA: 0x0016120B File Offset: 0x0015F40B
		public event OnBeforeItemLoadEventDelegate OnBeforeItemLoad
		{
			add
			{
				this.ConversationDataExtractor.OnBeforeItemLoad += value;
			}
			remove
			{
				this.ConversationDataExtractor.OnBeforeItemLoad -= value;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00161219 File Offset: 0x0015F419
		public OptimizationInfo OptimizationCounters
		{
			get
			{
				return this.ConversationDataExtractor.OptimizationCounters;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x00161226 File Offset: 0x0015F426
		public IConversationStatistics ConversationStatistics
		{
			get
			{
				return this.OptimizationCounters;
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x0016122E File Offset: 0x0015F42E
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

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x00161250 File Offset: 0x0015F450
		public byte[] SerializedTreeState
		{
			get
			{
				return this.ConversationState.SerializedState;
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x0016125D File Offset: 0x0015F45D
		protected ConversationDataExtractor ConversationDataExtractor
		{
			get
			{
				return this.conversationDataExtractor;
			}
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00161268 File Offset: 0x0015F468
		public byte[] GetSerializedTreeStateWithNodesToExclude(ICollection<IConversationTreeNode> nodesToExclude)
		{
			ConversationState conversationState = this.stateFactory.Create(nodesToExclude);
			return conversationState.SerializedState;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00161288 File Offset: 0x0015F488
		public ParticipantSet AllParticipants(ICollection<IConversationTreeNode> loadedNodes = null)
		{
			return this.ConversationDataExtractor.CalculateAllRecipients(this.ConversationTree, loadedNodes);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0016129C File Offset: 0x0015F49C
		protected List<StoreObjectId> GetFilteredItemIds(StoreObjectId localFolderId, DateTime? createdBefore, Func<IStorePropertyBag, bool> filter)
		{
			HashSet<StoreObjectId> localItemIds = this.GetLocalItemIds(localFolderId);
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in this.ConversationTree)
			{
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode.StorePropertyBags)
				{
					StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
					if (localItemIds.Contains(objectId))
					{
						if (createdBefore != null && createdBefore != null)
						{
							ExDateTime? exDateTime = storePropertyBag.TryGetProperty(ItemSchema.ReceivedTime) as ExDateTime?;
							if (exDateTime == null || (exDateTime.Value.UniversalTime - createdBefore.Value.ToUniversalTime()).TotalSeconds > 1.0)
							{
								continue;
							}
						}
						if (filter == null || filter(storePropertyBag))
						{
							list.Add(objectId);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001613E0 File Offset: 0x0015F5E0
		private GroupOperationResult PerformFlaggingOperation(StoreObjectId itemId, Action<Item> flaggingAction)
		{
			using (Item item = Item.Bind(this.session, itemId, Conversation.flaggingProperties))
			{
				item.OpenAsReadWrite();
				flaggingAction(item);
				item.Save(SaveMode.ResolveConflicts);
			}
			return new GroupOperationResult(OperationResult.Succeeded, new StoreObjectId[]
			{
				itemId
			}, null);
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x00161444 File Offset: 0x0015F644
		private HashSet<StoreObjectId> GetLocalItemIds(StoreObjectId localFolderId)
		{
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in this.ConversationTree)
			{
				foreach (StoreObjectId storeObjectId in conversationTreeNode.ToListStoreObjectId())
				{
					StoreObjectId valueOrDefault = conversationTreeNode.GetValueOrDefault<StoreObjectId>(storeObjectId, StoreObjectSchema.ParentItemId, null);
					if (localFolderId == null || localFolderId.CompareTo(valueOrDefault) == 0)
					{
						hashSet.Add(storeObjectId);
					}
				}
			}
			if (hashSet.Count == 0 && localFolderId != null)
			{
				using (Folder folder = Folder.Bind(this.session, localFolderId))
				{
					SearchFolder searchFolder = folder as SearchFolder;
					if (searchFolder != null)
					{
						using (QueryResult queryResult = searchFolder.ConversationItemQuery(null, new SortBy[]
						{
							new SortBy(ConversationItemSchema.ConversationId, SortOrder.Ascending)
						}, new PropertyDefinition[]
						{
							ConversationItemSchema.ConversationItemIds
						}))
						{
							if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ConversationItemSchema.ConversationId, this.ConversationId)))
							{
								IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
								if (propertyBags.Length > 0)
								{
									StoreObjectId[] array = propertyBags[0].TryGetProperty(ConversationItemSchema.ConversationItemIds) as StoreObjectId[];
									if (array != null)
									{
										foreach (StoreObjectId item in array)
										{
											hashSet.Add(item);
										}
									}
								}
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x001615FC File Offset: 0x0015F7FC
		public void AggregateHeaders(params IAggregatorRule[] aggregationRules)
		{
			Util.ThrowOnNullArgument(aggregationRules, "aggregationRules");
			foreach (IAggregatorRule aggregatorRule in aggregationRules)
			{
				aggregatorRule.BeginAggregation();
			}
			foreach (IConversationTreeNode conversationTreeNode in this.ConversationTree)
			{
				foreach (IAggregatorRule aggregatorRule2 in aggregationRules)
				{
					if (conversationTreeNode.HasData)
					{
						aggregatorRule2.AddToAggregation(conversationTreeNode.MainPropertyBag);
					}
				}
			}
			foreach (IAggregatorRule aggregatorRule3 in aggregationRules)
			{
				aggregatorRule3.EndAggregation();
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001616C8 File Offset: 0x0015F8C8
		private ConversationActionItem GetAssociatedActionItem()
		{
			ConversationActionItem result = null;
			try
			{
				result = ConversationActionItem.Bind(this.session, this.ConversationId);
			}
			catch (ObjectNotFoundException)
			{
				string conversationTopic = (this.Topic == null) ? string.Empty : this.Topic;
				result = ConversationActionItem.Create(this.session, this.ConversationId, conversationTopic);
			}
			return result;
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00161728 File Offset: 0x0015F928
		private List<IConversationTreeNode> MatchAllNodes(IList<StoreObjectId> storeIds)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>(0);
			foreach (StoreObjectId storeObjectId in storeIds)
			{
				IConversationTreeNode item = null;
				if (this.ConversationTree.TryGetConversationTreeNode(storeObjectId, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0016178C File Offset: 0x0015F98C
		private GroupOperationResult SetRetentionPolicyInternal(StoreObjectId itemId, PolicyTag policyTag, bool isArchiveAction)
		{
			PropertyDefinition[] propsToReturn = isArchiveAction ? PolicyTagHelper.ArchiveProperties : PolicyTagHelper.RetentionProperties;
			using (Item item = Item.Bind(this.session, itemId, propsToReturn))
			{
				item.OpenAsReadWrite();
				if (isArchiveAction)
				{
					if (policyTag == null)
					{
						PolicyTagHelper.ClearPolicyTagForArchiveOnItem(item);
					}
					else
					{
						PolicyTagHelper.SetPolicyTagForArchiveOnItem(policyTag, item);
					}
				}
				else if (policyTag == null)
				{
					PolicyTagHelper.ClearPolicyTagForDeleteOnItem(item);
				}
				else
				{
					PolicyTagHelper.SetPolicyTagForDeleteOnItem(policyTag, item);
				}
				item.Save(SaveMode.ResolveConflicts);
			}
			return new GroupOperationResult(OperationResult.Succeeded, new StoreObjectId[]
			{
				itemId
			}, null);
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x00161820 File Offset: 0x0015FA20
		private StoreObjectId GetMostRecentItem(List<StoreObjectId> itemIds)
		{
			StoreObjectId storeObjectId = null;
			if (itemIds.Count == 1)
			{
				storeObjectId = itemIds[0];
			}
			else
			{
				ExDateTime t = ExDateTime.MinValue;
				foreach (StoreObjectId storeObjectId2 in itemIds)
				{
					ExDateTime valueOrDefault = this.ConversationTree.GetValueOrDefault<ExDateTime>(storeObjectId2, ItemSchema.ReceivedTime, ExDateTime.MinValue);
					if (valueOrDefault > t)
					{
						storeObjectId = storeObjectId2;
						t = valueOrDefault;
					}
				}
				if (storeObjectId == null && itemIds.Count > 0)
				{
					storeObjectId = itemIds[0];
				}
			}
			return storeObjectId;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x001618BC File Offset: 0x0015FABC
		private static string PredicateToBodyQueryString(LeafCondition leafCond)
		{
			if (!AqsParser.PropertyKeywordMap.ContainsKey(leafCond.PropertyName))
			{
				return null;
			}
			PropertyKeyword? propertyKeyword = new PropertyKeyword?(AqsParser.PropertyKeywordMap[leafCond.PropertyName]);
			if (propertyKeyword == null)
			{
				return null;
			}
			if (propertyKeyword.Value == PropertyKeyword.All || propertyKeyword.Value == PropertyKeyword.Body)
			{
				return (string)leafCond.Value;
			}
			return null;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00161924 File Offset: 0x0015FB24
		private static List<string> ConditionToBodyQueryString(Condition condition)
		{
			List<string> list = new List<string>();
			if (condition.Type == null || condition.Type == 1)
			{
				using (List<Condition>.Enumerator enumerator = ((CompoundCondition)condition).Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Condition condition2 = enumerator.Current;
						List<string> list2 = Conversation.ConditionToBodyQueryString(condition2);
						if (list2.Count > 0)
						{
							list.AddRange(list2);
						}
					}
					return list;
				}
			}
			if (condition.Type == 2)
			{
				List<string> list3 = Conversation.ConditionToBodyQueryString(((NegationCondition)condition).Child);
				if (list3.Count > 0)
				{
					list.AddRange(list3);
				}
			}
			else
			{
				if (condition.Type != 3)
				{
					throw new ArgumentException("No condition node other than NOT, AND, OR and Leaf is allowed.");
				}
				string text = Conversation.PredicateToBodyQueryString((LeafCondition)condition);
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00161A08 File Offset: 0x0015FC08
		private bool InternalTryAqsMatch(IList<StoreObjectId> storeIds, string searchString, CultureInfo cultureinfo, out IList<string> bodySearchString, out List<IConversationTreeNode> nodes)
		{
			nodes = null;
			bodySearchString = null;
			AqsParser aqsParser = new AqsParser();
			using (Condition condition = aqsParser.Parse(searchString, AqsParser.ParseOption.SuppressError, cultureinfo))
			{
				bodySearchString = Conversation.ConditionToBodyQueryString(condition);
				if (bodySearchString == null || bodySearchString.Count < 1)
				{
					nodes = this.MatchAllNodes(storeIds);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002DB6 RID: 11702
		private const double MaxTimeForRoundTripThroughWebServices = 1.0;

		// Token: 0x04002DB7 RID: 11703
		private static readonly PropertyDefinition[] flaggingProperties = new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			TaskSchema.StartDate,
			TaskSchema.DueDate,
			ItemSchema.CompleteDate
		};

		// Token: 0x04002DB8 RID: 11704
		private readonly ConversationId conversationId;

		// Token: 0x04002DB9 RID: 11705
		private readonly MailboxSession session;

		// Token: 0x04002DBA RID: 11706
		private readonly ConversationDataExtractor conversationDataExtractor;

		// Token: 0x04002DBB RID: 11707
		private readonly IConversationTreeFactory conversationTreeFactory;

		// Token: 0x04002DBC RID: 11708
		private readonly ConversationStateFactory stateFactory;

		// Token: 0x04002DBD RID: 11709
		private IConversationTree conversationTree;

		// Token: 0x04002DBE RID: 11710
		private ConversationState conversationState;
	}
}
