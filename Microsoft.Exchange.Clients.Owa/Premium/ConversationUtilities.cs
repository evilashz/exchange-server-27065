using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004ED RID: 1261
	internal static class ConversationUtilities
	{
		// Token: 0x06002FEA RID: 12266 RVA: 0x001179FC File Offset: 0x00115BFC
		public static ExDateTime? GetLastDeliveryTime(Conversation conversation)
		{
			MinMaxAggregatorRule<ExDateTime> minMaxAggregatorRule = new MinMaxAggregatorRule<ExDateTime>(ItemSchema.ReceivedTime, false, ExDateTime.MinValue);
			conversation.AggregateHeaders(new IAggregatorRule[]
			{
				minMaxAggregatorRule
			});
			if (minMaxAggregatorRule.Result == ExDateTime.MinValue)
			{
				return null;
			}
			return new ExDateTime?(minMaxAggregatorRule.Result);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00117A54 File Offset: 0x00115C54
		public static bool IsSmsConversation(Conversation conversation)
		{
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				using (List<IStorePropertyBag>.Enumerator enumerator2 = conversationTreeNode2.StorePropertyBags.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						IStorePropertyBag propertyBag = enumerator2.Current;
						string property = ItemUtility.GetProperty<string>(propertyBag, StoreObjectSchema.ItemClass, null);
						if (ObjectClass.IsSmsMessage(property))
						{
							return true;
						}
						return false;
					}
				}
			}
			return false;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00117B00 File Offset: 0x00115D00
		public static string GenerateSmsConversationTitle(StoreObjectId sentItemsFolderId, Conversation conversation)
		{
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				if (conversationTreeNode2.ParentNode != null)
				{
					using (List<IStorePropertyBag>.Enumerator enumerator2 = conversationTreeNode2.StorePropertyBags.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							IStorePropertyBag propertyBag = enumerator2.Current;
							StoreObjectId property = ItemUtility.GetProperty<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, null);
							string text = string.Empty;
							if (!sentItemsFolderId.Equals(property))
							{
								text = ItemUtility.GetProperty<string>(propertyBag, ItemSchema.SentRepresentingDisplayName, null);
							}
							else
							{
								text = ItemUtility.GetProperty<string>(propertyBag, ItemSchema.DisplayTo, null);
							}
							return Strings.SmsConversationTitle(text);
						}
					}
				}
			}
			return conversation.Topic;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00117BE8 File Offset: 0x00115DE8
		public static StoreObjectId MapConversationToItem(UserContext userContext, ConversationId conversationId, OwaStoreObjectId localFolderId)
		{
			MailboxSession session = (MailboxSession)localFolderId.GetSession(userContext);
			Conversation conversation = Conversation.Load(session, conversationId, new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.ReceivedTime,
				StoreObjectSchema.ParentItemId
			});
			StoreObjectId result = null;
			ExDateTime t = ExDateTime.MinValue;
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag propertyBag in conversationTreeNode2.StorePropertyBags)
				{
					if (conversationTreeNode2.ParentNode != null)
					{
						ExDateTime property = ItemUtility.GetProperty<ExDateTime>(propertyBag, ItemSchema.ReceivedTime, ExDateTime.MinValue);
						StoreObjectId property2 = ItemUtility.GetProperty<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, null);
						if (property2.Equals(localFolderId.StoreObjectId) && property > t)
						{
							VersionedId property3 = ItemUtility.GetProperty<VersionedId>(propertyBag, ItemSchema.Id, null);
							result = property3.ObjectId;
							t = property;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00117D20 File Offset: 0x00115F20
		public static ConversationId MapItemToConversation(UserContext userContext, OwaStoreObjectId itemId)
		{
			ConversationId result;
			using (Item item = Utilities.GetItem<Item>(itemId.GetSession(userContext), itemId.StoreObjectId, false, new PropertyDefinition[]
			{
				ItemSchema.ConversationId,
				StoreObjectSchema.ItemClass
			}))
			{
				string property = ItemUtility.GetProperty<string>(item, StoreObjectSchema.ItemClass, null);
				if (ConversationUtilities.IsConversationExcludedType(property))
				{
					result = null;
				}
				else
				{
					result = ItemUtility.GetProperty<ConversationId>(item, ItemSchema.ConversationId, null);
				}
			}
			return result;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00117D9C File Offset: 0x00115F9C
		public static bool IsConversationExcludedType(string itemClass)
		{
			return ConversationUtilities.conversationExcludedTypes.Contains(itemClass);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00117DAC File Offset: 0x00115FAC
		internal static bool ContainsConversationItem(UserContext userContext, OwaStoreObjectId[] ids)
		{
			foreach (OwaStoreObjectId owaStoreObjectId in ids)
			{
				if (owaStoreObjectId.IsConversationId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x00117DDC File Offset: 0x00115FDC
		internal static Conversation LoadConversation(MailboxSession mailboxSession, OwaStoreObjectId owaConversationId, params PropertyDefinition[] requestedProperties)
		{
			return Conversation.Load(mailboxSession, owaConversationId.ConversationId, ConversationUtilities.GetExcludedFolderIds(mailboxSession, owaConversationId.ParentFolderId), requestedProperties);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00117DF8 File Offset: 0x00115FF8
		internal static Conversation LoadConversation(UserContext userContext, OwaStoreObjectId owaConversationId, params PropertyDefinition[] requestedProperties)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (owaConversationId == null)
			{
				throw new ArgumentNullException("owaConversationId");
			}
			if (!owaConversationId.IsConversationId)
			{
				throw new ArgumentException("owaConversationId");
			}
			MailboxSession session = (MailboxSession)owaConversationId.GetSession(userContext);
			return Conversation.Load(session, owaConversationId.ConversationId, ConversationUtilities.GetExcludedFolderIds(session, owaConversationId.ParentFolderId), userContext.IsIrmEnabled, requestedProperties);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00117E60 File Offset: 0x00116060
		internal static OwaStoreObjectId[] GetAllItemIds(UserContext userContext, OwaStoreObjectId[] convIds, params StoreObjectId[] excludedFolderIds)
		{
			return ConversationUtilities.GetAllItemIds(userContext, convIds, null, null, excludedFolderIds);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00117E6C File Offset: 0x0011606C
		internal static OwaStoreObjectId[] GetAllItemIds(UserContext userContext, OwaStoreObjectId[] convIds, PropertyDefinition[] propertiesForFilter, Func<IStorePropertyBag, bool> filter, params StoreObjectId[] excludedFolderIds)
		{
			List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
			foreach (OwaStoreObjectId owaStoreObjectId in convIds)
			{
				PropertyDefinition[] array = new PropertyDefinition[(propertiesForFilter == null) ? 1 : (propertiesForFilter.Length + 1)];
				array[0] = StoreObjectSchema.ParentItemId;
				if (propertiesForFilter != null)
				{
					propertiesForFilter.CopyTo(array, 1);
				}
				Conversation conversation = ConversationUtilities.LoadConversation(userContext, owaStoreObjectId, array);
				list.AddRange(ConversationUtilities.GetAllItemIds(owaStoreObjectId, conversation, filter, excludedFolderIds));
			}
			return list.ToArray();
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00117EE0 File Offset: 0x001160E0
		private static IList<OwaStoreObjectId> GetAllItemIds(OwaStoreObjectId relatedId, Conversation conversation, Func<IStorePropertyBag, bool> filter, params StoreObjectId[] excludedFolderIds)
		{
			List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode2.StorePropertyBags)
				{
					StoreObjectId o = (StoreObjectId)storePropertyBag.TryGetProperty(StoreObjectSchema.ParentItemId);
					if (excludedFolderIds != null)
					{
						bool flag = false;
						foreach (StoreObjectId storeObjectId in excludedFolderIds)
						{
							if (storeObjectId.CompareTo(o) == 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
					if (filter == null || filter(storePropertyBag))
					{
						VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
						if (versionedId != null)
						{
							list.Add(OwaStoreObjectId.CreateFromStoreObjectId(versionedId.ObjectId, relatedId));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00117FFC File Offset: 0x001161FC
		internal static OwaStoreObjectId[] GetLocalItemIds(UserContext userContext, OwaStoreObjectId[] convIds, OwaStoreObjectId localFolderId)
		{
			return ConversationUtilities.GetLocalItemIds(userContext, convIds, localFolderId, null, null);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00118008 File Offset: 0x00116208
		internal static OwaStoreObjectId[] GetLocalItemIds(UserContext userContext, OwaStoreObjectId[] convIds, OwaStoreObjectId localFolderId, PropertyDefinition[] propertiesForFilter, Func<IStorePropertyBag, bool> filter)
		{
			List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
			StoreObjectId storeObjectId = (localFolderId != null) ? localFolderId.StoreObjectId : null;
			int i = 0;
			while (i < convIds.Length)
			{
				OwaStoreObjectId owaStoreObjectId = convIds[i];
				if (owaStoreObjectId.IsConversationId)
				{
					MailboxSession session = (MailboxSession)owaStoreObjectId.GetSession(userContext);
					PropertyDefinition[] array = new PropertyDefinition[(propertiesForFilter == null) ? 1 : (propertiesForFilter.Length + 1)];
					array[0] = StoreObjectSchema.ParentItemId;
					if (propertiesForFilter != null)
					{
						propertiesForFilter.CopyTo(array, 1);
					}
					Conversation conversation = Conversation.Load(session, owaStoreObjectId.ConversationId, array);
					using (List<StoreObjectId>.Enumerator enumerator = ConversationUtilities.GetLocalItemIds(session, conversation, storeObjectId ?? owaStoreObjectId.ParentFolderId, filter).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StoreObjectId storeObjectId2 = enumerator.Current;
							list.Add(OwaStoreObjectId.CreateFromStoreObjectId(storeObjectId2, owaStoreObjectId));
						}
						goto IL_C8;
					}
					goto IL_C1;
				}
				goto IL_C1;
				IL_C8:
				i++;
				continue;
				IL_C1:
				list.Add(owaStoreObjectId);
				goto IL_C8;
			}
			return list.ToArray();
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00118104 File Offset: 0x00116304
		internal static List<StoreObjectId> GetLocalItemIds(MailboxSession session, Conversation conversation, StoreObjectId localFolderId)
		{
			return ConversationUtilities.GetLocalItemIds(session, conversation, localFolderId, null);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00118110 File Offset: 0x00116310
		private static List<StoreObjectId> GetLocalItemIds(MailboxSession session, Conversation conversation, StoreObjectId localFolderId, Func<IStorePropertyBag, bool> filter)
		{
			List<StoreObjectId> localItemIdsInParentFolder = ConversationUtilities.GetLocalItemIdsInParentFolder(conversation, localFolderId, null);
			if (localItemIdsInParentFolder.Count == 0)
			{
				using (Folder folder = Folder.Bind(session, localFolderId))
				{
					SearchFolder searchFolder = folder as SearchFolder;
					if (searchFolder != null)
					{
						return ConversationUtilities.GetLocalItemIdsForSearchFolder(conversation, searchFolder, filter);
					}
				}
				return localItemIdsInParentFolder;
			}
			return localItemIdsInParentFolder;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0011816C File Offset: 0x0011636C
		internal static List<StoreObjectId> GetLocalItemIds(Conversation conversation, Folder folder)
		{
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder != null)
			{
				return ConversationUtilities.GetLocalItemIdsForSearchFolder(conversation, searchFolder, null);
			}
			return ConversationUtilities.GetLocalItemIdsInParentFolder(conversation, folder.Id.ObjectId, null);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x001181A0 File Offset: 0x001163A0
		private static List<StoreObjectId> GetLocalItemIdsInParentFolder(Conversation conversation, StoreObjectId localFolderId, Func<IStorePropertyBag, bool> filter)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode2.StorePropertyBags)
				{
					StoreObjectId o = (StoreObjectId)storePropertyBag.TryGetProperty(StoreObjectSchema.ParentItemId);
					if ((localFolderId == null || localFolderId.CompareTo(o) == 0) && (filter == null || !filter(storePropertyBag)))
					{
						VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
						if (versionedId != null)
						{
							list.Add(versionedId.ObjectId);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x00118288 File Offset: 0x00116488
		private static List<StoreObjectId> GetLocalItemIdsForSearchFolder(Conversation conversation, SearchFolder searchFolder, Func<IStorePropertyBag, bool> filter)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>();
			using (QueryResult queryResult = searchFolder.ConversationItemQuery(null, new SortBy[]
			{
				new SortBy(ConversationItemSchema.ConversationId, SortOrder.Ascending)
			}, new PropertyDefinition[]
			{
				ConversationItemSchema.ConversationItemIds
			}))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ConversationItemSchema.ConversationId, conversation.ConversationId)))
				{
					StoreObjectId[] array = queryResult.GetRows(1)[0][0] as StoreObjectId[];
					if (array != null)
					{
						foreach (StoreObjectId item in array)
						{
							hashSet.Add(item);
						}
					}
				}
			}
			List<StoreObjectId> list2 = new List<StoreObjectId>();
			foreach (StoreObjectId storeObjectId in hashSet)
			{
				IConversationTreeNode conversationTreeNode = null;
				if (!conversation.ConversationTree.TryGetConversationTreeNode(storeObjectId, out conversationTreeNode))
				{
					list2.Add(storeObjectId);
				}
			}
			foreach (StoreObjectId item2 in list2)
			{
				hashSet.Remove(item2);
			}
			if (filter != null)
			{
				using (IEnumerator<IConversationTreeNode> enumerator3 = conversation.ConversationTree.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						IConversationTreeNode conversationTreeNode2 = enumerator3.Current;
						ConversationTreeNode conversationTreeNode3 = (ConversationTreeNode)conversationTreeNode2;
						foreach (IStorePropertyBag storePropertyBag in conversationTreeNode3.StorePropertyBags)
						{
							StoreObjectId objectId = ItemUtility.GetProperty<VersionedId>(storePropertyBag, ItemSchema.Id, null).ObjectId;
							if (hashSet.Contains(objectId) && filter(storePropertyBag))
							{
								list.Add(objectId);
							}
						}
					}
					return list;
				}
			}
			list.AddRange(hashSet);
			return list;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x001184A0 File Offset: 0x001166A0
		internal static void MarkLocalNodes(Conversation conversation, List<StoreObjectId> localItemIds)
		{
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				if (ConversationUtilities.localTreeNodes.ContainsKey(conversationTreeNode2))
				{
					throw new OwaInvalidOperationException("You cannot call MarkLocalNodes twice for the same conversation.");
				}
				bool value = false;
				foreach (IStorePropertyBag propertyBag in conversationTreeNode2.StorePropertyBags)
				{
					VersionedId property = ItemUtility.GetProperty<VersionedId>(propertyBag, ItemSchema.Id, null);
					if (property != null && localItemIds.Contains(property.ObjectId))
					{
						value = true;
					}
				}
				ConversationUtilities.localTreeNodes[conversationTreeNode2] = value;
			}
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00118578 File Offset: 0x00116778
		internal static bool IsLocalNode(IConversationTreeNode node)
		{
			if (!ConversationUtilities.localTreeNodes.ContainsKey(node))
			{
				throw new OwaInvalidOperationException("You must call MarkLocalNodes before calling IsLocalNode.");
			}
			return ConversationUtilities.localTreeNodes[node];
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x001185A0 File Offset: 0x001167A0
		internal static IList<StoreObjectId> GetExcludedFolderIds(MailboxSession session, StoreObjectId parentFolderId)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(1);
			if (ConversationUtilities.HideDeletedItems)
			{
				StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(session, DefaultFolderType.DeletedItems);
				if (!storeObjectId.Equals(parentFolderId))
				{
					list.Add(storeObjectId);
				}
			}
			return list;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x001185D4 File Offset: 0x001167D4
		internal static IList<StoreObjectId> ExcludeFolders(IList<StoreObjectId> itemIds, IList<StoreObjectId> excludedFolderIds)
		{
			if (excludedFolderIds.Count == 0)
			{
				return itemIds;
			}
			List<StoreObjectId> list = new List<StoreObjectId>(itemIds.Count);
			for (int i = 0; i < itemIds.Count; i++)
			{
				bool flag = false;
				StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(itemIds[i]);
				for (int j = 0; j < excludedFolderIds.Count; j++)
				{
					if (parentIdFromMessageId.Equals(excludedFolderIds[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(itemIds[i]);
				}
			}
			return list;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x00118654 File Offset: 0x00116854
		internal static StoreObjectId[] GetPrereadItemIds(MailboxSession session, OwaStoreObjectId[] convIds)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			for (int i = 0; i < convIds.Length; i++)
			{
				Conversation conversation = Conversation.Load(session, convIds[i].ConversationId, new PropertyDefinition[]
				{
					ItemSchema.Id
				});
				List<StoreObjectId> messageIdsForPreread = conversation.GetMessageIdsForPreread();
				list.AddRange(messageIdsForPreread);
			}
			return list.ToArray();
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x001186AC File Offset: 0x001168AC
		internal static void RenderItemParts(TextWriter writer, UserContext userContext, OwaStoreObjectId owaConversationId, Conversation conversation, OwaStoreObjectId[] expandedIds, int[] expandedInternetMIds, List<StoreObjectId> localItemIds, string searchWords)
		{
			ConversationUtilities.RenderItemParts(writer, userContext, owaConversationId, conversation, expandedIds, expandedInternetMIds, localItemIds, searchWords, true);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x001186CC File Offset: 0x001168CC
		internal static void RenderItemParts(TextWriter writer, UserContext userContext, OwaStoreObjectId owaConversationId, Conversation conversation, OwaStoreObjectId[] expandedIds, int[] expandedInternetMIds, List<StoreObjectId> localItemIds, string searchWords, bool shouldRenderSelected)
		{
			if (conversation.ConversationTree.Count == 0)
			{
				return;
			}
			ConversationSortOrder conversationSortOrder = userContext.UserOptions.ConversationSortOrder;
			conversation.ConversationTree.Sort(ConversationUtilities.GetConversationTreeSortOrder(conversationSortOrder));
			List<ConversationTreeNode> list = new List<ConversationTreeNode>(0);
			MailboxSession session = (MailboxSession)owaConversationId.GetSession(userContext);
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				if (conversationTreeNode2.ParentNode != null)
				{
					list.Add(conversationTreeNode2);
					ConversationUtilities.SortPropertyBags(conversationTreeNode2, localItemIds, session);
				}
			}
			IConversationTreeNode conversationTreeNode3 = null;
			List<IConversationTreeNode> list2 = null;
			conversation.LoadBodySummaries();
			if (string.IsNullOrEmpty(searchWords))
			{
				ConversationUtilities.GetNodesToExpand(userContext, owaConversationId, conversation, owaConversationId.ParentFolderId, expandedIds, expandedInternetMIds, out list2, out conversationTreeNode3);
				conversation.LoadItemParts(list2);
				ConversationUtilities.LogConversationPerformanceData(conversation);
			}
			else
			{
				if (localItemIds.Count == 0)
				{
					return;
				}
				conversation.LoadItemParts(localItemIds, searchWords, userContext.UserCulture, out list2);
			}
			ConversationUtilities.MarkLocalNodes(conversation, localItemIds);
			int num = 0;
			for (int i = 0; i < list.Count; i++)
			{
				ConversationTreeNode conversationTreeNode4 = list[i];
				ConversationTreeNode conversationTreeNode5;
				if ((conversationSortOrder & ConversationSortOrder.NewestOnTop) == (ConversationSortOrder)0)
				{
					conversationTreeNode5 = ((i >= 1) ? list[i - 1] : null);
				}
				else
				{
					conversationTreeNode5 = ((i <= list.Count - 2) ? list[i + 1] : null);
				}
				bool isExpanded = list2.Contains(conversationTreeNode4);
				bool flag = conversationTreeNode5 != null && conversationTreeNode4.ParentNode != null && conversationTreeNode4.ParentNode.StorePropertyBags != null && conversationTreeNode5 != conversationTreeNode4.ParentNode;
				if (flag && !ConversationUtilities.ShouldRenderItem(conversationTreeNode4.ParentNode, session))
				{
					flag = false;
				}
				string elementId = "divIP" + num.ToString();
				bool isSelected = shouldRenderSelected && conversationTreeNode4 == conversationTreeNode3;
				bool flag2;
				using (ItemPartWriter writer2 = ItemPartWriter.GetWriter(userContext, writer, owaConversationId, conversation, conversationTreeNode4))
				{
					flag2 = writer2.Render(elementId, isExpanded, flag, isSelected);
				}
				if (flag2)
				{
					num++;
				}
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x001188E8 File Offset: 0x00116AE8
		internal static ConversationTreeSortOrder GetConversationTreeSortOrder(ConversationSortOrder sortOrder)
		{
			ConversationTreeSortOrder result;
			if (sortOrder != ConversationSortOrder.ChronologicalNewestOnTop)
			{
				switch (sortOrder)
				{
				case ConversationSortOrder.ChronologicalNewestOnBottom:
					result = ConversationTreeSortOrder.ChronologicalAscending;
					break;
				case ConversationSortOrder.TreeNewestOnBottom:
					result = ConversationTreeSortOrder.DeepTraversalAscending;
					break;
				default:
					throw new InvalidOperationException("Invalid sort order");
				}
			}
			else
			{
				result = ConversationTreeSortOrder.ChronologicalDescending;
			}
			return result;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00118928 File Offset: 0x00116B28
		internal static bool ShouldExcludeFromExpansion(UserContext userContext, OwaStoreObjectId owaConversationId, IStorePropertyBag propertyBag, StoreObjectId conversationParentFolderId)
		{
			StoreObjectId property = ItemUtility.GetProperty<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, null);
			MailboxSession mailboxSession = (MailboxSession)owaConversationId.GetSession(userContext);
			StoreObjectId storeObjectId = userContext.GetDeletedItemsFolderId(mailboxSession).StoreObjectId;
			return property.Equals(storeObjectId) && !property.Equals(conversationParentFolderId);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00118A88 File Offset: 0x00116C88
		internal static void SortPropertyBags(IConversationTreeNode node, List<StoreObjectId> localItemIds, MailboxSession session)
		{
			StoreObjectId sentItemsFolderId = session.GetDefaultFolderId(DefaultFolderType.SentItems);
			node.StorePropertyBags.Sort(delegate(IStorePropertyBag left, IStorePropertyBag right)
			{
				StoreObjectId objectId = ((VersionedId)left[ItemSchema.Id]).ObjectId;
				StoreObjectId objectId2 = ((VersionedId)right[ItemSchema.Id]).ObjectId;
				bool flag = objectId != null && localItemIds.Contains(objectId);
				bool flag2 = objectId2 != null && localItemIds.Contains(objectId2);
				if (flag && !flag2)
				{
					return -1;
				}
				if (flag2 && !flag)
				{
					return 1;
				}
				StoreObjectId storeObjectId = (StoreObjectId)left[StoreObjectSchema.ParentItemId];
				StoreObjectId storeObjectId2 = (StoreObjectId)right[StoreObjectSchema.ParentItemId];
				if (storeObjectId == null && storeObjectId2 == null)
				{
					return 0;
				}
				if (storeObjectId == null)
				{
					return 1;
				}
				if (storeObjectId2 == null)
				{
					return -1;
				}
				if (storeObjectId.Equals(storeObjectId2))
				{
					return 0;
				}
				if (storeObjectId.Equals(sentItemsFolderId))
				{
					return -1;
				}
				if (storeObjectId2.Equals(sentItemsFolderId))
				{
					return 1;
				}
				UserContext userContext = UserContextManager.GetUserContext();
				string cachedFolderName = userContext.GetCachedFolderName(storeObjectId, session);
				string cachedFolderName2 = userContext.GetCachedFolderName(storeObjectId2, session);
				return string.Compare(cachedFolderName, cachedFolderName2, StringComparison.CurrentCulture);
			});
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x00118AD4 File Offset: 0x00116CD4
		internal static bool IsSupportedReadingPaneType(UserContext userContext, IStorePropertyBag propertyBag)
		{
			string property = ItemUtility.GetProperty<string>(propertyBag, StoreObjectSchema.ItemClass, null);
			if (ConversationUtilities.IsVoteMessage(propertyBag))
			{
				return false;
			}
			if (string.Equals(property, "IPM.Schedule.Meeting.Request", StringComparison.Ordinal) || string.Equals(property, "IPM.Schedule.Meeting.Canceled", StringComparison.Ordinal) || string.Equals(property, "IPM.Sharing", StringComparison.Ordinal) || string.Equals(property, "IPM.Post", StringComparison.Ordinal) || ObjectClass.IsVoiceMessage(property) || ObjectClass.IsMeetingResponse(property) || ObjectClass.IsSmsMessage(property))
			{
				return true;
			}
			if (ObjectClass.IsSmime(property))
			{
				return false;
			}
			FormKey formKey = new FormKey(ApplicationElement.Item, property, "Preview", string.Empty);
			FormValue formValue = FormsRegistryManager.LookupForm(formKey, userContext.Experiences);
			string a = formValue.Value as string;
			return string.Equals(a, "forms/premium/ReadMessage.ascx", StringComparison.Ordinal);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00118B8C File Offset: 0x00116D8C
		internal static bool IsOpenInOutlookType(IStorePropertyBag propertyBag)
		{
			string property = ItemUtility.GetProperty<string>(propertyBag, StoreObjectSchema.ItemClass, null);
			return !ObjectClass.IsOfClass(property, "IPM.Note.Microsoft.Approval.Request") && ConversationUtilities.IsVoteMessage(propertyBag);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00118BBC File Offset: 0x00116DBC
		internal static bool IsVoteMessage(IStorePropertyBag propertyBag)
		{
			string property = ItemUtility.GetProperty<string>(propertyBag, StoreObjectSchema.ItemClass, null);
			object obj = propertyBag.TryGetProperty(MessageItemSchema.VotingBlob);
			return ObjectClass.IsOfClass(property, "IPM.Note") && !(obj is PropertyError);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00118C00 File Offset: 0x00116E00
		internal static StoreObjectId GetLatestMessage(MailboxSession mailboxSession, Conversation conversation, StoreObjectId folderId)
		{
			StoreObjectId result = null;
			ExDateTime t = ExDateTime.MinValue;
			IList<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds(mailboxSession, conversation, folderId);
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode2.StorePropertyBags)
				{
					StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
					if (localItemIds.Contains(objectId))
					{
						ExDateTime property = ItemUtility.GetProperty<ExDateTime>(storePropertyBag, ItemSchema.ReceivedTime, ExDateTime.MinValue);
						if (t <= property)
						{
							result = objectId;
							t = property;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x00118CEC File Offset: 0x00116EEC
		internal static IList<StoreObjectId> GetFlagedItems(MailboxSession mailboxSession, Conversation conversation, StoreObjectId folderId, params FlagStatus[] flagsStatus)
		{
			if (flagsStatus == null || flagsStatus.Length == 0)
			{
				throw new ArgumentException("flagStatus should contain something");
			}
			IList<StoreObjectId> list = new List<StoreObjectId>();
			ExDateTime minValue = ExDateTime.MinValue;
			IList<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds(mailboxSession, conversation, folderId);
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode2.StorePropertyBags)
				{
					StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
					object obj = storePropertyBag.TryGetProperty(ItemSchema.FlagStatus);
					if (localItemIds.Contains(objectId) && !(obj is int))
					{
						foreach (FlagStatus flagStatus in flagsStatus)
						{
							if ((int)obj == (int)flagStatus)
							{
								list.Add(objectId);
								break;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00118E10 File Offset: 0x00117010
		internal static bool IsDeletedItemPart(MailboxSession mailboxSession, IConversationTreeNode treeNode)
		{
			for (int i = 0; i < treeNode.StorePropertyBags.Count; i++)
			{
				StoreObjectId folderId = (StoreObjectId)treeNode.StorePropertyBags[i][StoreObjectSchema.ParentItemId];
				if (!Utilities.IsDefaultFolderId(mailboxSession, folderId, DefaultFolderType.DeletedItems))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00118E5C File Offset: 0x0011705C
		internal static int GetGlobalCount(Conversation conversation)
		{
			int num = 0;
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				num += conversationTreeNode2.StorePropertyBags.Count;
			}
			return num;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x00118EB8 File Offset: 0x001170B8
		internal static bool HideDeletedItems
		{
			get
			{
				return Globals.HideDeletedItems || UserContextManager.GetUserContext().UserOptions.HideDeletedItems;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00118ED2 File Offset: 0x001170D2
		internal static bool ShouldAllowConversationView(UserContext userContext, Folder folder)
		{
			return !userContext.IsInOtherMailbox(folder) && !Utilities.IsPublic(folder) && !Utilities.IsDefaultFolder(folder, DefaultFolderType.JunkEmail) && !Utilities.IsDefaultFolder(folder, DefaultFolderType.Notes) && !Utilities.IsWebPartDelegateAccessRequest(OwaContext.Current);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00118F06 File Offset: 0x00117106
		internal static bool IsConversationSortColumn(ColumnId column)
		{
			return column == ColumnId.ConversationLastDeliveryTime || column == ColumnId.ConversationSenderList || column == ColumnId.ConversationToList || column == ColumnId.ConversationSubject || column == ColumnId.ConversationSize || column == ColumnId.ConversationIcon || column == ColumnId.ConversationHasAttachment || column == ColumnId.ConversationImportance || column == ColumnId.ConversationFlagStatus;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x00118F38 File Offset: 0x00117138
		internal static bool IsMultiValuedConversationSortColumn(ColumnId column)
		{
			return column == ColumnId.ConversationSenderList || column == ColumnId.ConversationToList || column == ColumnId.ConversationIcon;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00118F4C File Offset: 0x0011714C
		internal static bool IgnoreConversation(UserContext userContext, OwaStoreObjectId conversationId)
		{
			Conversation conversation = ConversationUtilities.LoadConversation(userContext, conversationId, new PropertyDefinition[]
			{
				ItemSchema.Id,
				StoreObjectSchema.ItemClass
			});
			if (!conversationId.IsPublic)
			{
				ConversationUtilities.DeleteCorrelatedCalendarItems(conversation);
			}
			AggregateOperationResult aggregateOperationResult = conversation.AlwaysDelete(true, true);
			return aggregateOperationResult.OperationResult == OperationResult.Succeeded;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x00118F9C File Offset: 0x0011719C
		internal static void CancelIgnoreConversation(UserContext userContext, OwaStoreObjectId owaStoreObjectId, bool restoreItems)
		{
			using (ConversationActionItem conversationActionItemById = ConversationUtilities.GetConversationActionItemById(userContext, owaStoreObjectId))
			{
				if (conversationActionItemById != null)
				{
					conversationActionItemById.AlwaysDeleteValue = false;
					conversationActionItemById.AlwaysMoveValue = null;
					if (restoreItems)
					{
						conversationActionItemById.Delete(ConversationActionItemSchema.ConversationActionMaxDeliveryTime);
					}
					else
					{
						conversationActionItemById.ConversationActionMaxDeliveryTime = ExDateTime.UtcNow;
					}
					conversationActionItemById.Save(SaveMode.ResolveConflicts);
				}
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00119004 File Offset: 0x00117204
		internal static bool IsConversationIgnored(UserContext userContext, OwaStoreObjectId owaStoreObjectId, Conversation conversation)
		{
			MailboxSession mailboxSession = owaStoreObjectId.GetSession(userContext) as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
			if (conversation == null || ConversationUtilities.GetAllItemIds(owaStoreObjectId, conversation, null, new StoreObjectId[]
			{
				defaultFolderId,
				defaultFolderId2
			}).Count == 0)
			{
				using (ConversationActionItem conversationActionItemById = ConversationUtilities.GetConversationActionItemById(userContext, owaStoreObjectId))
				{
					return conversationActionItemById != null && conversationActionItemById.AlwaysDeleteValue;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x00119090 File Offset: 0x00117290
		internal static bool IsConversationIgnored(Item item)
		{
			ConversationId property = ItemUtility.GetProperty<ConversationId>(item, ItemSchema.ConversationId, null);
			MailboxSession mailboxSession = item.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			bool result;
			using (ConversationActionItem conversationActionItem = ConversationUtilities.GetConversationActionItem(property, mailboxSession))
			{
				result = (conversationActionItem != null && conversationActionItem.AlwaysDeleteValue);
			}
			return result;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x001190F0 File Offset: 0x001172F0
		internal static void UpdateAlwaysCategorizeRule(UserContext userContext, OwaStoreObjectId conversationId, Conversation conversation, string[] categoriesToAdd, string[] categoriesToRemove)
		{
			string[] array = null;
			using (ConversationActionItem conversationActionItemById = ConversationUtilities.GetConversationActionItemById(userContext, conversationId))
			{
				if (conversationActionItemById != null)
				{
					array = conversationActionItemById.AlwaysCategorizeValue;
				}
			}
			List<string> list = new List<string>();
			if (array != null)
			{
				list.AddRange(array);
			}
			foreach (string item in categoriesToAdd)
			{
				list.Add(item);
			}
			foreach (string item2 in categoriesToRemove)
			{
				list.Remove(item2);
			}
			conversation.AlwaysCategorize(list.ToArray(), false);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00119198 File Offset: 0x00117398
		internal static void ClearAlwaysCategorizeRule(UserContext userContext, OwaStoreObjectId conversationId)
		{
			using (ConversationActionItem conversationActionItemById = ConversationUtilities.GetConversationActionItemById(userContext, conversationId))
			{
				if (conversationActionItemById != null && conversationActionItemById.AlwaysCategorizeValue.Length > 0)
				{
					Conversation conversation = ConversationUtilities.LoadConversation(userContext, conversationId, new PropertyDefinition[0]);
					conversation.AlwaysCategorize(new string[0], false);
				}
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x001191F4 File Offset: 0x001173F4
		internal static ConversationActionItem GetConversationActionItemById(UserContext userContext, OwaStoreObjectId owaStoreObjectId)
		{
			if (owaStoreObjectId.IsPublic)
			{
				return null;
			}
			ConversationId conversationId;
			if (owaStoreObjectId.IsConversationId)
			{
				conversationId = owaStoreObjectId.ConversationId;
			}
			else
			{
				try
				{
					conversationId = ConversationUtilities.MapItemToConversation(userContext, owaStoreObjectId);
				}
				catch (ObjectNotFoundException)
				{
					return null;
				}
			}
			if (conversationId == null)
			{
				return null;
			}
			return ConversationUtilities.GetConversationActionItem(conversationId, (MailboxSession)owaStoreObjectId.GetSession(userContext));
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00119254 File Offset: 0x00117454
		public static string MaskConversationSubject(string subject)
		{
			if (!string.IsNullOrEmpty(subject) && subject.Trim().Length != 0)
			{
				return subject;
			}
			return LocalizedStrings.GetNonEncoded(522267577);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00119278 File Offset: 0x00117478
		private static ConversationActionItem GetConversationActionItem(ConversationId conversationId, MailboxSession mailboxSession)
		{
			StoreId storeId = ConversationActionItem.QueryConversationActionsFolder(mailboxSession, conversationId);
			if (storeId != null)
			{
				return ConversationActionItem.Bind(mailboxSession, storeId);
			}
			return null;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0011929C File Offset: 0x0011749C
		private static void DeleteCorrelatedCalendarItems(Conversation conversation)
		{
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (IStorePropertyBag storePropertyBag in conversationTreeNode2.StorePropertyBags)
				{
					string itemClass = (string)storePropertyBag.TryGetProperty(StoreObjectSchema.ItemClass);
					if (ObjectClass.IsMeetingMessage(itemClass))
					{
						VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
						try
						{
							MeetingUtilities.DeleteMeetingMessageCalendarItem(versionedId.ObjectId);
						}
						catch (ObjectNotFoundException)
						{
						}
					}
				}
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x00119370 File Offset: 0x00117570
		private static void GetNodesToExpand(UserContext userContext, OwaStoreObjectId owaConversationId, Conversation conversation, StoreObjectId parentFolderId, OwaStoreObjectId[] expandedIds, int[] expandeInternetMIds, out List<IConversationTreeNode> nodesToExpand, out IConversationTreeNode nodeToSelect)
		{
			nodeToSelect = null;
			nodesToExpand = new List<IConversationTreeNode>();
			if (expandedIds != null)
			{
				foreach (OwaStoreObjectId owaStoreObjectId in expandedIds)
				{
					IConversationTreeNode item;
					if (conversation.ConversationTree.TryGetConversationTreeNode(owaStoreObjectId.StoreObjectId, out item))
					{
						nodesToExpand.Add(item);
					}
				}
			}
			if (expandeInternetMIds != null)
			{
				foreach (int num in expandeInternetMIds)
				{
					foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
					{
						string property = ItemUtility.GetProperty<string>(conversationTreeNode.StorePropertyBags[0], ItemSchema.InternetMessageId, null);
						if (!string.IsNullOrEmpty(property) && property.GetHashCode() == num)
						{
							nodesToExpand.Add(conversationTreeNode);
						}
					}
				}
			}
			bool flag = (ConversationSortOrder)0 != (userContext.UserOptions.ConversationSortOrder & ConversationSortOrder.NewestOnTop);
			ExDateTime t = flag ? ExDateTime.MinValue : ExDateTime.MaxValue;
			ExDateTime t2 = ExDateTime.MinValue;
			ExDateTime t3 = ExDateTime.MinValue;
			ConversationTreeNode conversationTreeNode2 = null;
			ConversationTreeNode conversationTreeNode3 = null;
			foreach (IConversationTreeNode conversationTreeNode4 in conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode5 = (ConversationTreeNode)conversationTreeNode4;
				if (conversationTreeNode5.ParentNode != null)
				{
					ExDateTime property2 = ItemUtility.GetProperty<ExDateTime>(conversationTreeNode5.StorePropertyBags[0], ItemSchema.ReceivedTime, flag ? ExDateTime.MinValue : ExDateTime.MaxValue);
					bool flag2 = true;
					if (!ConversationUtilities.ShouldExcludeFromExpansion(userContext, owaConversationId, conversationTreeNode5.StorePropertyBags[0], parentFolderId))
					{
						flag2 = ItemUtility.GetProperty<bool>(conversationTreeNode5.StorePropertyBags[0], MessageItemSchema.IsRead, false);
						if (flag)
						{
							if (!flag2 && property2 >= t)
							{
								nodeToSelect = conversationTreeNode5;
								t = property2;
							}
						}
						else if (!flag2 && property2 <= t)
						{
							nodeToSelect = conversationTreeNode5;
							t = property2;
						}
						if (property2 >= t2)
						{
							conversationTreeNode2 = conversationTreeNode5;
							t2 = property2;
						}
					}
					if (property2 >= t3)
					{
						conversationTreeNode3 = conversationTreeNode5;
						t3 = property2;
					}
					if (!flag2 && !nodesToExpand.Contains(conversationTreeNode5))
					{
						nodesToExpand.Add(conversationTreeNode5);
					}
				}
			}
			IConversationTreeNode conversationTreeNode6;
			if ((conversationTreeNode6 = nodeToSelect) == null)
			{
				conversationTreeNode6 = (conversationTreeNode2 ?? conversationTreeNode3);
			}
			nodeToSelect = conversationTreeNode6;
			if (nodesToExpand.Count == 0 && nodeToSelect != null)
			{
				nodesToExpand.Add(nodeToSelect);
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x00119604 File Offset: 0x00117804
		public static bool ShouldRenderItem(IConversationTreeNode node, MailboxSession session)
		{
			bool property = ItemUtility.GetProperty<bool>(node.StorePropertyBags[0], MessageItemSchema.IsDraft, false);
			bool flag = ConversationUtilities.IsDeletedItemPart(session, node);
			bool flag2 = !ConversationUtilities.IsLocalNode(node);
			return !property || !flag || !flag2;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00119648 File Offset: 0x00117848
		public static void LogConversationPerformanceData(Conversation conversation)
		{
			if (!Globals.CollectPerRequestPerformanceStats || OwaContext.Current.OwaPerformanceData == null)
			{
				return;
			}
			if (conversation == null)
			{
				throw new ArgumentNullException("conversation");
			}
			ConversationUtilities.AddPerformanceTrace("leaf", conversation.OptimizationCounters.LeafNodeCount);
			ConversationUtilities.AddPerformanceTrace("extr", conversation.OptimizationCounters.ItemsExtracted);
			ConversationUtilities.AddPerformanceTrace("open", conversation.OptimizationCounters.ItemsOpened);
			ConversationUtilities.AddPerformanceTrace("summ", conversation.OptimizationCounters.SummariesConstructed);
			ConversationUtilities.AddPerformanceTrace("notag", conversation.OptimizationCounters.BodyTagNotPresentCount);
			ConversationUtilities.AddPerformanceTrace("tagmis", conversation.OptimizationCounters.BodyTagMismatchedCount);
			ConversationUtilities.AddPerformanceTrace("bfmis", conversation.OptimizationCounters.BodyFormatMismatchedCount);
			ConversationUtilities.AddPerformanceTrace("attach", conversation.OptimizationCounters.AttachmentPresentCount);
			ConversationUtilities.AddPerformanceTrace("nopar", conversation.OptimizationCounters.ParticipantNotFoundCount);
			ConversationUtilities.AddPerformanceTrace("expro", conversation.OptimizationCounters.ExtraPropertiesNeededCount);
			ConversationUtilities.AddPerformanceTrace("nomshdr", conversation.OptimizationCounters.NonMSHeaderCount);
			ConversationUtilities.AddPerformanceTrace("inline", conversation.OptimizationCounters.PossibleInlinesCount);
			ConversationUtilities.AddPerformanceTrace("irm", conversation.OptimizationCounters.IrmProtectedCount);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00119788 File Offset: 0x00117988
		private static void AddPerformanceTrace(string keyName, int keyValue)
		{
			if (keyValue != 0)
			{
				OwaContext.Current.OwaPerformanceData.TraceOther(string.Format("{0}={1}", keyName, keyValue));
			}
		}

		// Token: 0x04002192 RID: 8594
		public const string ConversationItemType = "IPM.Conversation";

		// Token: 0x04002193 RID: 8595
		private static readonly string[] conversationExcludedTypesArray = new string[]
		{
			"IPM.Activity",
			"IPM.Appointment",
			"IPM.Contact",
			"IPM.ContentClassDef",
			"IPM.DistList",
			"IPM.Microsoft.ScheduleData.FreeBusy",
			"IPM.InkNodes",
			"IPM.Note.Exchange.ActiveSync",
			"IPM.Note.Rules",
			"IPM.Document",
			"IPM.InfoPathForm"
		};

		// Token: 0x04002194 RID: 8596
		private static readonly HashSet<string> conversationExcludedTypes = new HashSet<string>(ConversationUtilities.conversationExcludedTypesArray, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002195 RID: 8597
		private static readonly Dictionary<IConversationTreeNode, bool> localTreeNodes = new Dictionary<IConversationTreeNode, bool>(ConversationTreeNodeBase.EqualityComparer);
	}
}
