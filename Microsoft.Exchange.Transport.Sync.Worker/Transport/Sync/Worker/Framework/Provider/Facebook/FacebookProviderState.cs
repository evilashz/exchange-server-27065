using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Facebook;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.Facebook
{
	// Token: 0x020001CB RID: 459
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FacebookProviderState : SyncStorageProviderState
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002075F File Offset: 0x0001E95F
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00020767 File Offset: 0x0001E967
		public IFacebookClient Client { get; private set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00020770 File Offset: 0x0001E970
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00020778 File Offset: 0x0001E978
		public string CurrentWatermark { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00020781 File Offset: 0x0001E981
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00020789 File Offset: 0x0001E989
		public List<FacebookUser> CloudUpdates { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00020792 File Offset: 0x0001E992
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0002079A File Offset: 0x0001E99A
		public int MaxDownloadItems { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000207A3 File Offset: 0x0001E9A3
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000207AB File Offset: 0x0001E9AB
		public bool MoreItemsAvailable { get; set; }

		// Token: 0x06000D66 RID: 3430 RVA: 0x000207B4 File Offset: 0x0001E9B4
		internal FacebookProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, IFacebookClient client) : this(subscription, syncLogSession, client, AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(subscription.AggregationType))
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000207D0 File Offset: 0x0001E9D0
		internal FacebookProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, IFacebookClient client, int maxDownloadItems) : base(subscription, syncLogSession, false, new EventHandler<DownloadCompleteEventArgs>(FrameworkPerfCounterHandler.Instance.OnFacebookSyncDownloadCompletion))
		{
			SyncUtilities.ThrowIfArgumentNull("client", client);
			this.Client = client;
			this.MaxDownloadItems = maxDownloadItems;
			this.Client.SubscribeDownloadCompletedEvent(new EventHandler<DownloadCompleteEventArgs>(base.InternalOnDownloadCompletion));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00020827 File Offset: 0x0001EA27
		internal void TriggerRequestEvent()
		{
			base.CheckDisposed();
			FrameworkPerfCounterHandler.Instance.OnFacebookRequest(this, null);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002083B File Offset: 0x0001EA3B
		internal void TriggerRequestEventWithNoChanges()
		{
			base.CheckDisposed();
			FrameworkPerfCounterHandler.Instance.OnFacebookRequestWithNoChanges(this, null);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002084F File Offset: 0x0001EA4F
		internal void TriggerContactDownloadEvent()
		{
			base.CheckDisposed();
			FrameworkPerfCounterHandler.Instance.OnFacebookContactDownload(this, null);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00020863 File Offset: 0x0001EA63
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00020865 File Offset: 0x0001EA65
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FacebookProviderState>(this);
		}
	}
}
