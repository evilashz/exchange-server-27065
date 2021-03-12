using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000270 RID: 624
	internal class NormalQueryView : BaseQueryView
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x0004E530 File Offset: 0x0004C730
		public static void PrepareDraftItemIds(MailboxSession session, IList<ConversationType> conversations)
		{
			if (conversations.Count > 0)
			{
				Dictionary<StoreId, List<ConversationType>> dictionary = new Dictionary<StoreId, List<ConversationType>>();
				IEnumerable<ConversationType> enumerable = from conversation in conversations
				where conversation.DraftStoreIds != null && conversation.DraftStoreIds.FirstOrDefault<StoreId>() != null
				select conversation;
				foreach (ConversationType conversationType in enumerable)
				{
					foreach (StoreId key in conversationType.DraftStoreIds)
					{
						List<ConversationType> list;
						if (dictionary.TryGetValue(key, out list))
						{
							list.Add(conversationType);
						}
						else
						{
							list = new List<ConversationType>();
							list.Add(conversationType);
							dictionary.Add(key, list);
						}
					}
				}
				for (int i = 0; i < dictionary.Keys.Count; i += 50)
				{
					int num = (i + 50 > dictionary.Keys.Count) ? (dictionary.Keys.Count - i) : 50;
					QueryFilter[] array = new QueryFilter[num];
					for (int j = i; j < i + num; j++)
					{
						array[j - i] = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, dictionary.Keys.ElementAt(j));
					}
					QueryFilter filter = new OrFilter(array);
					NormalQueryView.QueryDraftsToUpdateConversation(session, dictionary, filter);
				}
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
		public NormalQueryView(IQueryResult queryResult, int rowsToGet, BasePagingType paging)
		{
			this.CombineRPCViewData(queryResult, rowsToGet, paging);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004E6C4 File Offset: 0x0004C8C4
		public NormalQueryView(object[][] view, int rowsToGet)
		{
			this.RetrieveCalendarViewData(view, rowsToGet);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		public override ItemType[] ConvertToItems(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession)
		{
			ItemType[] array = new ItemType[this.viewList.Count];
			for (int i = 0; i < this.viewList.Count; i++)
			{
				IDictionary<PropertyDefinition, object> rowData = BaseQueryView.GetRowData(propsToFetch, this.viewList[i]);
				StoreObjectType storeObjectType;
				ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = classDeterminer.GetToServiceObjectPropertyList(rowData, out storeObjectType);
				ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectType);
				this.CheckClientConnection();
				CommandOptions commandOptions = CommandOptions.None;
				if (idAndSession.Session is PublicFolderSession && ClientInfo.OWA.IsMatch(idAndSession.Session.ClientInfoString))
				{
					commandOptions = CommandOptions.ConvertParentFolderIdToPublicFolderId;
				}
				array[i] = itemType;
				toServiceObjectPropertyList.ConvertPropertiesToServiceObject(itemType, rowData, idAndSession, commandOptions);
			}
			return array;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004E780 File Offset: 0x0004C980
		public override FindItemParentWrapper ConvertToFindItemParentWrapper(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, BasePageResult pageResult, QueryType queryType)
		{
			ItemType[] items = this.ConvertToItems(propsToFetch, classDeterminer, idAndSession);
			return new FindItemParentWrapper(items, pageResult);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004E7A0 File Offset: 0x0004C9A0
		public override BaseFolderType[] ConvertToFolderObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession)
		{
			BaseFolderType[] array = new BaseFolderType[this.viewList.Count];
			for (int i = 0; i < this.viewList.Count; i++)
			{
				IDictionary<PropertyDefinition, object> rowData = BaseQueryView.GetRowData(propsToFetch, this.viewList[i]);
				StoreObjectType storeObjectType;
				ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = classDeterminer.GetToServiceObjectPropertyList(rowData, out storeObjectType);
				BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectType);
				this.CheckClientConnection();
				CommandOptions commandOptions = CommandOptions.None;
				if (idAndSession.Session is PublicFolderSession && ClientInfo.OWA.IsMatch(idAndSession.Session.ClientInfoString))
				{
					commandOptions = (CommandOptions.ConvertParentFolderIdToPublicFolderId | CommandOptions.ConvertFolderIdToPublicFolderId);
				}
				array[i] = baseFolderType;
				toServiceObjectPropertyList.ConvertPropertiesToServiceObject(baseFolderType, rowData, idAndSession, commandOptions);
			}
			return array;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004E840 File Offset: 0x0004CA40
		public override ConversationType[] ConvertToConversationObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, RequestDetailsLogger logger)
		{
			ConversationType[] array = new ConversationType[this.viewList.Count];
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			for (int i = 0; i < this.viewList.Count; i++)
			{
				IDictionary<PropertyDefinition, object> rowData = BaseQueryView.GetRowData(propsToFetch, this.viewList[i]);
				StoreObjectType storeObjectType;
				ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = classDeterminer.GetToServiceObjectPropertyList(rowData, out storeObjectType);
				ConversationType conversationType = new ConversationType();
				this.CheckClientConnection();
				array[i] = conversationType;
				toServiceObjectPropertyList.ConvertPropertiesToServiceObject(conversationType, rowData, idAndSession);
			}
			NormalQueryView.PrepareDraftItemIdsIfNecessary(classDeterminer, idAndSession.Session as MailboxSession, array);
			if (ClientInfo.OWA.IsMatch(idAndSession.Session.ClientInfoString))
			{
				ConversationFeedLoader conversationFeedLoader = new ConversationFeedLoader(mailboxSession, EWSSettings.RequestTimeZone);
				conversationFeedLoader.LoadConversationFeedItemsIfNecessary(classDeterminer, array);
			}
			return array;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0004E900 File Offset: 0x0004CB00
		private static void PrepareDraftItemIdsIfNecessary(PropertyListForViewRowDeterminer classDeterminer, MailboxSession session, IList<ConversationType> conversations)
		{
			ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyListForConversation = classDeterminer.GetToServiceObjectPropertyListForConversation();
			if (toServiceObjectPropertyListForConversation != null)
			{
				bool flag = false;
				ResponseShape responseShape = toServiceObjectPropertyListForConversation.ResponseShape;
				if (responseShape.BaseShape == ShapeEnum.AllProperties)
				{
					flag = true;
				}
				else
				{
					PropertyPath[] additionalProperties = toServiceObjectPropertyListForConversation.ResponseShape.AdditionalProperties;
					if (additionalProperties != null)
					{
						foreach (PropertyPath propertyPath in additionalProperties)
						{
							PropertyUri propertyUri = propertyPath as PropertyUri;
							if (propertyUri != null && propertyUri.Uri == PropertyUriEnum.ConversationDraftItemIds)
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (flag)
				{
					NormalQueryView.PrepareDraftItemIds(session, conversations);
				}
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004E984 File Offset: 0x0004CB84
		public override Persona[] ConvertPersonViewToPersonaObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, MailboxSession mailboxSession)
		{
			Persona[] array = new Persona[this.viewList.Count];
			for (int i = 0; i < this.viewList.Count; i++)
			{
				Persona persona = this.LoadPersonaFrom(this.viewList[i], propsToFetch, classDeterminer, mailboxSession);
				array[i] = persona;
			}
			return array;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004E9D4 File Offset: 0x0004CBD4
		private static void QueryDraftsToUpdateConversation(MailboxSession session, Dictionary<StoreId, List<ConversationType>> draftToConversationMap, QueryFilter filter)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Drafts);
			using (Folder folder = Folder.Bind(session, defaultFolderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, filter, new SortBy[]
				{
					new SortBy(ItemSchema.Id, SortOrder.Ascending)
				}, NormalQueryView.draftsInConversationProperties))
				{
					if (queryResult != null)
					{
						bool flag = true;
						while (flag)
						{
							object[][] rows = queryResult.GetRows(10000);
							if (rows.Length > 0)
							{
								foreach (object[] array2 in rows)
								{
									StoreId storeId = array2[0] as StoreId;
									bool flag2 = (bool)array2[1];
									bool flag3 = (bool)array2[2];
									if (flag2 && !flag3)
									{
										StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
										List<ConversationType> list = draftToConversationMap[storeObjectId];
										foreach (ConversationType conversationType in list)
										{
											if (conversationType.DraftItemIdsList == null)
											{
												conversationType.DraftItemIdsList = new List<BaseItemId>(conversationType.DraftStoreIds.Count<StoreId>());
											}
											ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, new MailboxId(session), null);
											conversationType.DraftItemIdsList.Add(new ItemId
											{
												Id = concatenatedId.Id,
												ChangeKey = concatenatedId.ChangeKey
											});
										}
									}
								}
							}
							else
							{
								flag = false;
							}
						}
					}
				}
			}
			foreach (KeyValuePair<StoreId, List<ConversationType>> keyValuePair in draftToConversationMap)
			{
				List<ConversationType> value = keyValuePair.Value;
				foreach (ConversationType conversationType2 in value)
				{
					if (conversationType2.DraftItemIdsList != null && conversationType2.DraftItemIdsList.Count > 0)
					{
						conversationType2.DraftItemIds = conversationType2.DraftItemIdsList.ToArray<BaseItemId>();
					}
				}
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004EC5C File Offset: 0x0004CE5C
		private Persona LoadPersonaFrom(object[] viewListRow, PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, MailboxSession mailboxSession)
		{
			IDictionary<PropertyDefinition, object> rowData = BaseQueryView.GetRowData(propsToFetch, viewListRow);
			StoreObjectType storeObjectType;
			ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = classDeterminer.GetToServiceObjectPropertyList(rowData, out storeObjectType);
			this.CheckClientConnection();
			return Persona.LoadFromPropertyBag(mailboxSession, rowData, toServiceObjectPropertyList);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0004EC8C File Offset: 0x0004CE8C
		protected virtual void ThrowIfFindCountLimitExceeded(uint viewLength)
		{
			int num;
			if (!CallContext.Current.Budget.TryIncrementFoundObjectCount(viewLength, out num))
			{
				ExceededFindCountLimitException.Throw();
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004ECB4 File Offset: 0x0004CEB4
		private void CombineRPCViewData(IQueryResult queryResult, int rowsToGet, BasePagingType pagingType)
		{
			IndexedPageView indexedPageView = pagingType as IndexedPageView;
			bool flag = indexedPageView != null && indexedPageView.Offset == 0;
			if (pagingType != null)
			{
				bool noRowCountRetrieval = pagingType.NoRowCountRetrieval;
			}
			bool flag2 = pagingType != null && pagingType.LoadPartialPageRows;
			int num = flag ? 0 : queryResult.CurrentRow;
			bool flag3 = true;
			if (rowsToGet == 2147483647)
			{
				CallContext.Current.Budget.CanAllocateFoundObjects((uint)rowsToGet, out rowsToGet);
			}
			do
			{
				this.CheckClientConnection();
				base.CanAllocateFoundObjects(pagingType, (uint)rowsToGet, out rowsToGet);
				object[][] rows = queryResult.GetRows(rowsToGet, out flag3);
				int length = rows.GetLength(0);
				rowsToGet -= length;
				for (int i = 0; i < length; i++)
				{
					this.viewList.Add(rows[i]);
				}
				base.AllocateBudgetFoundObjects(length, pagingType);
			}
			while (!flag2 && flag3 && rowsToGet > 0);
			if (!flag3)
			{
				this.totalItems = num + this.viewList.Count;
				this.retrievedLastItem = true;
			}
			else
			{
				this.totalItems = queryResult.EstimatedRowCount;
				if (flag2)
				{
					int num2 = this.totalItems - num;
					this.retrievedLastItem = (this.viewList.Count == num2);
				}
				else
				{
					this.retrievedLastItem = false;
				}
			}
			if (pagingType != null)
			{
				pagingType.RowsFetched = this.viewList.Count;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004EDE8 File Offset: 0x0004CFE8
		private void RetrieveCalendarViewData(object[][] view, int rowsToGet)
		{
			this.totalItems = view.Length;
			int num = view.GetLength(0);
			if (num > rowsToGet)
			{
				num = rowsToGet;
				this.retrievedLastItem = false;
			}
			else
			{
				this.retrievedLastItem = true;
			}
			if (num > 0)
			{
				this.ThrowIfFindCountLimitExceeded((uint)num);
			}
			for (int i = 0; i < num; i++)
			{
				this.viewList.Add(view[i]);
			}
		}

		// Token: 0x04000C10 RID: 3088
		private const int DraftQueryFilterBatchSize = 50;

		// Token: 0x04000C11 RID: 3089
		private static readonly PropertyDefinition[] draftsInConversationProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MessageItemSchema.IsDraft,
			MessageItemSchema.HasBeenSubmitted
		};

		// Token: 0x04000C12 RID: 3090
		private List<object[]> viewList = new List<object[]>();

		// Token: 0x02000271 RID: 625
		private class ByteArrayComparer : IEqualityComparer<byte[]>
		{
			// Token: 0x06001056 RID: 4182 RVA: 0x0004EE78 File Offset: 0x0004D078
			public bool Equals(byte[] x, byte[] y)
			{
				if (x == null || y == null)
				{
					return x == y;
				}
				if (x.Length != y.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i] != y[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001057 RID: 4183 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
			public int GetHashCode(byte[] obj)
			{
				int num = 0;
				foreach (byte b in obj)
				{
					num += (int)b;
					num *= 37;
				}
				return num;
			}
		}
	}
}
