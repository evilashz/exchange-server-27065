using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200033E RID: 830
	internal class FolderListViewDataSource : ExchangeListViewDataSource, IListViewDataSource
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x000B4B77 File Offset: 0x000B2D77
		public FolderListViewDataSource(UserContext context, Hashtable properties, Folder folder, SortBy[] sortBy) : this(context, false, properties, folder, sortBy, null)
		{
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000B4B86 File Offset: 0x000B2D86
		public FolderListViewDataSource(UserContext context, Hashtable properties, Folder folder, SortBy[] sortBy, QueryFilter filter) : this(context, false, properties, folder, sortBy, filter)
		{
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000B4B96 File Offset: 0x000B2D96
		public FolderListViewDataSource(UserContext context, bool conversationMode, Hashtable properties, Folder folder, SortBy[] sortBy) : this(context, conversationMode, properties, folder, sortBy, null)
		{
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000B4BA6 File Offset: 0x000B2DA6
		public FolderListViewDataSource(UserContext context, bool conversationMode, Hashtable properties, Folder folder, SortBy[] sortBy, QueryFilter filter)
		{
			this.shouldDisposeQueryResult = true;
			base..ctor(properties);
			this.folder = folder;
			this.sortBy = sortBy;
			this.userContext = context;
			this.filterQuery = filter;
			this.conversationMode = conversationMode;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000B4BDC File Offset: 0x000B2DDC
		public FolderListViewDataSource(UserContext context, bool conversationMode, Hashtable properties, Folder folder, SortBy[] sortBy, QueryFilter filter, QueryResult queryResult, bool shouldDisposeQueryResult)
		{
			this.shouldDisposeQueryResult = true;
			base..ctor(properties);
			this.folder = folder;
			this.sortBy = sortBy;
			this.userContext = context;
			this.filterQuery = filter;
			this.conversationMode = conversationMode;
			this.queryResult = queryResult;
			this.shouldDisposeQueryResult = shouldDisposeQueryResult;
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x000B4C2D File Offset: 0x000B2E2D
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000B4C35 File Offset: 0x000B2E35
		internal override QueryResult QueryResult
		{
			get
			{
				return this.queryResult;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000B4C3D File Offset: 0x000B2E3D
		public SortBy[] SortBy
		{
			get
			{
				return this.sortBy;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000B4C45 File Offset: 0x000B2E45
		public override int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x000B4C4D File Offset: 0x000B2E4D
		public override int TotalItemCount
		{
			get
			{
				if (this.conversationMode)
				{
					return this.folder.ItemCount;
				}
				return base.TotalItemCount;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000B4C69 File Offset: 0x000B2E69
		public int UnreadCount
		{
			get
			{
				if (this.folder.TryGetProperty(FolderSchema.UnreadCount) is int)
				{
					return (int)this.folder[FolderSchema.UnreadCount];
				}
				return 0;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x000B4C99 File Offset: 0x000B2E99
		public string ContainerId
		{
			get
			{
				return Utilities.GetIdAsString(this.folder);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x000B4CA8 File Offset: 0x000B2EA8
		public bool UserHasRightToLoad
		{
			get
			{
				if (!Utilities.IsPublic(this.Folder))
				{
					this.hasRightToLoad = new bool?(true);
				}
				if (this.hasRightToLoad == null)
				{
					object obj = this.Folder.TryGetProperty(StoreObjectSchema.EffectiveRights);
					EffectiveRights effectiveRights = (EffectiveRights)obj;
					this.hasRightToLoad = new bool?((effectiveRights & EffectiveRights.Read) == EffectiveRights.Read);
				}
				return this.hasRightToLoad.Value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x000B4D0F File Offset: 0x000B2F0F
		public OwaStoreObjectId NewSelectionId
		{
			get
			{
				if (this.conversationMode)
				{
					return this.newSelectionId;
				}
				return null;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000B4D21 File Offset: 0x000B2F21
		protected override bool IsPreviousItemLoaded
		{
			get
			{
				return this.isPreviousItemLoaded;
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000B4D2C File Offset: 0x000B2F2C
		public bool IsSearchInProgress()
		{
			bool result = false;
			SearchFolder searchFolder = this.Folder as SearchFolder;
			if (searchFolder != null && this.userContext.IsPushNotificationsEnabled)
			{
				result = this.userContext.MapiNotificationManager.IsSearchInProgress((MailboxSession)searchFolder.Session, searchFolder.StoreObjectId);
			}
			return result;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000B4D7C File Offset: 0x000B2F7C
		public void Load(string seekValue, int itemCount)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderListViewDataSource.Load(string seekValue, int itemCount)");
			if (itemCount < 1)
			{
				throw new ArgumentOutOfRangeException("itemCount", "itemCount must be greater than 0");
			}
			if (seekValue == null)
			{
				throw new ArgumentNullException("seekValue");
			}
			if (!this.UserHasRightToLoad)
			{
				return;
			}
			PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
			if (this.folder.ItemCount == 0)
			{
				return;
			}
			bool flag = false;
			try
			{
				if (this.queryResult == null)
				{
					this.queryResult = this.CreateQueryResult(this.filterQuery, this.sortBy, requestedProperties);
				}
				PropertyDefinition property = this.sortBy[0].ColumnDefinition;
				if (this.sortBy[0].ColumnDefinition == ItemSchema.Subject)
				{
					property = ItemSchema.NormalizedSubject;
				}
				if (this.sortBy[0].SortOrder == SortOrder.Ascending)
				{
					this.queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, property, seekValue), SeekToConditionFlags.None);
				}
				else
				{
					int length = seekValue.Length;
					char[] array = seekValue.ToCharArray();
					char[] array2 = array;
					int num = length - 1;
					array2[num] += '\u0001';
					string propertyValue = new string(array);
					this.queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.LessThan, property, propertyValue), SeekToConditionFlags.None);
				}
				if (this.queryResult.EstimatedRowCount == this.queryResult.CurrentRow)
				{
					this.queryResult.SeekToOffset(SeekReference.OriginCurrent, -1 * itemCount);
				}
				this.GetView(this.queryResult, itemCount, this.queryResult.CurrentRow);
				flag = true;
			}
			finally
			{
				if (!flag || this.shouldDisposeQueryResult)
				{
					this.DisposeQueryResultIfPresent();
				}
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000B4F00 File Offset: 0x000B3100
		public bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			return this.LoadById(adjacentObjectId, seekDirection, itemCount, true);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000B4F0C File Offset: 0x000B310C
		public void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount)
		{
			this.LoadById(seekToObjectId, seekDirection, itemCount, false);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000B4F1C File Offset: 0x000B311C
		private bool LoadById(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount, bool adjacent)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderListViewDataSource.Load(IUniqueItemId seekToItemId, SeekDirection seekDirection, int itemCount)");
			if (itemCount < 1)
			{
				throw new ArgumentOutOfRangeException("itemCount", "itemCount must be greater than 0");
			}
			if (seekToObjectId == null)
			{
				throw new ArgumentNullException("seekToObjectId");
			}
			if (!this.UserHasRightToLoad)
			{
				return true;
			}
			StoreId storeId = Utilities.TryGetStoreId(seekToObjectId);
			PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
			if (this.folder.ItemCount == 0)
			{
				return true;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				if (this.queryResult == null)
				{
					this.queryResult = this.CreateQueryResult(this.filterQuery, this.sortBy, requestedProperties);
				}
				if (!this.conversationMode)
				{
					StoreObjectId storeObjectId = storeId as StoreObjectId;
					if (storeObjectId != null)
					{
						flag2 = this.queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, storeObjectId));
					}
				}
				else
				{
					ConversationId conversationId = storeId as ConversationId;
					if (conversationId != null)
					{
						if (adjacent)
						{
							OwaStoreObjectId owaStoreObjectId = seekToObjectId as OwaStoreObjectId;
							if (owaStoreObjectId.InstanceKey != null)
							{
								flag2 = this.queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InstanceKey, owaStoreObjectId.InstanceKey));
							}
						}
						else
						{
							flag2 = this.queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ConversationItemSchema.ConversationId, conversationId));
							if (flag2)
							{
								IStorePropertyBag[] propertyBags = this.queryResult.GetPropertyBags(1);
								byte[] instanceKey = propertyBags[0].TryGetProperty(ItemSchema.InstanceKey) as byte[];
								this.newSelectionId = OwaStoreObjectId.CreateFromConversationId(conversationId, this.Folder, instanceKey);
								this.queryResult.SeekToOffset(SeekReference.OriginCurrent, -1);
							}
						}
					}
				}
				if (adjacent && !flag2)
				{
					return false;
				}
				if (!flag2)
				{
					this.queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				}
				switch (seekDirection)
				{
				case SeekDirection.Next:
					if (adjacent)
					{
						this.queryResult.SeekToOffset(SeekReference.OriginCurrent, 1);
					}
					else if (this.queryResult.EstimatedRowCount < this.queryResult.CurrentRow + itemCount + 1)
					{
						this.queryResult.SeekToOffset(SeekReference.OriginCurrent, this.queryResult.EstimatedRowCount - this.queryResult.CurrentRow - itemCount);
					}
					break;
				case SeekDirection.Previous:
				{
					int offset;
					if (adjacent)
					{
						if (this.queryResult.CurrentRow == 0)
						{
							return true;
						}
						if (this.queryResult.CurrentRow < itemCount)
						{
							itemCount = this.queryResult.CurrentRow;
						}
						offset = -1 * itemCount;
					}
					else if (this.queryResult.CurrentRow + 1 < itemCount)
					{
						offset = -1 * (this.queryResult.CurrentRow + 1);
					}
					else
					{
						offset = 1 - itemCount;
					}
					this.queryResult.SeekToOffset(SeekReference.OriginCurrent, offset);
					break;
				}
				}
				this.GetView(this.queryResult, itemCount, this.queryResult.CurrentRow);
				flag = true;
			}
			finally
			{
				if (!flag || this.shouldDisposeQueryResult)
				{
					this.DisposeQueryResultIfPresent();
				}
			}
			return true;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000B51E4 File Offset: 0x000B33E4
		public void Load(int startRange, int itemCount)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderListViewDataSource.Load(int startRange, int itemCount)");
			if (startRange < 0)
			{
				throw new ArgumentOutOfRangeException("startRange", "Start range (startRange) must be greater than 0");
			}
			if (itemCount < 1)
			{
				throw new ArgumentOutOfRangeException("itemCount", "itemCount must be greater than 0");
			}
			if (!this.UserHasRightToLoad)
			{
				return;
			}
			PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
			if (this.folder.ItemCount <= startRange)
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Requested start range is greater than the number of items in the folder, back up to last page");
				startRange = this.folder.ItemCount - itemCount;
				if (startRange < 0)
				{
					startRange = 0;
				}
			}
			if (this.folder.ItemCount == 0)
			{
				return;
			}
			bool flag = false;
			try
			{
				if (this.queryResult == null)
				{
					this.queryResult = this.CreateQueryResult(this.filterQuery, this.sortBy, requestedProperties);
				}
				int currentRow = 0;
				if (startRange != 0 && startRange < this.queryResult.EstimatedRowCount)
				{
					currentRow = this.queryResult.SeekToOffset(SeekReference.OriginCurrent, startRange);
				}
				this.GetView(this.queryResult, itemCount, currentRow);
				flag = true;
			}
			finally
			{
				if (!flag || this.shouldDisposeQueryResult)
				{
					this.DisposeQueryResultIfPresent();
				}
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000B5300 File Offset: 0x000B3500
		private void GetView(QueryResult queryResult, int itemCount, int currentRow)
		{
			if (currentRow > 0)
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, currentRow - 1);
				itemCount++;
				this.isPreviousItemLoaded = true;
			}
			try
			{
				this.totalCount = queryResult.EstimatedRowCount;
			}
			catch (AccessDeniedException)
			{
				if (Utilities.IsWebPartDelegateAccessRequest(OwaContext.Current))
				{
					this.totalCount = 0;
					base.Items = new object[0][];
					return;
				}
				throw;
			}
			object[][] array = Utilities.FetchRowsFromQueryResult(queryResult, itemCount);
			if (0 < array.Length)
			{
				base.StartRange = currentRow;
				base.EndRange = currentRow + array.Length - 1;
				if (this.isPreviousItemLoaded)
				{
					base.EndRange--;
				}
			}
			base.Items = array;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000B53AC File Offset: 0x000B35AC
		public string GetItemId()
		{
			if (this.conversationMode)
			{
				ConversationId itemProperty = this.GetItemProperty<ConversationId>(ConversationItemSchema.ConversationId);
				byte[] itemProperty2 = this.GetItemProperty<byte[]>(ItemSchema.InstanceKey);
				if (itemProperty != null)
				{
					return OwaStoreObjectId.CreateFromConversationId(itemProperty, this.Folder, itemProperty2).ToString();
				}
				return null;
			}
			else
			{
				VersionedId itemProperty3 = this.GetItemProperty<VersionedId>(ItemSchema.Id);
				if (itemProperty3 != null)
				{
					return Utilities.GetItemIdString(itemProperty3.ObjectId, this.Folder);
				}
				return null;
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000B5414 File Offset: 0x000B3614
		public string GetChangeKey()
		{
			VersionedId itemProperty = this.GetItemProperty<VersionedId>(ItemSchema.Id);
			return itemProperty.ChangeKeyAsBase64String();
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000B5434 File Offset: 0x000B3634
		public string GetItemClass()
		{
			if (this.conversationMode)
			{
				return "IPM.Conversation";
			}
			string text = this.GetItemProperty<string>(StoreObjectSchema.ItemClass, null);
			if (string.IsNullOrEmpty(text))
			{
				text = "IPM.Unknown";
			}
			return text;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000B546B File Offset: 0x000B366B
		private QueryResult CreateQueryResult(QueryFilter filterQuery, SortBy[] sortBy, PropertyDefinition[] requestedProperties)
		{
			if (this.conversationMode)
			{
				return this.folder.ConversationItemQuery(filterQuery, sortBy, requestedProperties);
			}
			return this.folder.ItemQuery(ItemQueryType.None, filterQuery, sortBy, requestedProperties);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000B5493 File Offset: 0x000B3693
		private void DisposeQueryResultIfPresent()
		{
			if (this.queryResult != null)
			{
				this.queryResult.Dispose();
				this.queryResult = null;
			}
		}

		// Token: 0x040016D9 RID: 5849
		protected int totalCount;

		// Token: 0x040016DA RID: 5850
		private Folder folder;

		// Token: 0x040016DB RID: 5851
		private SortBy[] sortBy;

		// Token: 0x040016DC RID: 5852
		private UserContext userContext;

		// Token: 0x040016DD RID: 5853
		private QueryResult queryResult;

		// Token: 0x040016DE RID: 5854
		private bool shouldDisposeQueryResult;

		// Token: 0x040016DF RID: 5855
		private QueryFilter filterQuery;

		// Token: 0x040016E0 RID: 5856
		private bool? hasRightToLoad;

		// Token: 0x040016E1 RID: 5857
		private bool isPreviousItemLoaded;

		// Token: 0x040016E2 RID: 5858
		private bool conversationMode;

		// Token: 0x040016E3 RID: 5859
		private OwaStoreObjectId newSelectionId;
	}
}
