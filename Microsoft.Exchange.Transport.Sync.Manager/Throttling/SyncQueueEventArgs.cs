using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncQueueEventArgs : EventArgs
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00018D4E File Offset: 0x00016F4E
		private SyncQueueEventArgs(Guid databaseGuid, TimeSpan syncInterval, int numberOfItemsChanged, TimeSpan dispatchLagTime)
		{
			this.databaseGuid = databaseGuid;
			this.syncInterval = syncInterval;
			this.numberOfItemsChanged = numberOfItemsChanged;
			this.dispatchLagTime = dispatchLagTime;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00018D73 File Offset: 0x00016F73
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00018D7B File Offset: 0x00016F7B
		public TimeSpan SyncInterval
		{
			get
			{
				return this.syncInterval;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00018D83 File Offset: 0x00016F83
		public int NumberOfItemsChanged
		{
			get
			{
				return this.numberOfItemsChanged;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00018D8B File Offset: 0x00016F8B
		public TimeSpan DispatchLagTime
		{
			get
			{
				return this.dispatchLagTime;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00018D93 File Offset: 0x00016F93
		public static SyncQueueEventArgs CreateReportSyncQueueDispatchLagTimeEventArgs(TimeSpan dispatchLagTime)
		{
			SyncUtilities.ThrowIfArgumentLessThanZero("dispatchLagTime", dispatchLagTime);
			return new SyncQueueEventArgs(Guid.Empty, TimeSpan.MinValue, 0, dispatchLagTime);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00018DB1 File Offset: 0x00016FB1
		public static SyncQueueEventArgs CreateSubscriptionAddedEventArgs(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			return new SyncQueueEventArgs(databaseGuid, TimeSpan.MinValue, 1, TimeSpan.MinValue);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00018DCF File Offset: 0x00016FCF
		public static SyncQueueEventArgs CreateSubscriptionRemovedEventArgs(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			return new SyncQueueEventArgs(databaseGuid, TimeSpan.MinValue, -1, TimeSpan.MinValue);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00018DED File Offset: 0x00016FED
		public static SyncQueueEventArgs CreateSubscriptionEnqueuedEventArgs(Guid databaseGuid, TimeSpan syncInterval)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentLessThanZero("syncInterval", syncInterval);
			return new SyncQueueEventArgs(databaseGuid, syncInterval, 1, TimeSpan.MinValue);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00018E12 File Offset: 0x00017012
		public static SyncQueueEventArgs CreateSubscriptionDequeuedEventArgs(Guid databaseGuid, TimeSpan syncInterval)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentLessThanZero("syncInterval", syncInterval);
			return new SyncQueueEventArgs(databaseGuid, syncInterval, -1, TimeSpan.MinValue);
		}

		// Token: 0x04000234 RID: 564
		private readonly Guid databaseGuid;

		// Token: 0x04000235 RID: 565
		private readonly TimeSpan syncInterval;

		// Token: 0x04000236 RID: 566
		private readonly int numberOfItemsChanged;

		// Token: 0x04000237 RID: 567
		private readonly TimeSpan dispatchLagTime;
	}
}
