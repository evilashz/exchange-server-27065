using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200026A RID: 618
	internal class RecipientInfoCacheSyncProvider : ISyncProvider, IDisposeTrackable, IDisposable
	{
		// Token: 0x060016F2 RID: 5874 RVA: 0x00089BB8 File Offset: 0x00087DB8
		public RecipientInfoCacheSyncProvider(MailboxSession mailboxSession, int maxEntries)
		{
			if (maxEntries <= 0)
			{
				throw new ArgumentException("MaxEntries must be a positive number");
			}
			this.recipientInfoCache = RecipientInfoCache.Create(mailboxSession, "OWA.AutocompleteCache");
			try
			{
				this.entryList = this.recipientInfoCache.Load("AutoCompleteCache");
				for (int i = this.entryList.Count - 1; i >= 0; i--)
				{
					if (string.IsNullOrEmpty(this.entryList[i].SmtpAddress) || (!string.Equals(this.entryList[i].RoutingType, "SMTP", StringComparison.OrdinalIgnoreCase) && !string.Equals(this.entryList[i].RoutingType, "EX", StringComparison.OrdinalIgnoreCase)))
					{
						this.entryList.RemoveAt(i);
					}
				}
			}
			catch (CorruptDataException)
			{
				this.entryList = new List<RecipientInfoCacheEntry>(0);
				this.recipientInfoCache.Save(this.entryList, "AutoCompleteCache", 100);
			}
			this.maxNumEntries = maxEntries;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00089CD0 File Offset: 0x00087ED0
		public ISyncLogger SyncLogger
		{
			get
			{
				return AirSyncDiagnostics.GetSyncLogger();
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00089CD7 File Offset: 0x00087ED7
		internal StoreObjectId ItemId
		{
			get
			{
				return this.recipientInfoCache.ItemId;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00089CE4 File Offset: 0x00087EE4
		private Dictionary<RecipientInfoCacheSyncItemId, RecipientInfoCacheSyncItem> FastCache
		{
			get
			{
				if (this.fastCacheNotDirectlyUsed == null)
				{
					this.fastCacheNotDirectlyUsed = new Dictionary<RecipientInfoCacheSyncItemId, RecipientInfoCacheSyncItem>(this.entryList.Count);
					foreach (RecipientInfoCacheEntry item in this.entryList)
					{
						RecipientInfoCacheSyncItem recipientInfoCacheSyncItem = RecipientInfoCacheSyncItem.Bind(item);
						this.fastCacheNotDirectlyUsed[(RecipientInfoCacheSyncItemId)recipientInfoCacheSyncItem.Id] = recipientInfoCacheSyncItem;
					}
				}
				return this.fastCacheNotDirectlyUsed;
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00089D74 File Offset: 0x00087F74
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientInfoCacheSyncProvider>(this);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00089D7C File Offset: 0x00087F7C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00089D91 File Offset: 0x00087F91
		public void BindToFolderSync(FolderSync folderSync)
		{
			this.CheckDisposed("BindToFolderSync");
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00089D9E File Offset: 0x00087F9E
		public ISyncWatermark CreateNewWatermark()
		{
			this.CheckDisposed("CreateNewWatermark");
			return RecipientInfoCacheSyncWatermark.Create();
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00089DB0 File Offset: 0x00087FB0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00089DBF File Offset: 0x00087FBF
		public void DisposeNewOperationsCachedData()
		{
			this.CheckDisposed("DisposeNewOperationsCachedData");
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00089DCC File Offset: 0x00087FCC
		public ISyncItem GetItem(ISyncItemId id, params PropertyDefinition[] specifiedPrefetchProperties)
		{
			this.CheckDisposed("GetItem");
			RecipientInfoCacheSyncItemId recipientInfoCacheSyncItemId = id as RecipientInfoCacheSyncItemId;
			if (recipientInfoCacheSyncItemId == null)
			{
				return null;
			}
			if (this.FastCache.ContainsKey(recipientInfoCacheSyncItemId))
			{
				return this.FastCache[recipientInfoCacheSyncItemId];
			}
			return null;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00089E0C File Offset: 0x0008800C
		public ISyncWatermark GetMaxItemWatermark(ISyncWatermark currentWatermark)
		{
			this.CheckDisposed("GetMaxItemWatermark");
			if (this.entryList.Count <= this.maxNumEntries)
			{
				return RecipientInfoCacheSyncWatermark.Create(this.entryList, this.recipientInfoCache.LastModifiedTime);
			}
			if (!this.entryListIsSorted)
			{
				this.entryList.Sort();
				this.entryListIsSorted = true;
			}
			return RecipientInfoCacheSyncWatermark.Create(this.entryList.GetRange(this.entryList.Count - this.maxNumEntries, this.maxNumEntries), this.recipientInfoCache.LastModifiedTime);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00089E9C File Offset: 0x0008809C
		public bool GetNewOperations(ISyncWatermark minSyncWatermark, ISyncWatermark maxSyncWatermark, bool enumerateDeletes, int numOperations, QueryFilter filter, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			this.CheckDisposed("GetNewOperations");
			AirSyncDiagnostics.TraceInfo<int>(ExTraceGlobals.SyncTracer, this, "RecipientInfoCacheSyncProvider.GetNewOperations. numOperations = {0}", numOperations);
			if (newServerManifest == null)
			{
				throw new ArgumentNullException("newServerManifest");
			}
			if (!enumerateDeletes)
			{
				throw new NotImplementedException("enumerateDeletes is false!");
			}
			if (filter != null)
			{
				throw new NotImplementedException("filter is non-null! Filters are not supported on RecipientInfoCacheSyncProvider");
			}
			return this.ComputeNewItems(minSyncWatermark as RecipientInfoCacheSyncWatermark, maxSyncWatermark as RecipientInfoCacheSyncWatermark, numOperations, newServerManifest);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00089F08 File Offset: 0x00088108
		public OperationResult DeleteItems(params ISyncItemId[] syncIds)
		{
			return OperationResult.Failed;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00089F0B File Offset: 0x0008810B
		public List<IConversationTreeNode> GetInFolderItemsForConversation(ConversationId conversationId)
		{
			this.CheckDisposed("GetInFolderItemsForConversation");
			return null;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00089F19 File Offset: 0x00088119
		public ISyncItemId CreateISyncItemIdForNewItem(StoreObjectId itemId)
		{
			this.CheckDisposed("CreateISyncItemIdForNewItem");
			return null;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00089F28 File Offset: 0x00088128
		private bool ComputeNewItems(RecipientInfoCacheSyncWatermark minWatermark, RecipientInfoCacheSyncWatermark maxWatermark, int numOperations, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			if (minWatermark == null)
			{
				throw new ArgumentNullException("minWatermark");
			}
			if (maxWatermark == null)
			{
				maxWatermark = (RecipientInfoCacheSyncWatermark)this.GetMaxItemWatermark(null);
			}
			List<RecipientInfoCacheSyncItemId> list = new List<RecipientInfoCacheSyncItemId>(minWatermark.Entries.Count);
			bool flag = false;
			foreach (KeyValuePair<RecipientInfoCacheSyncItemId, long> keyValuePair in minWatermark.Entries)
			{
				if (!maxWatermark.Entries.ContainsKey(keyValuePair.Key))
				{
					if (numOperations != -1 && newServerManifest.Count >= numOperations)
					{
						flag = true;
						break;
					}
					ServerManifestEntry serverManifestEntry = new ServerManifestEntry(keyValuePair.Key);
					serverManifestEntry.ChangeType = ChangeType.Delete;
					newServerManifest[keyValuePair.Key] = serverManifestEntry;
					list.Add(keyValuePair.Key);
				}
			}
			foreach (RecipientInfoCacheSyncItemId key in list)
			{
				minWatermark.Entries.Remove(key);
			}
			if (flag)
			{
				return true;
			}
			foreach (KeyValuePair<RecipientInfoCacheSyncItemId, long> keyValuePair2 in maxWatermark.Entries)
			{
				bool flag2 = !minWatermark.Entries.ContainsKey(keyValuePair2.Key) || minWatermark.Entries[keyValuePair2.Key].CompareTo(keyValuePair2.Value) != 0;
				if (flag2)
				{
					if (numOperations != -1 && newServerManifest.Count >= numOperations)
					{
						return true;
					}
					minWatermark.Entries[keyValuePair2.Key] = keyValuePair2.Value;
					ServerManifestEntry serverManifestEntry2 = new ServerManifestEntry(keyValuePair2.Key);
					serverManifestEntry2.ChangeType = ChangeType.Add;
					Dictionary<RecipientInfoCacheSyncItemId, long> dictionary = new Dictionary<RecipientInfoCacheSyncItemId, long>(1);
					dictionary[keyValuePair2.Key] = keyValuePair2.Value;
					serverManifestEntry2.Watermark = RecipientInfoCacheSyncWatermark.Create(dictionary, maxWatermark.LastModifiedTime);
					newServerManifest[keyValuePair2.Key] = serverManifestEntry2;
				}
			}
			minWatermark.LastModifiedTime = maxWatermark.LastModifiedTime;
			return false;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0008A16C File Offset: 0x0008836C
		private void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0008A187 File Offset: 0x00088387
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0008A1A0 File Offset: 0x000883A0
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.recipientInfoCache != null)
				{
					this.recipientInfoCache.Dispose();
					this.recipientInfoCache = null;
				}
				if (this.fastCacheNotDirectlyUsed != null)
				{
					foreach (RecipientInfoCacheSyncItem recipientInfoCacheSyncItem in this.fastCacheNotDirectlyUsed.Values)
					{
						recipientInfoCacheSyncItem.Dispose();
					}
					this.fastCacheNotDirectlyUsed.Clear();
					this.fastCacheNotDirectlyUsed = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x04000E1C RID: 3612
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000E1D RID: 3613
		private int maxNumEntries = int.MaxValue;

		// Token: 0x04000E1E RID: 3614
		private bool disposed;

		// Token: 0x04000E1F RID: 3615
		private RecipientInfoCache recipientInfoCache;

		// Token: 0x04000E20 RID: 3616
		private List<RecipientInfoCacheEntry> entryList;

		// Token: 0x04000E21 RID: 3617
		private bool entryListIsSorted;

		// Token: 0x04000E22 RID: 3618
		private Dictionary<RecipientInfoCacheSyncItemId, RecipientInfoCacheSyncItem> fastCacheNotDirectlyUsed;
	}
}
