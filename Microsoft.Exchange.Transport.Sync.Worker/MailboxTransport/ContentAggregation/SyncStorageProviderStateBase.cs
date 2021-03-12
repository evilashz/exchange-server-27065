using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SyncStorageProviderStateBase : DisposeTrackableBase
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00014186 File Offset: 0x00012386
		public int TotalSuccessfulRoundtrips
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.TotalSuccessfulRoundtrips;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00014199 File Offset: 0x00012399
		internal TimeSpan AverageSuccessfulRoundtripTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageSuccessfulRoundtripTime;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000141AC File Offset: 0x000123AC
		public int TotalUnsuccessfulRoundtrips
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.TotalUnsuccessfulRoundtrips;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000141BF File Offset: 0x000123BF
		internal TimeSpan AverageUnsuccessfulRoundtripTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageUnsuccessfulRoundtripTime;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000141D2 File Offset: 0x000123D2
		internal SyncStorageProviderStateBase(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.subscription = subscription;
			this.syncLogSession = syncLogSession;
			this.underRecovery = underRecovery;
			this.connectionStatistics = new SyncStorageProviderConnectionStatistics();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00014210 File Offset: 0x00012410
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0001421E File Offset: 0x0001241E
		internal ISyncWorkerData Subscription
		{
			get
			{
				base.CheckDisposed();
				return this.subscription;
			}
			set
			{
				base.CheckDisposed();
				SyncUtilities.ThrowIfArgumentNull("subscription", value);
				this.subscription = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00014238 File Offset: 0x00012438
		internal SyncLogSession SyncLogSession
		{
			get
			{
				base.CheckDisposed();
				return this.syncLogSession;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00014246 File Offset: 0x00012446
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00014254 File Offset: 0x00012454
		internal IList<SyncChangeEntry> Changes
		{
			get
			{
				base.CheckDisposed();
				return this.changes;
			}
			set
			{
				base.CheckDisposed();
				SyncUtilities.ThrowIfArgumentNull("changeList", value);
				this.changes = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001426E File Offset: 0x0001246E
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0001427C File Offset: 0x0001247C
		internal SyncChangeEntry ItemBeingRetrieved
		{
			get
			{
				base.CheckDisposed();
				return this.itemBeingRetrieved;
			}
			set
			{
				base.CheckDisposed();
				this.itemBeingRetrieved = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001428B File Offset: 0x0001248B
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00014299 File Offset: 0x00012499
		internal bool HasPermanentSyncErrors
		{
			get
			{
				base.CheckDisposed();
				return this.hasPermanentSyncErrors;
			}
			set
			{
				base.CheckDisposed();
				this.hasPermanentSyncErrors = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000142A8 File Offset: 0x000124A8
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000142B6 File Offset: 0x000124B6
		internal bool HasTransientSyncErrors
		{
			get
			{
				base.CheckDisposed();
				return this.hasTransientSyncErrors;
			}
			set
			{
				base.CheckDisposed();
				this.hasTransientSyncErrors = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000142C5 File Offset: 0x000124C5
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000142D3 File Offset: 0x000124D3
		internal ISyncStorageProviderItemRetriever ItemRetriever
		{
			get
			{
				base.CheckDisposed();
				return this.itemRetriever;
			}
			set
			{
				base.CheckDisposed();
				this.itemRetriever = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000142E2 File Offset: 0x000124E2
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000142F0 File Offset: 0x000124F0
		internal object ItemRetrieverState
		{
			get
			{
				base.CheckDisposed();
				return this.itemRetrieverState;
			}
			set
			{
				base.CheckDisposed();
				this.itemRetrieverState = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000142FF File Offset: 0x000124FF
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0001430D File Offset: 0x0001250D
		internal bool UnderRecovery
		{
			get
			{
				base.CheckDisposed();
				return this.underRecovery;
			}
			set
			{
				base.CheckDisposed();
				this.underRecovery = value;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001431C File Offset: 0x0001251C
		public override string ToString()
		{
			return string.Format("Change count: {0}, hasPermanentErrors: {1}, hasTransientErrors: {2}", (this.changes != null) ? this.changes.Count : 0, this.hasPermanentSyncErrors, this.hasTransientSyncErrors);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001435C File Offset: 0x0001255C
		internal void Add(SyncChangeEntry entry)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("entry", entry);
			lock (this.changes)
			{
				this.changes.Add(entry);
				if (entry.Exception != null)
				{
					if (entry.Exception is TransientException)
					{
						this.hasTransientSyncErrors = true;
					}
					else if (!(entry.Exception is OperationCanceledException))
					{
						this.hasPermanentSyncErrors = true;
					}
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000143E8 File Offset: 0x000125E8
		internal virtual void OnRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("roundtripCompleteEventArgs", roundtripCompleteEventArgs);
			this.connectionStatistics.OnRoundtripComplete(sender, roundtripCompleteEventArgs);
		}

		// Token: 0x040001F0 RID: 496
		private readonly SyncLogSession syncLogSession;

		// Token: 0x040001F1 RID: 497
		private ISyncWorkerData subscription;

		// Token: 0x040001F2 RID: 498
		private IList<SyncChangeEntry> changes;

		// Token: 0x040001F3 RID: 499
		private bool hasPermanentSyncErrors;

		// Token: 0x040001F4 RID: 500
		private bool hasTransientSyncErrors;

		// Token: 0x040001F5 RID: 501
		private SyncChangeEntry itemBeingRetrieved;

		// Token: 0x040001F6 RID: 502
		private ISyncStorageProviderItemRetriever itemRetriever;

		// Token: 0x040001F7 RID: 503
		private object itemRetrieverState;

		// Token: 0x040001F8 RID: 504
		private bool underRecovery;

		// Token: 0x040001F9 RID: 505
		protected SyncStorageProviderConnectionStatistics connectionStatistics;
	}
}
