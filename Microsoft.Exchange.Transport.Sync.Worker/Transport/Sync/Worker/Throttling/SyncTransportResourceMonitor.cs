using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncTransportResourceMonitor : SyncResourceMonitor
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000EA91 File Offset: 0x0000CC91
		public SyncTransportResourceMonitor(SyncLogSession syncLogSession, int maxPendingMessages) : base(syncLogSession, null, SyncResourceMonitorType.ServerTransportQueue)
		{
			this.maxPendingMessages = maxPendingMessages;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000EAAE File Offset: 0x0000CCAE
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000EAB6 File Offset: 0x0000CCB6
		protected int CurrentPendingMessagesCount { get; set; }

		// Token: 0x0600030A RID: 778 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		internal void AddLoad(int newMessages)
		{
			int num;
			lock (this.syncLock)
			{
				num = (this.CurrentPendingMessagesCount += newMessages);
			}
			base.SyncLogSession.LogVerbose((TSLID)1115UL, "AddLoad: CurrentPendingMessagesCount: {0}, added: {1}", new object[]
			{
				num,
				newMessages
			});
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EB48 File Offset: 0x0000CD48
		internal void RemoveLoad(int countOfMessagesNoLongerPending)
		{
			int num;
			lock (this.syncLock)
			{
				num = this.CurrentPendingMessagesCount - countOfMessagesNoLongerPending;
				this.CurrentPendingMessagesCount = Math.Max(0, num);
			}
			base.SyncLogSession.LogVerbose((TSLID)333UL, "RemoveLoad: CurrentPendingMessagesCount: {0}, removed: {1}", new object[]
			{
				num,
				countOfMessagesNoLongerPending
			});
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		protected override ResourceLoad GetResourceLoad(AggregationWorkItem workItem)
		{
			double num = (double)this.CurrentPendingMessagesCount / (double)this.maxPendingMessages;
			if (num > 1.0)
			{
				num = ResourceLoad.Critical.LoadRatio;
			}
			return new ResourceLoad(num, null, null);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EC17 File Offset: 0x0000CE17
		protected override IResourceLoadMonitor CreateResourceHealthMonitor(ResourceKey resourceKey)
		{
			return null;
		}

		// Token: 0x0400019C RID: 412
		private readonly object syncLock = new object();

		// Token: 0x0400019D RID: 413
		private readonly int maxPendingMessages;
	}
}
