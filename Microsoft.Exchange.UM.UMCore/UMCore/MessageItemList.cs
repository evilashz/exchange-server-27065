using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000177 RID: 375
	internal class MessageItemList
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x00030350 File Offset: 0x0002E550
		internal MessageItemList(UMSubscriber user, StoreObjectId folderId, MessageItemListSortType sortType, PropertyDefinition[] properties)
		{
			this.pager = MessageItemList.MessageItemPager.Create(sortType);
			this.idx = -1;
			this.user = user;
			this.folderId = folderId;
			this.ignoreList = new Dictionary<StoreObjectId, bool>();
			int num = 0;
			this.propIndexMap = new Dictionary<PropertyDefinition, int>();
			foreach (PropertyDefinition key in properties)
			{
				if (!this.propIndexMap.ContainsKey(key))
				{
					this.propIndexMap.Add(key, num++);
				}
			}
			foreach (PropertyDefinition key2 in this.pager.RequiredProperties)
			{
				if (!this.propIndexMap.ContainsKey(key2))
				{
					this.propIndexMap.Add(key2, num++);
				}
			}
			foreach (PropertyDefinition key3 in MessageItemList.myProps)
			{
				if (!this.propIndexMap.ContainsKey(key3))
				{
					this.propIndexMap.Add(key3, num++);
				}
			}
			this.properties = new PropertyDefinition[this.propIndexMap.Count];
			foreach (KeyValuePair<PropertyDefinition, int> keyValuePair in this.propIndexMap)
			{
				this.properties[keyValuePair.Value] = keyValuePair.Key;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x000304C8 File Offset: 0x0002E6C8
		internal StoreObjectId CurrentStoreObjectId
		{
			get
			{
				this.CheckIsValid();
				return StoreId.GetStoreObjectId(this.SafeGetProperty<StoreId>(this.view[this.idx], ItemSchema.Id, null));
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000304EE File Offset: 0x0002E6EE
		internal int CurrentOffset
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000304F6 File Offset: 0x0002E6F6
		internal T SafeGetProperty<T>(PropertyDefinition prop, T defaultValue)
		{
			this.CheckIsValid();
			return this.SafeGetProperty<T>(this.view[this.idx], prop, defaultValue);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00030513 File Offset: 0x0002E713
		internal void Ignore(StoreObjectId id)
		{
			this.ignoreList[id] = true;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00030522 File Offset: 0x0002E722
		internal void UnIgnore(StoreObjectId id)
		{
			this.ignoreList.Remove(id);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00030534 File Offset: 0x0002E734
		internal void Seek(StoreId storeId)
		{
			this.idx = -1;
			for (int i = 0; i < this.view.Length; i++)
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(this.SafeGetProperty<StoreId>(this.view[i], ItemSchema.Id, null));
				if (storeObjectId.Equals(storeId))
				{
					this.idx = i;
				}
			}
			this.CheckIsValid();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003058B File Offset: 0x0002E78B
		internal void Seek(int offset)
		{
			this.idx = offset;
			if (this.idx < -1 || (this.view != null && this.idx > this.view.Length))
			{
				throw new InvalidOperationException("InvalidIndex");
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000305C0 File Offset: 0x0002E7C0
		internal void Start()
		{
			this.idx = -1;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000305C9 File Offset: 0x0002E7C9
		internal void End()
		{
			this.idx = this.view.Length;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000305DC File Offset: 0x0002E7DC
		internal bool Next(bool unreadOnly)
		{
			StoreObjectId storeObjectId = null;
			if (this.view == null && this.PopulateItemView(unreadOnly) == 0)
			{
				return false;
			}
			if (this.idx >= this.view.Length)
			{
				storeObjectId = null;
			}
			else
			{
				while (++this.idx < this.view.Length || this.PopulateItemView(unreadOnly) > 0)
				{
					if (this.idx >= this.view.Length)
					{
						this.idx = this.view.Length;
						break;
					}
					bool flag = this.SafeGetProperty<bool>(this.view[this.idx], MessageItemSchema.IsRead, true);
					if (!this.ignoreList.ContainsKey(this.CurrentStoreObjectId) && (!unreadOnly || !flag))
					{
						storeObjectId = this.CurrentStoreObjectId;
						break;
					}
				}
			}
			return null != storeObjectId;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000306A4 File Offset: 0x0002E8A4
		internal bool Previous()
		{
			StoreObjectId storeObjectId = null;
			if (this.view == null)
			{
				this.PopulateItemView(false);
			}
			if (0 >= this.idx)
			{
				storeObjectId = null;
			}
			else
			{
				while (--this.idx >= 0)
				{
					if (!this.ignoreList.ContainsKey(this.CurrentStoreObjectId))
					{
						storeObjectId = this.CurrentStoreObjectId;
						break;
					}
				}
			}
			return null != storeObjectId;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00030708 File Offset: 0x0002E908
		private T SafeGetProperty<T>(object[] row, PropertyDefinition prop, T defaultValue)
		{
			int num = -1;
			if (!this.propIndexMap.TryGetValue(prop, out num))
			{
				throw new InvalidOperationException();
			}
			object obj = row[num];
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00030744 File Offset: 0x0002E944
		private int PopulateItemView(bool unreadOnly)
		{
			if (this.endOfMessages)
			{
				return 0;
			}
			if (unreadOnly && this.view != null && this.numUnreadMessagesNotYetPaged == 0)
			{
				return 0;
			}
			int result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (Folder folder = Folder.Bind(mailboxSessionLock.Session, this.folderId, new PropertyDefinition[]
				{
					FolderSchema.UnreadCount
				}))
				{
					object[][] array = this.pager.NextPage(this, folder, null == this.view);
					if (0 < array.Length)
					{
						this.lastViewRow = array[array.Length - 1];
					}
					if (this.view == null)
					{
						this.view = array;
						this.numUnreadMessagesNotYetPaged = folder.GetValueOrDefault<int>(FolderSchema.UnreadCount, int.MaxValue);
					}
					else if (0 < array.Length)
					{
						object[][] array2 = new object[this.view.Length + array.Length][];
						for (int i = 0; i < this.view.Length; i++)
						{
							array2[i] = this.view[i];
						}
						for (int j = 0; j < array.Length; j++)
						{
							array2[this.view.Length + j] = array[j];
						}
						this.view = array2;
					}
					this.endOfMessages = (array.Length < MessageItemList.PageSize);
					int num = 0;
					while (this.numUnreadMessagesNotYetPaged > 0 && num < array.Length)
					{
						if (!this.SafeGetProperty<bool>(this.view[num], MessageItemSchema.IsRead, true))
						{
							this.numUnreadMessagesNotYetPaged--;
						}
						num++;
					}
					result = array.Length;
				}
			}
			return result;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00030900 File Offset: 0x0002EB00
		private void CheckIsValid()
		{
			if (this.view == null || this.idx < 0 || this.idx >= this.view.Length)
			{
				throw new InvalidOperationException("InvalidIndex");
			}
		}

		// Token: 0x040009A5 RID: 2469
		internal static readonly int PageSize = 50;

		// Token: 0x040009A6 RID: 2470
		private static PropertyDefinition[] myProps = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MessageItemSchema.IsRead
		};

		// Token: 0x040009A7 RID: 2471
		private UMSubscriber user;

		// Token: 0x040009A8 RID: 2472
		private StoreObjectId folderId;

		// Token: 0x040009A9 RID: 2473
		private PropertyDefinition[] properties;

		// Token: 0x040009AA RID: 2474
		private int idx;

		// Token: 0x040009AB RID: 2475
		private object[][] view;

		// Token: 0x040009AC RID: 2476
		private Dictionary<StoreObjectId, bool> ignoreList;

		// Token: 0x040009AD RID: 2477
		private bool endOfMessages;

		// Token: 0x040009AE RID: 2478
		private int numUnreadMessagesNotYetPaged;

		// Token: 0x040009AF RID: 2479
		private MessageItemList.MessageItemPager pager;

		// Token: 0x040009B0 RID: 2480
		private object[] lastViewRow;

		// Token: 0x040009B1 RID: 2481
		private Dictionary<PropertyDefinition, int> propIndexMap;

		// Token: 0x02000178 RID: 376
		private abstract class MessageItemPager
		{
			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000B39 RID: 2873
			protected abstract SortBy[] Sort { get; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000B3A RID: 2874
			internal abstract PropertyDefinition[] RequiredProperties { get; }

			// Token: 0x06000B3B RID: 2875 RVA: 0x00030964 File Offset: 0x0002EB64
			internal static MessageItemList.MessageItemPager Create(MessageItemListSortType sortType)
			{
				MessageItemList.MessageItemPager result;
				switch (sortType)
				{
				case MessageItemListSortType.LifoVoicemail:
					result = new MessageItemList.LifoVoicemailPager();
					break;
				case MessageItemListSortType.FifoVoicemail:
					result = new MessageItemList.FifoVoicemailPager();
					break;
				case MessageItemListSortType.Email:
					result = new MessageItemList.EmailPager();
					break;
				default:
					throw new InvalidOperationException();
				}
				return result;
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x000309A8 File Offset: 0x0002EBA8
			internal virtual object[][] NextPage(MessageItemList list, Folder folder, bool isFirstPage)
			{
				object[][] rows;
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, this.Sort, list.properties))
				{
					if (!isFirstPage)
					{
						queryResult.SeekToCondition(SeekReference.OriginBeginning, this.BuildSeekFilter(list), SeekToConditionFlags.AllowExtendedFilters);
					}
					rows = queryResult.GetRows(MessageItemList.PageSize);
				}
				return rows;
			}

			// Token: 0x06000B3D RID: 2877
			protected abstract QueryFilter BuildSeekFilter(MessageItemList list);
		}

		// Token: 0x02000179 RID: 377
		private abstract class VoicemailPager : MessageItemList.MessageItemPager
		{
			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00030A10 File Offset: 0x0002EC10
			internal override PropertyDefinition[] RequiredProperties
			{
				get
				{
					return MessageItemList.VoicemailPager.myProps;
				}
			}

			// Token: 0x040009B2 RID: 2482
			private static PropertyDefinition[] myProps = new PropertyDefinition[]
			{
				ItemSchema.Importance,
				ItemSchema.ReceivedTime
			};
		}

		// Token: 0x0200017A RID: 378
		private class FifoVoicemailPager : MessageItemList.VoicemailPager
		{
			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00030A4A File Offset: 0x0002EC4A
			protected override SortBy[] Sort
			{
				get
				{
					return MessageItemList.FifoVoicemailPager.sort;
				}
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x00030A54 File Offset: 0x0002EC54
			protected override QueryFilter BuildSeekFilter(MessageItemList list)
			{
				ExDateTime exDateTime = list.SafeGetProperty<ExDateTime>(list.lastViewRow, ItemSchema.ReceivedTime, ExDateTime.MaxValue);
				Importance importance = list.SafeGetProperty<Importance>(list.lastViewRow, ItemSchema.Importance, Importance.High);
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ReceivedTime, exDateTime);
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.Importance, importance);
				return new AndFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
			}

			// Token: 0x040009B3 RID: 2483
			private static SortBy[] sort = new SortBy[]
			{
				new SortBy(ItemSchema.Importance, SortOrder.Descending),
				new SortBy(ItemSchema.ReceivedTime, SortOrder.Ascending)
			};
		}

		// Token: 0x0200017B RID: 379
		private class LifoVoicemailPager : MessageItemList.VoicemailPager
		{
			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00030B06 File Offset: 0x0002ED06
			protected override SortBy[] Sort
			{
				get
				{
					return MessageItemList.LifoVoicemailPager.sort;
				}
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x00030B10 File Offset: 0x0002ED10
			protected override QueryFilter BuildSeekFilter(MessageItemList list)
			{
				ExDateTime exDateTime = list.SafeGetProperty<ExDateTime>(list.lastViewRow, ItemSchema.ReceivedTime, ExDateTime.MaxValue);
				Importance importance = list.SafeGetProperty<Importance>(list.lastViewRow, ItemSchema.Importance, Importance.High);
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, exDateTime);
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.Importance, importance);
				return new AndFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
			}

			// Token: 0x040009B4 RID: 2484
			private static SortBy[] sort = new SortBy[]
			{
				new SortBy(ItemSchema.Importance, SortOrder.Descending),
				new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
			};
		}

		// Token: 0x0200017C RID: 380
		private class EmailPager : MessageItemList.MessageItemPager
		{
			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00030BC2 File Offset: 0x0002EDC2
			internal override PropertyDefinition[] RequiredProperties
			{
				get
				{
					return MessageItemList.EmailPager.myProps;
				}
			}

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00030BC9 File Offset: 0x0002EDC9
			protected override SortBy[] Sort
			{
				get
				{
					return MessageItemList.EmailPager.sort;
				}
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x00030BD0 File Offset: 0x0002EDD0
			protected override QueryFilter BuildSeekFilter(MessageItemList list)
			{
				ExDateTime exDateTime = list.SafeGetProperty<ExDateTime>(list.lastViewRow, ItemSchema.ReceivedTime, ExDateTime.MaxValue);
				return new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, exDateTime);
			}

			// Token: 0x040009B5 RID: 2485
			private static PropertyDefinition[] myProps = new PropertyDefinition[]
			{
				ItemSchema.ReceivedTime
			};

			// Token: 0x040009B6 RID: 2486
			private static SortBy[] sort = new SortBy[]
			{
				new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
			};
		}
	}
}
