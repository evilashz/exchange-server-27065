using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200000A RID: 10
	internal class DataAccessView : DisposeTrackableBase
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000044F8 File Offset: 0x000026F8
		public DataAccessView(ResponseFactory factory, Folder folder)
		{
			this.factory = factory;
			this.uidCache = new Queue<DataAccessView.UidPair>(DataAccessView.uidCacheSize);
			IList<PropertyDefinition> list;
			if (this.AdditionalProperties == null)
			{
				list = DataAccessView.ViewProperties;
			}
			else
			{
				list = new List<PropertyDefinition>(DataAccessView.ViewProperties);
				((List<PropertyDefinition>)list).AddRange(this.AdditionalProperties);
			}
			this.view = folder.ItemQuery(ItemQueryType.None, null, this.SortOrders, list);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004563 File Offset: 0x00002763
		public QueryResult TableView
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000456B File Offset: 0x0000276B
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004572 File Offset: 0x00002772
		protected static SortBy[] SortById
		{
			get
			{
				return DataAccessView.sortById;
			}
			set
			{
				DataAccessView.sortById = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000457A File Offset: 0x0000277A
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004581 File Offset: 0x00002781
		protected static int UidCacheSize
		{
			get
			{
				return DataAccessView.uidCacheSize;
			}
			set
			{
				DataAccessView.uidCacheSize = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004589 File Offset: 0x00002789
		protected virtual PropertyDefinition[] AdditionalProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000458C File Offset: 0x0000278C
		protected virtual SortBy[] SortOrders
		{
			get
			{
				return DataAccessView.sortById;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004594 File Offset: 0x00002794
		public void SetPoisonFlag(Folder folder, int imapId, bool isPoison)
		{
			StoreObjectId storeObjectId = this.GetStoreObjectId(imapId);
			if (storeObjectId != null)
			{
				try
				{
					folder.SetItemStatus(storeObjectId, isPoison ? MessageStatusFlags.MimeConversionFailed : MessageStatusFlags.None, MessageStatusFlags.MimeConversionFailed);
				}
				catch (LocalizedException)
				{
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045DC File Offset: 0x000027DC
		public StoreObjectId GetStoreObjectId(int imapId)
		{
			StoreObjectId storeObjectId = this.GetStoreObjectIdFromCache(imapId);
			if (storeObjectId != null)
			{
				return storeObjectId;
			}
			lock (this.factory.Store)
			{
				bool flag2 = false;
				try
				{
					bool flag3 = this.factory.Store.IsDead;
					if (flag3)
					{
						this.HandleInvalidateStore();
					}
					if (!this.factory.IsStoreConnected)
					{
						flag3 = this.factory.ConnectToTheStore();
						flag2 = true;
						if (flag3)
						{
							this.HandleInvalidateStore();
						}
					}
					storeObjectId = this.GetStoreObjectIdFromBE(imapId);
				}
				finally
				{
					if (flag2)
					{
						this.factory.DisconnectFromTheStore();
					}
				}
			}
			return storeObjectId;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004690 File Offset: 0x00002890
		public StoreObjectId[] GetStoreObjectIds(IList<ProtocolMessage> sortedMessages)
		{
			int[] array = new int[sortedMessages.Count];
			StoreObjectId[] array2 = new StoreObjectId[sortedMessages.Count];
			bool flag = false;
			for (int i = 0; i < sortedMessages.Count; i++)
			{
				ProtocolMessage protocolMessage = sortedMessages[i];
				array[i] = protocolMessage.Id;
				if (!flag)
				{
					StoreObjectId storeObjectIdFromCache = this.GetStoreObjectIdFromCache(protocolMessage.Id);
					if (storeObjectIdFromCache == null)
					{
						flag = true;
					}
					else
					{
						array2[i] = storeObjectIdFromCache;
					}
				}
			}
			if (flag)
			{
				lock (this.factory.Store)
				{
					bool flag3 = false;
					try
					{
						bool flag4 = this.factory.Store.IsDead;
						if (flag4)
						{
							this.HandleInvalidateStore();
						}
						if (!this.factory.IsStoreConnected)
						{
							flag4 = this.factory.ConnectToTheStore();
							flag3 = true;
							if (flag4)
							{
								this.HandleInvalidateStore();
							}
						}
						this.GetStoreObjectIdsFromBE(array, array2);
					}
					finally
					{
						if (flag3)
						{
							this.factory.DisconnectFromTheStore();
						}
					}
				}
			}
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j] == null)
				{
					sortedMessages[j].IsDeleted = true;
				}
			}
			return array2;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000047CC File Offset: 0x000029CC
		protected void AddStoreObjectIdToCache(StoreObjectId storeObjectId, int imapId)
		{
			if (this.uidCache.Count >= DataAccessView.uidCacheSize)
			{
				this.uidCache.Dequeue();
			}
			this.uidCache.Enqueue(new DataAccessView.UidPair(storeObjectId, imapId));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004800 File Offset: 0x00002A00
		protected override void InternalDispose(bool isDisposing)
		{
			if (this.view != null)
			{
				try
				{
					this.view.Dispose();
				}
				catch (LocalizedException ex)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.factory.Session.SessionId, "Exception caught while disposing DataAccessView. {0}", ex.ToString());
				}
				this.view = null;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004864 File Offset: 0x00002A64
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DataAccessView>(this);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000486C File Offset: 0x00002A6C
		private static int GetUidCacheSizeFromConfig()
		{
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["UidCacheSize"], out num) || num <= 0)
			{
				num = 30;
			}
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004899 File Offset: 0x00002A99
		private void HandleInvalidateStore()
		{
			this.factory.NeedToReloadStoreStates = true;
			throw new StorageTransientException(new LocalizedString("Current MailboxSession is dead"));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000048B8 File Offset: 0x00002AB8
		private StoreObjectId GetStoreObjectIdFromCache(int imapId)
		{
			foreach (DataAccessView.UidPair uidPair in this.uidCache)
			{
				if (uidPair.ImapId == imapId)
				{
					return uidPair.StoreObjectId;
				}
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000491C File Offset: 0x00002B1C
		private StoreObjectId GetStoreObjectIdFromBE(int imapId)
		{
			StoreObjectId result = null;
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ImapId, imapId + DataAccessView.uidCacheSize / 2);
			this.view.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
			int num = DataAccessView.uidCacheSize;
			object[][] rows;
			do
			{
				rows = this.view.GetRows(num);
				for (int i = 0; i < rows.Length; i++)
				{
					int num2 = (int)rows[i][1];
					StoreObjectId objectId = ((VersionedId)rows[i][0]).ObjectId;
					if (num2 == imapId)
					{
						result = ((VersionedId)rows[i][0]).ObjectId;
					}
					this.AddStoreObjectIdToCache(objectId, num2);
				}
				num -= rows.Length;
			}
			while (num > 0 && rows.Length > 0);
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000049C8 File Offset: 0x00002BC8
		private void GetStoreObjectIdsFromBE(int[] sortedImapIds, StoreObjectId[] returnList)
		{
			int num = sortedImapIds[0] - DataAccessView.uidCacheSize / 2;
			int num2 = sortedImapIds[sortedImapIds.Length - 1] + DataAccessView.uidCacheSize / 2;
			int rowCount = (sortedImapIds.Length + DataAccessView.uidCacheSize < 10000) ? (sortedImapIds.Length + DataAccessView.uidCacheSize) : 10000;
			bool flag = true;
			int num3 = sortedImapIds.Length - 1;
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ImapId, num2);
			this.view.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
			object[][] rows;
			do
			{
				rows = this.view.GetRows(rowCount);
				for (int i = 0; i < rows.Length; i++)
				{
					int num4 = (int)rows[i][1];
					StoreObjectId objectId = ((VersionedId)rows[i][0]).ObjectId;
					this.AddStoreObjectIdToCache(objectId, num4);
					if (num3 >= 0 && num4 == sortedImapIds[num3])
					{
						returnList[num3] = objectId;
						num3--;
					}
					if (num4 <= num)
					{
						flag = false;
						break;
					}
				}
			}
			while (rows.Length > 0 && flag);
		}

		// Token: 0x04000056 RID: 86
		protected static readonly PropertyDefinition[] ViewProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ImapId
		};

		// Token: 0x04000057 RID: 87
		private static SortBy[] sortById = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Descending)
		};

		// Token: 0x04000058 RID: 88
		private static int uidCacheSize = DataAccessView.GetUidCacheSizeFromConfig();

		// Token: 0x04000059 RID: 89
		private QueryResult view;

		// Token: 0x0400005A RID: 90
		private ResponseFactory factory;

		// Token: 0x0400005B RID: 91
		private Queue<DataAccessView.UidPair> uidCache;

		// Token: 0x0200000B RID: 11
		protected internal struct ViewPropertyIndex
		{
			// Token: 0x0400005C RID: 92
			public const int VersionedId = 0;

			// Token: 0x0400005D RID: 93
			public const int ImapId = 1;

			// Token: 0x0400005E RID: 94
			public const int MaxIndex = 1;
		}

		// Token: 0x0200000C RID: 12
		private struct UidPair
		{
			// Token: 0x060000E4 RID: 228 RVA: 0x00004B07 File Offset: 0x00002D07
			public UidPair(StoreObjectId storeObjectId, int imapId)
			{
				this.StoreObjectId = storeObjectId;
				this.ImapId = imapId;
			}

			// Token: 0x0400005F RID: 95
			public StoreObjectId StoreObjectId;

			// Token: 0x04000060 RID: 96
			public int ImapId;
		}
	}
}
