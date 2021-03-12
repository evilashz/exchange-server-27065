using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncTransportUserResourceMonitor : SyncResourceMonitor
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		public SyncTransportUserResourceMonitor(SyncLogSession syncLogSession, int maxPendingMessagesPerUser) : base(syncLogSession, null, SyncResourceMonitorType.UserTransportQueue)
		{
			this.maxPendingMessagesPerUser = maxPendingMessagesPerUser;
			this.CurrentPendingMessagesCountPerUser = new Dictionary<Guid, int>(20);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000EC44 File Offset: 0x0000CE44
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		private protected Dictionary<Guid, int> CurrentPendingMessagesCountPerUser { protected get; private set; }

		// Token: 0x06000311 RID: 785 RVA: 0x0000EC58 File Offset: 0x0000CE58
		internal void AddLoad(Guid userMailboxGuid, int newMessages)
		{
			int num;
			lock (this.syncLock)
			{
				this.CurrentPendingMessagesCountPerUser.TryGetValue(userMailboxGuid, out num);
				this.CurrentPendingMessagesCountPerUser[userMailboxGuid] = num + newMessages;
			}
			base.SyncLogSession.LogDebugging((TSLID)941UL, "Adding load for user {0} from prevLoad {1} by {2}", new object[]
			{
				userMailboxGuid,
				num,
				newMessages
			});
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		internal void RemoveLoad(Guid userMailboxGuid, int countOfMessagesNoLongerPending)
		{
			int num;
			lock (this.syncLock)
			{
				this.CurrentPendingMessagesCountPerUser.TryGetValue(userMailboxGuid, out num);
				int num2 = num - countOfMessagesNoLongerPending;
				num2 = Math.Max(0, num2);
				if (num2 > 0)
				{
					this.CurrentPendingMessagesCountPerUser[userMailboxGuid] = num2;
				}
				else
				{
					this.CurrentPendingMessagesCountPerUser.Remove(userMailboxGuid);
				}
			}
			base.SyncLogSession.LogDebugging((TSLID)1126UL, "Removing load for user {0} from prevLoad {1} by {2}", new object[]
			{
				userMailboxGuid,
				num,
				countOfMessagesNoLongerPending
			});
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		protected override ResourceLoad GetResourceLoad(AggregationWorkItem workItem)
		{
			int num;
			this.CurrentPendingMessagesCountPerUser.TryGetValue(workItem.UserMailboxGuid, out num);
			double num2 = (double)num / (double)this.maxPendingMessagesPerUser;
			if (num2 > 1.0)
			{
				num2 = ResourceLoad.Critical.LoadRatio;
			}
			return new ResourceLoad(num2, null, null);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		protected override IResourceLoadMonitor CreateResourceHealthMonitor(ResourceKey resourceKey)
		{
			return null;
		}

		// Token: 0x0400019F RID: 415
		private readonly object syncLock = new object();

		// Token: 0x040001A0 RID: 416
		private readonly int maxPendingMessagesPerUser;
	}
}
