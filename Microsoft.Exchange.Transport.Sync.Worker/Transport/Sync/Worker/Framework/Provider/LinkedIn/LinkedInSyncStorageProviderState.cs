using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.LinkedIn
{
	// Token: 0x020001E8 RID: 488
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LinkedInSyncStorageProviderState : SyncStorageProviderState
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0003278D File Offset: 0x0003098D
		internal LinkedInSyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, LinkedInAppConfig linkedInConfig, ILinkedInWebClient webClient) : this(subscription, syncLogSession, linkedInConfig, webClient, AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(subscription.AggregationType))
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000327AA File Offset: 0x000309AA
		internal LinkedInSyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, LinkedInAppConfig linkedInConfig, ILinkedInWebClient webClient, int maxDownloadItemsPerConnection) : base(subscription, syncLogSession, false, new EventHandler<DownloadCompleteEventArgs>(FrameworkPerfCounterHandler.Instance.OnLinkedInSyncDownloadCompletion))
		{
			SyncUtilities.ThrowIfArgumentNull("linkedInConfig", linkedInConfig);
			this.config = linkedInConfig;
			this.client = webClient;
			this.maxDownloadItemsPerConnection = maxDownloadItemsPerConnection;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000327E7 File Offset: 0x000309E7
		internal LinkedInAppConfig Config
		{
			get
			{
				base.CheckDisposed();
				return this.config;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x000327F5 File Offset: 0x000309F5
		internal int MaxDownloadItemsPerConnection
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadItemsPerConnection;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00032803 File Offset: 0x00030A03
		internal ILinkedInWebClient LinkedInClient
		{
			get
			{
				base.CheckDisposed();
				if (this.Config != null)
				{
					return this.client;
				}
				return null;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0003281B File Offset: 0x00030A1B
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00032829 File Offset: 0x00030A29
		internal string CurrentWatermark
		{
			get
			{
				base.CheckDisposed();
				return this.currentWatermark;
			}
			set
			{
				base.CheckDisposed();
				this.currentWatermark = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00032838 File Offset: 0x00030A38
		internal string SyncWatermark
		{
			get
			{
				base.CheckDisposed();
				StringWatermark stringWatermark = (StringWatermark)base.BaseWatermark;
				string result;
				stringWatermark.Load(out result);
				return result;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00032860 File Offset: 0x00030A60
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003286E File Offset: 0x00030A6E
		internal LinkedInConnections CloudData
		{
			get
			{
				base.CheckDisposed();
				return this.cloudData;
			}
			set
			{
				base.CheckDisposed();
				this.cloudData = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003287D File Offset: 0x00030A7D
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003288B File Offset: 0x00030A8B
		internal bool MoreItemsAvailable
		{
			get
			{
				base.CheckDisposed();
				return this.moreItemsAvailable;
			}
			set
			{
				base.CheckDisposed();
				this.moreItemsAvailable = value;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003289A File Offset: 0x00030A9A
		internal void TriggerRequestEvent()
		{
			base.CheckDisposed();
			FrameworkPerfCounterHandler.Instance.OnLinkedInRequest();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000328AC File Offset: 0x00030AAC
		internal void TriggerContactDownloadEvent()
		{
			base.CheckDisposed();
			FrameworkPerfCounterHandler.Instance.OnLinkedInContactDownload();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000328BE File Offset: 0x00030ABE
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000328C0 File Offset: 0x00030AC0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LinkedInSyncStorageProviderState>(this);
		}

		// Token: 0x040008BB RID: 2235
		private readonly LinkedInAppConfig config;

		// Token: 0x040008BC RID: 2236
		private readonly ILinkedInWebClient client;

		// Token: 0x040008BD RID: 2237
		private readonly int maxDownloadItemsPerConnection;

		// Token: 0x040008BE RID: 2238
		private bool moreItemsAvailable;

		// Token: 0x040008BF RID: 2239
		private LinkedInConnections cloudData;

		// Token: 0x040008C0 RID: 2240
		private string currentWatermark;
	}
}
