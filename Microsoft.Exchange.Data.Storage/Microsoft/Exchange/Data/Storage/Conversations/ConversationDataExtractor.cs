using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008DB RID: 2267
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationDataExtractor
	{
		// Token: 0x060054A4 RID: 21668 RVA: 0x0015EC94 File Offset: 0x0015CE94
		public ConversationDataExtractor(ItemPartLoader itemPartLoader, bool isIrmEnabled, OptimizationInfo optimizationInfo, bool isSmimeSupported = false, string domainName = null)
		{
			this.isIrmEnabled = isIrmEnabled;
			this.itemPartLoader = itemPartLoader;
			this.optimizationInfo = optimizationInfo;
			this.isSmimeSupported = isSmimeSupported;
			this.domainName = domainName;
			this.treeNodeBodyFragment = new Dictionary<StoreObjectId, KeyValuePair<BodyFragmentInfo, bool>>();
			this.bodySummaryLoadedNodes = new HashSet<StoreObjectId>();
			this.loadStatus = new Dictionary<KeyValuePair<ConversationBodyScanner, StoreObjectId>, ExtractionData>();
			this.loadedItemParts = new Dictionary<StoreObjectId, LoadedItemPart>();
			this.itemParts = new Dictionary<StoreObjectId, ItemPart>();
			this.displayNameToParticipant = new Dictionary<string, Participant>();
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0015ED0E File Offset: 0x0015CF0E
		public bool ConversationDataLoaded
		{
			get
			{
				return this.itemParts.Count > 0;
			}
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0015ED20 File Offset: 0x0015CF20
		private static void DiffItemParts(BodyFragmentInfo parentBodyFragment, LoadedItemPart childItemPart)
		{
			if (childItemPart.UniqueFragmentInfo != null)
			{
				return;
			}
			if (parentBodyFragment != null)
			{
				BodyDiffer bodyDiffer = new BodyDiffer(childItemPart.BodyFragmentInfo, parentBodyFragment);
				childItemPart.UniqueFragmentInfo = bodyDiffer.UniqueBodyPart;
				childItemPart.DisclaimerFragmentInfo = bodyDiffer.DisclaimerPart;
				return;
			}
			childItemPart.UniqueFragmentInfo = childItemPart.BodyFragmentInfo.Trim();
			childItemPart.DisclaimerFragmentInfo = FragmentInfo.Empty;
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x0015ED7B File Offset: 0x0015CF7B
		// (set) Token: 0x060054A8 RID: 21672 RVA: 0x0015ED82 File Offset: 0x0015CF82
		public static long MaxBytesForConversation
		{
			get
			{
				return ConversationDataExtractor.maxBytesForConversation;
			}
			set
			{
				ConversationDataExtractor.maxBytesForConversation = value;
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0015ED8A File Offset: 0x0015CF8A
		public bool HasLoadedItemPart(StoreObjectId itemId)
		{
			return this.loadedItemParts.ContainsKey(itemId);
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0015ED98 File Offset: 0x0015CF98
		public ItemPart GetItemPart(IConversationTree conversationTree, StoreObjectId itemId)
		{
			IStorePropertyBag propertyBag = null;
			ItemPart itemPart = null;
			IConversationTreeNode conversationTreeNode = null;
			if (!conversationTree.TryGetConversationTreeNode(itemId, out conversationTreeNode) || !conversationTreeNode.TryGetPropertyBag(itemId, out propertyBag))
			{
				throw new ArgumentException("No ConversationTreeNode/PropertyBag can be found for the passed StoreObjectId");
			}
			if (!this.itemParts.TryGetValue(itemId, out itemPart))
			{
				this.LoadItemPart(conversationTree, propertyBag);
				itemPart = this.loadedItemParts[itemId];
			}
			if (itemPart is LoadedItemPart && !itemPart.DidLoadSucceed)
			{
				return itemPart;
			}
			if (itemPart.UniqueFragmentInfo == null)
			{
				LoadedItemPart loadedItemPart = (LoadedItemPart)itemPart;
				ExtractionData extractionData;
				if (!this.CanSkipDiffing(conversationTree, loadedItemPart, out extractionData))
				{
					BodyFragmentInfo parentBodyFragment = null;
					if (conversationTreeNode.ParentNode.HasData)
					{
						StoreObjectId mainStoreObjectId = conversationTreeNode.ParentNode.MainStoreObjectId;
						if (this.treeNodeBodyFragment.ContainsKey(mainStoreObjectId))
						{
							parentBodyFragment = this.treeNodeBodyFragment[mainStoreObjectId].Key;
						}
						else
						{
							LoadedItemPart loadedItemPart2 = null;
							if (!this.loadedItemParts.TryGetValue(mainStoreObjectId, out loadedItemPart2))
							{
								IStorePropertyBag propertyBag2;
								if (!conversationTreeNode.ParentNode.TryGetPropertyBag(mainStoreObjectId, out propertyBag2))
								{
									throw new ArgumentException("No Property bag can be found for the passed StoreObjectId on the ParentNode");
								}
								this.LoadItemPart(conversationTree, propertyBag2);
								loadedItemPart2 = this.loadedItemParts[mainStoreObjectId];
							}
							parentBodyFragment = loadedItemPart2.BodyFragmentInfo;
						}
					}
					ConversationDataExtractor.DiffItemParts(parentBodyFragment, loadedItemPart);
				}
				else
				{
					itemPart.UniqueFragmentInfo = extractionData.ChildUniqueBody;
					itemPart.DisclaimerFragmentInfo = extractionData.ChildDisclaimer;
				}
			}
			return itemPart;
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0015EEE5 File Offset: 0x0015D0E5
		public void LoadBodySummaries(IConversationTree conversationTree)
		{
			this.LoadItemPartsOrBodySummaries(conversationTree, null);
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0015EEEF File Offset: 0x0015D0EF
		public void LoadItemParts(IConversationTree conversationTree, ICollection<StoreObjectId> storeIds)
		{
			Util.ThrowOnNullOrEmptyArgument(storeIds, "storeIds");
			Util.ThrowOnNullOrEmptyArgument(conversationTree, "conversationTree");
			this.LoadItemPartsOrBodySummaries(conversationTree, storeIds);
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0015EF18 File Offset: 0x0015D118
		public ParticipantSet CalculateAllRecipients(IConversationTree conversationTree, ICollection<IConversationTreeNode> loadedNodes)
		{
			loadedNodes = (loadedNodes ?? conversationTree);
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>(from node in loadedNodes
			select node.MainStoreObjectId);
			if (!this.ConversationDataLoaded)
			{
				this.LoadItemPartsOrBodySummaries(conversationTree, hashSet);
			}
			ParticipantSet participantSet = new ParticipantSet();
			foreach (StoreObjectId itemId in hashSet)
			{
				ItemPart itemPart = this.GetItemPart(conversationTree, itemId);
				participantSet.UnionWith(itemPart.AllRecipients);
			}
			return participantSet;
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0015EFC0 File Offset: 0x0015D1C0
		public ParticipantTable LoadReplyAllParticipantsPerType(IConversationTree conversationTree)
		{
			return this.LoadReplyAllParticipantsPerTypeImpl(conversationTree, conversationTree, InternalSchema.ReplyAllDisplayNames);
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0015EFD0 File Offset: 0x0015D1D0
		public ParticipantSet LoadReplyAllParticipants(IConversationTree conversationTree, IConversationTreeNode node)
		{
			return this.LoadReplyAllParticipantsImpl(conversationTree, new IConversationTreeNode[]
			{
				node
			}, InternalSchema.ReplyAllDisplayNames);
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0015EFF8 File Offset: 0x0015D1F8
		public List<StoreObjectId> GetMessageIdsForPreread(IConversationTree tree)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(1);
			foreach (IConversationTreeNode conversationTreeNode in tree)
			{
				bool flag = false;
				if (!conversationTreeNode.HasChildren)
				{
					flag = true;
				}
				else if (conversationTreeNode.GetValueOrDefault<byte[]>(ItemSchema.BodyTag, null) == null)
				{
					flag = true;
				}
				if (flag)
				{
					list.Add(conversationTreeNode.MainStoreObjectId);
				}
			}
			return list;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0015F084 File Offset: 0x0015D284
		public bool ConversationNodeContainedInChildren(IConversationTree conversationTree, IConversationTreeNode node)
		{
			if (node.HasChildren)
			{
				this.LoadItemParts(conversationTree, (from n in node.ChildNodes
				select n.MainStoreObjectId).ToList<StoreObjectId>());
			}
			ItemPart itemPart = this.GetItemPart(conversationTree, node.MainStoreObjectId);
			if (itemPart != null && itemPart.Attachments != null && itemPart.Attachments.Count > 0)
			{
				return false;
			}
			foreach (IConversationTreeNode conversationTreeNode in node.ChildNodes)
			{
				if (this.HasLoadedItemPart(conversationTreeNode.MainStoreObjectId))
				{
					LoadedItemPart loadedItemPart = this.GetItemPart(conversationTree, node.MainStoreObjectId) as LoadedItemPart;
					ExtractionData extractionData = null;
					if (loadedItemPart.BodyFragmentInfo != null && this.IsBodyPartPresent(loadedItemPart.BodyFragmentInfo, node.StorePropertyBags[0], true, out extractionData))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0015F28C File Offset: 0x0015D48C
		public Dictionary<IConversationTreeNode, ParticipantSet> LoadAddedParticipants(IConversationTree conversationTree)
		{
			Dictionary<IConversationTreeNode, ParticipantSet> participantsPerNode = new Dictionary<IConversationTreeNode, ParticipantSet>(ConversationTreeNodeBase.EqualityComparer);
			conversationTree.ExecuteSortedAction(ConversationTreeSortOrder.ChronologicalAscending, delegate(ConversationTreeSortOrder param0)
			{
				ParticipantSet participantSet = new ParticipantSet();
				foreach (IConversationTreeNode conversationTreeNode in conversationTree)
				{
					ParticipantSet participantSet2 = this.LoadReplyAllParticipantsImpl(conversationTree, new IConversationTreeNode[]
					{
						conversationTreeNode
					}, InternalSchema.ReplyDisplayNames);
					ParticipantSet participantSet3 = new ParticipantSet();
					foreach (IParticipant participant in participantSet2)
					{
						if (participantSet.Add(participant))
						{
							participantSet3.Add(participant);
						}
					}
					if (participantSet3.Any<IParticipant>())
					{
						participantsPerNode.Add(conversationTreeNode, participantSet3);
					}
					participantSet.UnionWith(this.LoadFrom(conversationTree, conversationTreeNode));
				}
			});
			return participantsPerNode;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060054B3 RID: 21683 RVA: 0x0015F2DC File Offset: 0x0015D4DC
		// (remove) Token: 0x060054B4 RID: 21684 RVA: 0x0015F314 File Offset: 0x0015D514
		public event OnBeforeItemLoadEventDelegate OnBeforeItemLoad;

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x0015F349 File Offset: 0x0015D549
		public OptimizationInfo OptimizationCounters
		{
			get
			{
				return this.optimizationInfo;
			}
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0015F354 File Offset: 0x0015D554
		public override string ToString()
		{
			return "ConversationDataExtractor:IsIrmEnabled=" + this.isIrmEnabled.ToString();
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0015F37C File Offset: 0x0015D57C
		private IEnumerable<IParticipant> LoadFrom(IConversationTree conversationTree, IConversationTreeNode node)
		{
			return this.LoadParticipants(conversationTree, new ConversationDataExtractor.ParticipantLoaderDelegate(this.LoadFromField), new List<IConversationTreeNode>
			{
				node
			});
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0015F3AC File Offset: 0x0015D5AC
		private IDictionary<RecipientItemType, ParticipantSet> LoadReplyAllParticipantField(IConversationTree conversationTree, IStorePropertyBag propertyBag, SmartPropertyDefinition propertyDefinition)
		{
			if (propertyBag.GetValueOrDefault<bool>(InternalSchema.IsDraft, false))
			{
				return new Dictionary<RecipientItemType, ParticipantSet>();
			}
			ParticipantTable replyAllParticipants;
			if (!this.TryGetParticipantsFromReplyAllParticipantsProperty(propertyBag, propertyDefinition, out replyAllParticipants))
			{
				StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
				if (!this.loadedItemParts.ContainsKey(objectId))
				{
					this.optimizationInfo.UpdateItemParticipantNotFound(objectId);
					this.LoadItemPart(conversationTree, propertyBag);
				}
				replyAllParticipants = this.loadedItemParts[objectId].ReplyAllParticipants;
			}
			return replyAllParticipants.ToParticipantSet();
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0015F42C File Offset: 0x0015D62C
		private ParticipantSet LoadParticipants(IConversationTree conversationTree, ConversationDataExtractor.ParticipantLoaderDelegate loaderDelegate, ICollection<IConversationTreeNode> nodes)
		{
			ParticipantSet participantSet = new ParticipantSet();
			nodes = (nodes ?? conversationTree.ToList<IConversationTreeNode>());
			foreach (IConversationTreeNode conversationTreeNode in nodes)
			{
				if (conversationTreeNode.HasData)
				{
					foreach (IStorePropertyBag propertyBag in conversationTreeNode.StorePropertyBags)
					{
						participantSet.UnionWith(loaderDelegate(propertyBag));
					}
				}
			}
			return participantSet;
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0015F4D4 File Offset: 0x0015D6D4
		private ParticipantTable LoadParticipantsPerType(ConversationDataExtractor.ParticipantPerTypeLoaderDelegate loaderDelegate, IConversationTree conversationTree, IEnumerable<IConversationTreeNode> nodes, SmartPropertyDefinition propertyDefinition)
		{
			ParticipantTable participantTable = new ParticipantTable();
			foreach (IConversationTreeNode conversationTreeNode in nodes)
			{
				if (conversationTreeNode.HasData)
				{
					foreach (IStorePropertyBag propertyBag in conversationTreeNode.StorePropertyBags)
					{
						IDictionary<RecipientItemType, ParticipantSet> dictionary = loaderDelegate(conversationTree, propertyBag, propertyDefinition);
						foreach (KeyValuePair<RecipientItemType, ParticipantSet> keyValuePair in dictionary)
						{
							participantTable[keyValuePair.Key].UnionWith(keyValuePair.Value);
						}
					}
				}
			}
			return participantTable;
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0015F5C8 File Offset: 0x0015D7C8
		private List<Participant> LoadFromField(IStorePropertyBag propertyBag)
		{
			List<Participant> list = new List<Participant>();
			Participant participant = propertyBag.TryGetProperty(ItemSchema.From) as Participant;
			if (participant != null)
			{
				list.Add(participant);
			}
			return list;
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0015F600 File Offset: 0x0015D800
		private bool TryExtractRecipients(IStorePropertyBag propertyBag, ParticipantTable recipientTable, IList<Participant> replyToParticipants)
		{
			IList<Participant> list = new List<Participant>();
			if (!this.TryGetParticipantsFromDisplayNameProperty(propertyBag, ItemSchema.DisplayTo, list))
			{
				return false;
			}
			recipientTable.Add(RecipientItemType.To, list.ToArray<Participant>());
			list.Clear();
			if (!this.TryGetParticipantsFromDisplayNameProperty(propertyBag, ItemSchema.DisplayCc, list))
			{
				return false;
			}
			recipientTable.Add(RecipientItemType.Cc, list.ToArray<Participant>());
			list.Clear();
			if (!this.TryGetParticipantsFromDisplayNameProperty(propertyBag, ItemSchema.DisplayBcc, list))
			{
				return false;
			}
			recipientTable.Add(RecipientItemType.Bcc, list.ToArray<Participant>());
			return this.TryGetParticipantsFromDisplayNameProperty(propertyBag, MessageItemSchema.ReplyToNames, replyToParticipants);
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0015F68C File Offset: 0x0015D88C
		private bool CanSkipDiffing(IConversationTree conversationTree, LoadedItemPart loadedItemPart, out ExtractionData extractionData)
		{
			extractionData = null;
			IConversationTreeNode conversationTreeNode;
			if (conversationTree.TryGetConversationTreeNode(loadedItemPart.ObjectId, out conversationTreeNode))
			{
				IConversationTreeNode parentNode = conversationTreeNode.ParentNode;
				return parentNode != null && parentNode.HasData && this.IsBodyPartPresent(loadedItemPart.BodyFragmentInfo, parentNode.StorePropertyBags[0], out extractionData);
			}
			return false;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0015F6E0 File Offset: 0x0015D8E0
		private void AddBodySummary(IConversationTree conversationTree, ref IStorePropertyBag propertyBag, FragmentInfo bodyFragment)
		{
			StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			if (!this.bodySummaryLoadedNodes.Contains(objectId))
			{
				this.bodySummaryLoadedNodes.Add(objectId);
				string summaryText = bodyFragment.GetSummaryText();
				if (!this.SetBodySummary(conversationTree, ref propertyBag, summaryText, objectId))
				{
					this.optimizationInfo.UpdateItemSummaryConstructed(objectId);
				}
			}
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0015F740 File Offset: 0x0015D940
		private void AddBodySummaryError(IConversationTree conversationTree, ref IStorePropertyBag propertyBag)
		{
			StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			if (!this.bodySummaryLoadedNodes.Contains(objectId) && this.loadedItemParts.ContainsKey(objectId))
			{
				if (!this.loadedItemParts[objectId].DidLoadSucceed)
				{
					this.bodySummaryLoadedNodes.Add(objectId);
					PropertyError summary = new PropertyError(InternalSchema.TextBody, PropertyErrorCode.NotEnoughMemory);
					this.SetBodySummary(conversationTree, ref propertyBag, summary, objectId);
					return;
				}
				if (this.InternalHasIrmFailed(objectId))
				{
					this.bodySummaryLoadedNodes.Add(objectId);
					PropertyError summary2 = new PropertyError(InternalSchema.TextBody, PropertyErrorCode.AccessDenied);
					this.SetBodySummary(conversationTree, ref propertyBag, summary2, objectId);
				}
			}
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0015F7E8 File Offset: 0x0015D9E8
		private bool SetBodySummary(IConversationTree conversationTree, ref IStorePropertyBag propertyBag, object summary, StoreObjectId storeId)
		{
			bool result = false;
			QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(propertyBag, ConversationDataExtractor.BodyPropertiesCanBeExtracted, new object[]
			{
				summary,
				summary
			});
			propertyBag = queryResultPropertyBag.AsIStorePropertyBag();
			IConversationTreeNode conversationTreeNode = null;
			conversationTree.TryGetConversationTreeNode(storeId, out conversationTreeNode);
			foreach (StoreObjectId storeObjectId in conversationTreeNode.ToListStoreObjectId())
			{
				if (storeObjectId.Equals(storeId))
				{
					LoadedItemPart loadedItemPart;
					if (this.loadedItemParts.TryGetValue(storeObjectId, out loadedItemPart))
					{
						loadedItemPart.AddBodySummary(summary);
						result = true;
					}
					conversationTreeNode.UpdatePropertyBag(storeObjectId, propertyBag);
				}
			}
			return result;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0015F89C File Offset: 0x0015DA9C
		private bool CanExtractItemPart(IConversationTree conversationTree, IStorePropertyBag propertyBag, out ParticipantTable recipientTable, out List<Participant> replyToParticipants)
		{
			IConversationTreeNode treeNode = null;
			StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			recipientTable = null;
			replyToParticipants = null;
			if (!conversationTree.TryGetConversationTreeNode(objectId, out treeNode))
			{
				throw new ArgumentException("No ConversationTreeNode can be found for the passed StorePropertyBag");
			}
			if (this.itemParts.ContainsKey(objectId))
			{
				return true;
			}
			if (this.OnBeforeItemLoad != null)
			{
				LoadItemEventArgs loadItemEventArgs = new LoadItemEventArgs(treeNode, propertyBag);
				this.OnBeforeItemLoad(this, loadItemEventArgs);
				if (loadItemEventArgs.MessagePropertyDefinitions != null)
				{
					foreach (PropertyDefinition propertyDefinition in loadItemEventArgs.MessagePropertyDefinitions)
					{
						ICollection<PropertyDefinition> requiredPropertyDefinitionsWhenReading = propertyDefinition.RequiredPropertyDefinitionsWhenReading;
						foreach (PropertyDefinition propertyDefinition2 in requiredPropertyDefinitionsWhenReading)
						{
							if (!conversationTree.IsPropertyLoaded(propertyDefinition2) && !ConversationDataExtractor.nonNativePropertiesCanBeExtracted.Contains(propertyDefinition2) && !ConversationDataExtractor.BodyPropertiesCanBeExtracted.Contains(propertyDefinition2))
							{
								this.optimizationInfo.UpdateItemExtraPropertiesNeeded(objectId);
								return false;
							}
						}
					}
				}
			}
			if (true.Equals(propertyBag.TryGetProperty(StoreObjectSchema.IsRestricted)))
			{
				this.optimizationInfo.UpdateItemIrmProtected(objectId);
				return false;
			}
			if (true.Equals(propertyBag.TryGetProperty(InternalSchema.MapiHasAttachment)))
			{
				if (true.Equals(propertyBag.TryGetProperty(ItemSchema.HasAttachment)))
				{
					this.optimizationInfo.UpdateItemAttachmentPresent(objectId);
				}
				this.optimizationInfo.UpdateItemMapiAttachmentPresent(objectId);
				return false;
			}
			recipientTable = new ParticipantTable();
			replyToParticipants = new List<Participant>();
			if (!this.TryExtractRecipients(propertyBag, recipientTable, replyToParticipants))
			{
				this.optimizationInfo.UpdateItemParticipantNotFound(objectId);
				return false;
			}
			return true;
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0015FA58 File Offset: 0x0015DC58
		private void AddRecipientToRecipientDisplayNameCache(LoadedItemPart itemPart)
		{
			foreach (KeyValuePair<string, Participant> keyValuePair in itemPart.DisplayNameToParticipant)
			{
				this.displayNameToParticipant[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0015FAC0 File Offset: 0x0015DCC0
		private void AddRecipientToRecipientDisplayNameCache(ItemPart itemPart)
		{
			IStorePropertyBag storePropertyBag = itemPart.StorePropertyBag;
			Participant participant = storePropertyBag.TryGetProperty(ItemSchema.Sender) as Participant;
			if (participant != null)
			{
				this.AddParticipantToRecipientDisplayNameCache(participant);
			}
			Participant participant2 = storePropertyBag.TryGetProperty(ItemSchema.From) as Participant;
			if (participant2 != null)
			{
				this.AddParticipantToRecipientDisplayNameCache(participant2);
			}
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0015FB16 File Offset: 0x0015DD16
		private void AddParticipantToRecipientDisplayNameCache(Participant participant)
		{
			if (!this.displayNameToParticipant.ContainsKey(participant.DisplayName))
			{
				this.displayNameToParticipant[participant.DisplayName] = participant;
			}
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0015FB40 File Offset: 0x0015DD40
		private bool TryGetParticipantsFromDisplayNameProperty(IStorePropertyBag propertyBag, StorePropertyDefinition displayNamePropertyDefinitions, IList<Participant> participants)
		{
			IList<string> displayNames;
			return Participant.TryGetParticipantsFromDisplayNameProperty(propertyBag, displayNamePropertyDefinitions, out displayNames) && this.TryResolveParticipants(displayNames, participants);
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0015FB64 File Offset: 0x0015DD64
		private bool TryGetParticipantsFromReplyAllParticipantsProperty(IStorePropertyBag propertyBag, SmartPropertyDefinition propertyDefinition, out ParticipantTable participantTable)
		{
			participantTable = new ParticipantTable();
			object obj = propertyBag.TryGetProperty(propertyDefinition);
			if (PropertyError.IsPropertyError(obj))
			{
				return false;
			}
			IDictionary<RecipientItemType, HashSet<string>> dictionary = (IDictionary<RecipientItemType, HashSet<string>>)obj;
			foreach (KeyValuePair<RecipientItemType, HashSet<string>> keyValuePair in dictionary)
			{
				IList<Participant> participants = new List<Participant>();
				if (!this.TryResolveParticipants(keyValuePair.Value, participants))
				{
					return false;
				}
				participantTable.Add(keyValuePair.Key, participants);
			}
			return true;
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0015FBFC File Offset: 0x0015DDFC
		private bool TryResolveParticipants(IEnumerable<string> displayNames, IList<Participant> participants)
		{
			foreach (string key in displayNames)
			{
				Participant item;
				if (!this.displayNameToParticipant.TryGetValue(key, out item))
				{
					return false;
				}
				participants.Add(item);
			}
			return true;
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0015FC60 File Offset: 0x0015DE60
		private bool IsBodyPartPresent(BodyFragmentInfo childBodyFragment, IStorePropertyBag parentPropertyBag, out ExtractionData extractionData)
		{
			return this.IsBodyPartPresent(childBodyFragment, parentPropertyBag, false, out extractionData);
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0015FC6C File Offset: 0x0015DE6C
		public bool IsBodyPartPresent(BodyFragmentInfo childBodyFragment, IStorePropertyBag parentPropertyBag, bool ignoreCache, out ExtractionData extractionData)
		{
			StoreObjectId objectId = ((VersionedId)parentPropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			KeyValuePair<ConversationBodyScanner, StoreObjectId> key = new KeyValuePair<ConversationBodyScanner, StoreObjectId>(childBodyFragment.BodyScanner, objectId);
			if (this.loadStatus.TryGetValue(key, out extractionData))
			{
				bool flag = extractionData != null && extractionData.BodyFragment != null;
				if (flag || !ignoreCache)
				{
					return flag;
				}
			}
			if (!ignoreCache && this.loadedItemParts.ContainsKey(objectId))
			{
				extractionData = new ExtractionData();
			}
			else
			{
				byte[] array = parentPropertyBag.TryGetProperty(ItemSchema.BodyTag) as byte[];
				if (array == null && ignoreCache)
				{
					LoadedItemPart loadedItemPart = null;
					if (this.loadedItemParts.TryGetValue(objectId, out loadedItemPart) && loadedItemPart.BodyFragmentInfo != null && loadedItemPart.BodyFragmentInfo.BodyTag != null)
					{
						array = loadedItemPart.BodyFragmentInfo.BodyTag.ToByteArray();
					}
				}
				this.optimizationInfo.IncrementBodyTagMatchingAttempts();
				if (array != null)
				{
					extractionData = new ExtractionData(childBodyFragment, BodyTagInfo.FromByteArray(array));
					if (extractionData.BodyFragment != null)
					{
						this.loadStatus[key] = extractionData;
						if (!extractionData.IsFormatReliable)
						{
							this.optimizationInfo.UpdateItemBodyFormatMismatched(objectId);
							this.optimizationInfo.IncrementBodyTagMatchingIssues();
						}
					}
					else
					{
						this.optimizationInfo.UpdateItemBodyTagMismatched(objectId);
						this.optimizationInfo.IncrementBodyTagMatchingIssues();
					}
				}
				else
				{
					extractionData = new ExtractionData();
					this.optimizationInfo.UpdateItemBodyTagNotPresent(objectId);
					this.optimizationInfo.IncrementBodyTagMatchingIssues();
				}
			}
			this.loadStatus[key] = extractionData;
			return extractionData.BodyFragment != null;
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0015FDF4 File Offset: 0x0015DFF4
		private void LoadItemPart(IConversationTree conversationTree, IStorePropertyBag propertyBag)
		{
			StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			if (this.loadedItemParts.ContainsKey(objectId))
			{
				return;
			}
			IConversationTreeNode treeNode = null;
			if (!conversationTree.TryGetConversationTreeNode(objectId, out treeNode))
			{
				throw new ArgumentException("No ConversationTreeNode can be found for the passed StorePropertyBag");
			}
			LoadItemEventArgs loadItemEventArgs = new LoadItemEventArgs(treeNode, propertyBag);
			PropertyDefinition[] array = null;
			if (this.OnBeforeItemLoad != null)
			{
				this.OnBeforeItemLoad(this, loadItemEventArgs);
				if (loadItemEventArgs.OpportunisticLoadPropertyDefinitions != null && loadItemEventArgs.MessagePropertyDefinitions != null)
				{
					array = new PropertyDefinition[loadItemEventArgs.OpportunisticLoadPropertyDefinitions.Length + loadItemEventArgs.MessagePropertyDefinitions.Length];
					loadItemEventArgs.MessagePropertyDefinitions.CopyTo(array, 0);
					loadItemEventArgs.OpportunisticLoadPropertyDefinitions.CopyTo(array, loadItemEventArgs.MessagePropertyDefinitions.Length);
				}
				else if (loadItemEventArgs.OpportunisticLoadPropertyDefinitions != null)
				{
					array = loadItemEventArgs.OpportunisticLoadPropertyDefinitions;
				}
				else
				{
					array = loadItemEventArgs.MessagePropertyDefinitions;
				}
			}
			LoadedItemPart loadedItemPart = this.itemPartLoader.Load(propertyBag[ItemSchema.Id] as StoreId, propertyBag, loadItemEventArgs.HtmlStreamOptionCallback, array, this.bytesLoadedForConversation, this.isSmimeSupported, this.domainName);
			if (loadedItemPart.DidLoadSucceed)
			{
				this.bytesLoadedForConversation += loadedItemPart.BytesLoaded;
			}
			this.loadedItemParts.Add(objectId, loadedItemPart);
			this.AddRecipientToRecipientDisplayNameCache(this.loadedItemParts[objectId]);
			this.optimizationInfo.UpdateItemOpened(objectId);
			if (propertyBag.TryGetProperty(InternalSchema.MapiHasAttachment).Equals(true))
			{
				this.optimizationInfo.UpdateItemMapiAttachmentPresent(objectId);
				if (propertyBag.TryGetProperty(ItemSchema.HasAttachment).Equals(true))
				{
					this.optimizationInfo.UpdateItemAttachmentPresent(objectId);
				}
			}
			if (!this.itemParts.ContainsKey(objectId))
			{
				this.itemParts.Add(objectId, this.loadedItemParts[objectId]);
				return;
			}
			this.loadedItemParts[objectId].UniqueFragmentInfo = this.itemParts[objectId].UniqueFragmentInfo;
			this.loadedItemParts[objectId].DisclaimerFragmentInfo = this.itemParts[objectId].DisclaimerFragmentInfo;
			this.itemParts[objectId] = this.loadedItemParts[objectId];
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0016032C File Offset: 0x0015E52C
		private void LoadItemPartsOrBodySummaries(IConversationTree conversationTree, ICollection<StoreObjectId> storeIds)
		{
			conversationTree.ExecuteSortedAction(ConversationTreeSortOrder.ChronologicalDescending, delegate(ConversationTreeSortOrder param0)
			{
				HashSet<StoreObjectId> hashSet = null;
				if (storeIds != null && storeIds.Any<StoreObjectId>())
				{
					hashSet = new HashSet<StoreObjectId>(storeIds);
				}
				this.RecursiveLoadBodyFragments(conversationTree, hashSet);
				foreach (IConversationTreeNode conversationTreeNode in conversationTree)
				{
					if (!this.treeNodeBodyFragment.ContainsKey(conversationTreeNode.MainStoreObjectId))
					{
						for (int i = 0; i < conversationTreeNode.StorePropertyBags.Count; i++)
						{
							IStorePropertyBag storePropertyBag = conversationTreeNode.StorePropertyBags[i];
							this.AddBodySummaryError(conversationTree, ref storePropertyBag);
						}
					}
					else
					{
						FragmentInfo fragmentInfo = FragmentInfo.Empty;
						FragmentInfo disclaimerFragment = FragmentInfo.Empty;
						BodyFragmentInfo bodyFragmentInfo = this.treeNodeBodyFragment[conversationTreeNode.MainStoreObjectId].Key;
						fragmentInfo = bodyFragmentInfo;
						if (conversationTreeNode.ParentNode.HasData && this.treeNodeBodyFragment.ContainsKey(conversationTreeNode.ParentNode.MainStoreObjectId))
						{
							BodyFragmentInfo key = this.treeNodeBodyFragment[conversationTreeNode.ParentNode.MainStoreObjectId].Key;
							ExtractionData extractionData;
							if (this.IsBodyPartPresent(bodyFragmentInfo, conversationTreeNode.ParentNode.StorePropertyBags[0], out extractionData))
							{
								fragmentInfo = extractionData.ChildUniqueBody;
								disclaimerFragment = extractionData.ChildDisclaimer;
							}
							else
							{
								BodyDiffer bodyDiffer = new BodyDiffer(bodyFragmentInfo, key);
								fragmentInfo = bodyDiffer.UniqueBodyPart;
								disclaimerFragment = bodyDiffer.DisclaimerPart;
							}
						}
						for (int j = 0; j < conversationTreeNode.StorePropertyBags.Count; j++)
						{
							IStorePropertyBag storePropertyBag2 = conversationTreeNode.StorePropertyBags[j];
							StoreObjectId objectId = ((VersionedId)storePropertyBag2.TryGetProperty(ItemSchema.Id)).ObjectId;
							this.AddBodySummary(conversationTree, ref storePropertyBag2, fragmentInfo);
							if (!conversationTreeNode.HasChildren)
							{
								this.OptimizationCounters.UpdateItemIsLeafNode(objectId);
							}
							if (!this.itemParts.ContainsKey(objectId))
							{
								ParticipantTable recipients;
								List<Participant> replyToParticipants;
								if (this.treeNodeBodyFragment[conversationTreeNode.MainStoreObjectId].Value && this.CanExtractItemPart(conversationTree, storePropertyBag2, out recipients, out replyToParticipants))
								{
									this.optimizationInfo.UpdateItemExtracted(objectId);
									ItemPart itemPart = this.itemPartLoader.Create(objectId, storePropertyBag2, fragmentInfo, disclaimerFragment, recipients, replyToParticipants);
									this.itemParts.Add(objectId, itemPart);
									this.AddRecipientToRecipientDisplayNameCache(itemPart);
								}
								else if (hashSet != null && hashSet.Contains(objectId))
								{
									this.LoadItemPart(conversationTree, storePropertyBag2);
									bodyFragmentInfo = this.loadedItemParts[objectId].BodyFragmentInfo;
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0016036C File Offset: 0x0015E56C
		private void RecursiveLoadBodyFragments(IConversationTree conversationTree, ICollection<StoreObjectId> itemPartsToLoad)
		{
			foreach (IConversationTreeNode treeNode in conversationTree)
			{
				bool flag;
				this.RecursiveLoadBodyFragments(conversationTree, treeNode, itemPartsToLoad, out flag);
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x001603B8 File Offset: 0x0015E5B8
		private void RecursiveLoadBodyFragments(IConversationTree conversationTree, IConversationTreeNode treeNode, ICollection<StoreObjectId> itemPartsToLoad, out bool forceParentLoadBodyFragment)
		{
			IStorePropertyBag storePropertyBag = null;
			StoreObjectId storeObjectId = null;
			forceParentLoadBodyFragment = false;
			if (treeNode.HasData)
			{
				foreach (IStorePropertyBag storePropertyBag2 in treeNode.StorePropertyBags)
				{
					StoreObjectId objectId = ((VersionedId)storePropertyBag2.TryGetProperty(ItemSchema.Id)).ObjectId;
					storePropertyBag = storePropertyBag2;
					storeObjectId = objectId;
					if (itemPartsToLoad == null || itemPartsToLoad.Contains(objectId))
					{
						forceParentLoadBodyFragment = true;
						break;
					}
				}
			}
			BodyFragmentInfo bodyFragmentInfo = null;
			bool flag = false;
			bool flag2 = false;
			foreach (IConversationTreeNode conversationTreeNode in treeNode.ChildNodes)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				bool flag3;
				this.RecursiveLoadBodyFragments(conversationTree, conversationTreeNode2, itemPartsToLoad, out flag3);
				flag2 = (flag2 || flag3);
				if (storePropertyBag != null)
				{
					if (this.itemParts.ContainsKey(storeObjectId))
					{
						flag = true;
						bodyFragmentInfo = (this.treeNodeBodyFragment.ContainsKey(treeNode.MainStoreObjectId) ? this.treeNodeBodyFragment[treeNode.MainStoreObjectId].Key : null);
					}
					else if (this.treeNodeBodyFragment.ContainsKey(conversationTreeNode2.MainStoreObjectId))
					{
						ExtractionData extractionData;
						this.IsBodyPartPresent(this.treeNodeBodyFragment[conversationTreeNode2.MainStoreObjectId].Key, storePropertyBag, out extractionData);
						if (extractionData != null && extractionData.BodyFragment != null)
						{
							if (!flag && extractionData.IsFormatReliable)
							{
								bodyFragmentInfo = extractionData.BodyFragment;
								flag = true;
							}
							else if (bodyFragmentInfo == null)
							{
								bodyFragmentInfo = extractionData.BodyFragment;
							}
						}
					}
				}
			}
			if (!treeNode.HasData)
			{
				return;
			}
			bool flag4 = false;
			if (itemPartsToLoad != null && itemPartsToLoad.Contains(storeObjectId) && !flag)
			{
				flag4 = true;
			}
			else if (bodyFragmentInfo == null && (forceParentLoadBodyFragment || flag2))
			{
				flag4 = true;
			}
			if (flag4 && !this.loadedItemParts.ContainsKey(storeObjectId))
			{
				this.LoadItemPart(conversationTree, storePropertyBag);
			}
			if (this.loadedItemParts.ContainsKey(storeObjectId))
			{
				LoadedItemPart loadedItemPart = this.loadedItemParts[storeObjectId];
				if (this.InternalHasIrmFailed(storeObjectId))
				{
					bodyFragmentInfo = null;
				}
				else
				{
					bodyFragmentInfo = loadedItemPart.BodyFragmentInfo;
					flag = true;
				}
			}
			if (bodyFragmentInfo != null)
			{
				this.treeNodeBodyFragment[treeNode.MainStoreObjectId] = new KeyValuePair<BodyFragmentInfo, bool>(bodyFragmentInfo, flag);
			}
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00160600 File Offset: 0x0015E800
		private ParticipantSet LoadReplyAllParticipantsImpl(IConversationTree conversationTree, ICollection<IConversationTreeNode> nodes, SmartPropertyDefinition propertyDefinition)
		{
			ParticipantSet participantSet = new ParticipantSet();
			ParticipantTable participantTable = this.LoadReplyAllParticipantsPerTypeImpl(conversationTree, nodes, propertyDefinition);
			participantSet.UnionWith(participantTable[RecipientItemType.To]);
			participantSet.UnionWith(participantTable[RecipientItemType.Cc]);
			return participantSet;
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00160638 File Offset: 0x0015E838
		private ParticipantTable LoadReplyAllParticipantsPerTypeImpl(IConversationTree conversationTree, ICollection<IConversationTreeNode> nodes, SmartPropertyDefinition propertyDefinition)
		{
			ParticipantTable participantTable = this.LoadParticipantsPerType(new ConversationDataExtractor.ParticipantPerTypeLoaderDelegate(this.LoadReplyAllParticipantField), conversationTree, nodes, propertyDefinition);
			participantTable[RecipientItemType.Cc].ExceptWith(participantTable[RecipientItemType.To]);
			return participantTable;
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00160670 File Offset: 0x0015E870
		private bool InternalHasIrmFailed(StoreObjectId storeId)
		{
			return this.isIrmEnabled && this.loadedItemParts[storeId].IrmInfo.IsRestricted && this.loadedItemParts[storeId].IrmInfo.DecryptionStatus.Failed;
		}

		// Token: 0x04002DA4 RID: 11684
		public static readonly NativeStorePropertyDefinition[] BodyPropertiesCanBeExtracted = new NativeStorePropertyDefinition[]
		{
			InternalSchema.TextBody,
			InternalSchema.Preview
		};

		// Token: 0x04002DA5 RID: 11685
		private static readonly PropertyDefinition[] nonNativePropertiesCanBeExtracted = new PropertyDefinition[]
		{
			InternalSchema.MapiReplyToNames,
			InternalSchema.MapiReplyToBlob
		};

		// Token: 0x04002DA6 RID: 11686
		private readonly ItemPartLoader itemPartLoader;

		// Token: 0x04002DA7 RID: 11687
		private readonly Dictionary<string, Participant> displayNameToParticipant;

		// Token: 0x04002DA8 RID: 11688
		private readonly Dictionary<StoreObjectId, ItemPart> itemParts;

		// Token: 0x04002DA9 RID: 11689
		private readonly Dictionary<StoreObjectId, LoadedItemPart> loadedItemParts;

		// Token: 0x04002DAA RID: 11690
		private readonly Dictionary<KeyValuePair<ConversationBodyScanner, StoreObjectId>, ExtractionData> loadStatus;

		// Token: 0x04002DAB RID: 11691
		private readonly OptimizationInfo optimizationInfo;

		// Token: 0x04002DAC RID: 11692
		private readonly HashSet<StoreObjectId> bodySummaryLoadedNodes;

		// Token: 0x04002DAD RID: 11693
		private readonly Dictionary<StoreObjectId, KeyValuePair<BodyFragmentInfo, bool>> treeNodeBodyFragment;

		// Token: 0x04002DAE RID: 11694
		private readonly bool isIrmEnabled;

		// Token: 0x04002DAF RID: 11695
		private readonly bool isSmimeSupported;

		// Token: 0x04002DB0 RID: 11696
		private readonly string domainName;

		// Token: 0x04002DB1 RID: 11697
		private static long maxBytesForConversation = 12582912L;

		// Token: 0x04002DB2 RID: 11698
		private long bytesLoadedForConversation;

		// Token: 0x020008DC RID: 2268
		// (Invoke) Token: 0x060054D5 RID: 21717
		private delegate List<Participant> ParticipantLoaderDelegate(IStorePropertyBag propertyBag);

		// Token: 0x020008DD RID: 2269
		// (Invoke) Token: 0x060054D9 RID: 21721
		private delegate IDictionary<RecipientItemType, ParticipantSet> ParticipantPerTypeLoaderDelegate(IConversationTree conversationTree, IStorePropertyBag propertyBag, SmartPropertyDefinition propertyDefinition);
	}
}
