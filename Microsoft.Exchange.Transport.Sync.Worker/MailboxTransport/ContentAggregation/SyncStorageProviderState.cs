using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SyncStorageProviderState : SyncStorageProviderStateBase
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00014408 File Offset: 0x00012608
		internal SyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery, EventHandler<DownloadCompleteEventArgs> downloadCompleted) : base(subscription, syncLogSession, underRecovery)
		{
			this.DownloadCompleted += downloadCompleted;
			this.failedCloudItemsSeen = new HashSet<string>();
			this.cloudStatistics = new CloudStatistics();
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000413 RID: 1043 RVA: 0x00014431 File Offset: 0x00012631
		// (remove) Token: 0x06000414 RID: 1044 RVA: 0x0001443A File Offset: 0x0001263A
		internal event EventHandler<RemoteServerRoundtripCompleteEventArgs> RemoteServerRoundtripCompleteEvent
		{
			add
			{
				this.InternalRemoteServerRoundtripCompleteEvent += value;
			}
			remove
			{
				this.InternalRemoteServerRoundtripCompleteEvent -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000415 RID: 1045 RVA: 0x00014444 File Offset: 0x00012644
		// (remove) Token: 0x06000416 RID: 1046 RVA: 0x0001447C File Offset: 0x0001267C
		private event EventHandler<DownloadCompleteEventArgs> DownloadCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000417 RID: 1047 RVA: 0x000144B4 File Offset: 0x000126B4
		// (remove) Token: 0x06000418 RID: 1048 RVA: 0x000144EC File Offset: 0x000126EC
		private event EventHandler<RemoteServerRoundtripCompleteEventArgs> InternalRemoteServerRoundtripCompleteEvent;

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00014521 File Offset: 0x00012721
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0001452F File Offset: 0x0001272F
		internal ISimpleStateStorage StateStorage
		{
			get
			{
				base.CheckDisposed();
				return this.stateStorage;
			}
			set
			{
				base.CheckDisposed();
				this.stateStorage = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001453E File Offset: 0x0001273E
		internal ByteQuantifiedSize BytesDownloaded
		{
			get
			{
				base.CheckDisposed();
				return this.bytesDownloaded;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001454C File Offset: 0x0001274C
		internal HashSet<string> FailedCloudItemsSeen
		{
			get
			{
				base.CheckDisposed();
				return this.failedCloudItemsSeen;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001455A File Offset: 0x0001275A
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00014568 File Offset: 0x00012768
		internal CloudStatistics CloudStatistics
		{
			get
			{
				base.CheckDisposed();
				return this.cloudStatistics;
			}
			set
			{
				base.CheckDisposed();
				this.cloudStatistics = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014577 File Offset: 0x00012777
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00014585 File Offset: 0x00012785
		public bool HasNoChangesOnCloud
		{
			get
			{
				base.CheckDisposed();
				return this.hasNoChangesOnCloud;
			}
			set
			{
				base.CheckDisposed();
				this.hasNoChangesOnCloud = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00014594 File Offset: 0x00012794
		public BaseWatermark BaseWatermark
		{
			get
			{
				base.CheckDisposed();
				return this.baseWatermark;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000145A2 File Offset: 0x000127A2
		public void SetBaseWatermark(BaseWatermark newWatermark)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("newWatermark", newWatermark);
			this.baseWatermark = newWatermark;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000145BC File Offset: 0x000127BC
		internal void InternalOnDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.bytesDownloaded += new ByteQuantifiedSize((ulong)e.BytesDownloaded);
			if (this.DownloadCompleted != null)
			{
				this.DownloadCompleted(sender, e);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000145F0 File Offset: 0x000127F0
		internal override void OnRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.OnRoundtripComplete(sender, roundtripCompleteEventArgs);
			RemoteServerRoundtripCompleteEventArgs e = (RemoteServerRoundtripCompleteEventArgs)roundtripCompleteEventArgs;
			if (this.InternalRemoteServerRoundtripCompleteEvent != null)
			{
				this.InternalRemoteServerRoundtripCompleteEvent(sender, e);
			}
		}

		// Token: 0x040001FA RID: 506
		internal static readonly int NoItemOrFolderCount = -1;

		// Token: 0x040001FB RID: 507
		private readonly HashSet<string> failedCloudItemsSeen;

		// Token: 0x040001FC RID: 508
		private ISimpleStateStorage stateStorage;

		// Token: 0x040001FD RID: 509
		private ByteQuantifiedSize bytesDownloaded;

		// Token: 0x040001FE RID: 510
		private bool hasNoChangesOnCloud;

		// Token: 0x040001FF RID: 511
		private BaseWatermark baseWatermark;

		// Token: 0x04000200 RID: 512
		private CloudStatistics cloudStatistics;
	}
}
