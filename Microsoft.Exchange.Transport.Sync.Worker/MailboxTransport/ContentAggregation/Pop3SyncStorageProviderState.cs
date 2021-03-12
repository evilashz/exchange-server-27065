using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.Pop;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001FB RID: 507
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3SyncStorageProviderState : SyncStorageProviderState
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x00038058 File Offset: 0x00036258
		internal Pop3SyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery) : this(subscription, syncLogSession, underRecovery, AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(subscription.AggregationType), (long)AggregationConfiguration.Instance.GetMaxDownloadSizePerConnection(subscription.AggregationType).ToBytes(), AggregationConfiguration.Instance.MaxDownloadSizePerItem, AggregationConfiguration.Instance.RemoteConnectionTimeout, new EventHandler<DownloadCompleteEventArgs>(FrameworkPerfCounterHandler.Instance.OnPop3RetrieveMessageCompletion))
		{
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000380BC File Offset: 0x000362BC
		internal Pop3SyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery, int maxDownloadItemsPerConnection, long maxDownloadSizePerConnection, long maxDownloadSizePerItem, int connectionTimeout, EventHandler<DownloadCompleteEventArgs> downloadCompleted) : base(subscription, syncLogSession, underRecovery, downloadCompleted)
		{
			this.maxDownloadItemsPerConnection = maxDownloadItemsPerConnection;
			this.maxDownloadSizePerConnection = maxDownloadSizePerConnection;
			this.maxDownloadSizePerItem = maxDownloadSizePerItem;
			this.pop3Client = Pop3Client.FromSubscription(this.PopSubscription, connectionTimeout, syncLogSession, new EventHandler<DownloadCompleteEventArgs>(base.InternalOnDownloadCompletion), new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete));
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00038124 File Offset: 0x00036324
		internal PopAggregationSubscription PopSubscription
		{
			get
			{
				base.CheckDisposed();
				return (PopAggregationSubscription)base.Subscription;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00038137 File Offset: 0x00036337
		internal Pop3Client Pop3Client
		{
			get
			{
				base.CheckDisposed();
				return this.pop3Client;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00038145 File Offset: 0x00036345
		internal long MaxDownloadSizePerConnection
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadSizePerConnection;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00038153 File Offset: 0x00036353
		internal long MaxDownloadSizePerItem
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadSizePerItem;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00038161 File Offset: 0x00036361
		internal int MaxDownloadItemsPerConnection
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadItemsPerConnection;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0003816F File Offset: 0x0003636F
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x0003817D File Offset: 0x0003637D
		internal Pop3ResultData Pop3ResultData
		{
			get
			{
				base.CheckDisposed();
				return this.pop3ResultData;
			}
			set
			{
				base.CheckDisposed();
				this.pop3ResultData = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0003818C File Offset: 0x0003638C
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0003819A File Offset: 0x0003639A
		internal int EmailsWereAdded
		{
			get
			{
				base.CheckDisposed();
				return this.emailsWereAdded;
			}
			set
			{
				base.CheckDisposed();
				this.emailsWereAdded = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000381A9 File Offset: 0x000363A9
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x000381B7 File Offset: 0x000363B7
		internal int EmailsYetToCome
		{
			get
			{
				base.CheckDisposed();
				return this.emailsYetToCome;
			}
			set
			{
				base.CheckDisposed();
				this.emailsYetToCome = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x000381C6 File Offset: 0x000363C6
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x000381D4 File Offset: 0x000363D4
		internal Exception DeletionError
		{
			get
			{
				base.CheckDisposed();
				return this.deletionError;
			}
			set
			{
				base.CheckDisposed();
				this.deletionError = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000381E4 File Offset: 0x000363E4
		internal PopBookmark PendingDeletionItems
		{
			get
			{
				if (this.pendingDeletionItems == null)
				{
					string encoded;
					base.StateStorage.TryGetPropertyValue("PendingDeletionItems", out encoded);
					this.pendingDeletionItems = PopBookmark.Parse(encoded);
				}
				return this.pendingDeletionItems;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00038220 File Offset: 0x00036420
		internal string SyncWatermark
		{
			get
			{
				base.CheckDisposed();
				string result;
				this.PopWatermark.Load(out result);
				return result;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00038241 File Offset: 0x00036441
		internal StringWatermark PopWatermark
		{
			get
			{
				base.CheckDisposed();
				return (StringWatermark)base.BaseWatermark;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00038254 File Offset: 0x00036454
		internal bool UniqueIdsResultDataLoaded
		{
			get
			{
				base.CheckDisposed();
				return this.uniqueIdsResultDataLoaded;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00038262 File Offset: 0x00036462
		internal AsyncOperationResult<Pop3ResultData> CachedGetUniqueIdsResult
		{
			get
			{
				base.CheckDisposed();
				return this.cachedGetUniqueIdsResult;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00038270 File Offset: 0x00036470
		internal void CacheNewWatermark(string newWatermark)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("newWatermark", newWatermark);
			this.newSyncWatermark = newWatermark;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003828A File Offset: 0x0003648A
		internal void UpdateSubscriptionWithCurrentWaterMark()
		{
			base.CheckDisposed();
			this.PopWatermark.Save(this.newSyncWatermark);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000382A3 File Offset: 0x000364A3
		internal void CacheGetUniqueIdsResult(AsyncOperationResult<Pop3ResultData> getUniqueIdsResult)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("getUniqueIdsResult", getUniqueIdsResult);
			this.cachedGetUniqueIdsResult = getUniqueIdsResult;
			this.uniqueIdsResultDataLoaded = true;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000382C4 File Offset: 0x000364C4
		internal void PersistPendingDeletionItems()
		{
			if (this.pendingDeletionItems != null && this.pendingDeletionItems.HasChanged)
			{
				string value = this.pendingDeletionItems.ToString();
				if (base.StateStorage.ContainsProperty("PendingDeletionItems"))
				{
					base.StateStorage.ChangePropertyValue("PendingDeletionItems", value);
				}
				else
				{
					base.StateStorage.AddProperty("PendingDeletionItems", value);
				}
				this.pendingDeletionItems.HasChanged = false;
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00038334 File Offset: 0x00036534
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.pop3Client != null)
			{
				this.pop3Client.Dispose();
				this.pop3Client = null;
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00038353 File Offset: 0x00036553
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3SyncStorageProviderState>(this);
		}

		// Token: 0x04000982 RID: 2434
		private const string PendingDeletionItemsPropertyName = "PendingDeletionItems";

		// Token: 0x04000983 RID: 2435
		private readonly long maxDownloadSizePerConnection;

		// Token: 0x04000984 RID: 2436
		private readonly long maxDownloadSizePerItem;

		// Token: 0x04000985 RID: 2437
		private readonly int maxDownloadItemsPerConnection;

		// Token: 0x04000986 RID: 2438
		private Pop3ResultData pop3ResultData;

		// Token: 0x04000987 RID: 2439
		private int emailsYetToCome;

		// Token: 0x04000988 RID: 2440
		private int emailsWereAdded;

		// Token: 0x04000989 RID: 2441
		private Pop3Client pop3Client;

		// Token: 0x0400098A RID: 2442
		private PopBookmark pendingDeletionItems;

		// Token: 0x0400098B RID: 2443
		private Exception deletionError;

		// Token: 0x0400098C RID: 2444
		private bool uniqueIdsResultDataLoaded;

		// Token: 0x0400098D RID: 2445
		private AsyncOperationResult<Pop3ResultData> cachedGetUniqueIdsResult;

		// Token: 0x0400098E RID: 2446
		private string newSyncWatermark = string.Empty;
	}
}
