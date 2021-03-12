using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncTransportServer : SyncResource
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000E680 File Offset: 0x0000C880
		protected SyncTransportServer(SyncLogSession syncLogSession, int maxPendingMessages, int maxPendingMessagesPerUser) : base(syncLogSession, "TransportServer:" + Environment.MachineName)
		{
			this.maxPendingMessages = maxPendingMessages;
			this.maxPendingMessagesPerUser = maxPendingMessagesPerUser;
			base.Initialize();
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		private protected SyncTransportResourceMonitor SyncTransportResourceMonitor { protected get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000E6C5 File Offset: 0x0000C8C5
		private protected SyncTransportUserResourceMonitor SyncTransportUserResourceMonitor { protected get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000E6CE File Offset: 0x0000C8CE
		protected override int MaxConcurrentWorkInUnknownState
		{
			get
			{
				return AggregationConfiguration.Instance.MaxItemsForTransportServerInUnknownHealthState;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000E6DA File Offset: 0x0000C8DA
		protected override SubscriptionSubmissionResult ResourceHealthUnknownResult
		{
			get
			{
				return SubscriptionSubmissionResult.TransportQueueHealthUnknown;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		protected override SubscriptionSubmissionResult MaxConcurrentWorkAgainstResourceLimitReachedResult
		{
			get
			{
				throw new InvalidOperationException("SuggestedConcurrency is not to be supported against SyncTransportServer.");
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		internal static SyncTransportServer CreateSyncTransportServer(SyncLogSession syncLogSession, int maxPendingMessages, int maxPendingMessagesPerUser)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentLessThanEqualToZero("maxPendingMessages", maxPendingMessages);
			SyncUtilities.ThrowIfArgumentLessThanEqualToZero("maxPendingMessagesPerUser", maxPendingMessagesPerUser);
			return new SyncTransportServer(syncLogSession, maxPendingMessages, maxPendingMessagesPerUser);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E724 File Offset: 0x0000C924
		internal override void RemoveWorkItem(AggregationWorkItem workItem)
		{
			base.RemoveWorkItem(workItem);
			int maxDownloadItemsPerConnection = AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(workItem.AggregationType);
			int num = 0;
			if (workItem.SyncHealthData != null)
			{
				num = workItem.SyncHealthData.TotalItemsSubmittedToTransport;
			}
			num = Math.Max(0, num);
			num = Math.Min(num, maxDownloadItemsPerConnection);
			int num2 = maxDownloadItemsPerConnection - num;
			if (num2 > 0)
			{
				base.SyncLogSession.LogDebugging((TSLID)344UL, "Reducing load by: {0} for subscription {1} in userMailbox {2}.", new object[]
				{
					num2,
					workItem.SubscriptionId,
					workItem.UserMailboxGuid
				});
				this.SyncTransportResourceMonitor.RemoveLoad(num2);
				this.SyncTransportUserResourceMonitor.RemoveLoad(workItem.UserMailboxGuid, num2);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E894 File Offset: 0x0000CA94
		internal void TrackMailItem(TransportMailItem transportMailItem, Guid userMailboxGuid, Guid subscriptionGuid, string cloudId)
		{
			SyncUtilities.ThrowIfArgumentNull("transportMailItem", transportMailItem);
			SyncUtilities.ThrowIfGuidEmpty("userMailboxGuid", userMailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			base.SyncLogSession.LogDebugging((TSLID)347UL, "Submitted cloudId: {0} for subscription {1} in userMailbox {2}.", new object[]
			{
				cloudId,
				subscriptionGuid,
				userMailboxGuid
			});
			int invocationCount = 0;
			ExDateTime startTime = ExDateTime.UtcNow;
			transportMailItem.OnReleaseFromActive += delegate(TransportMailItem tmi)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow - startTime;
				int num = Interlocked.Increment(ref invocationCount);
				this.SyncLogSession.LogDebugging((TSLID)348UL, "Received notification for cloudId: {0} for subscription {1} in userMailbox {2}. Time to deliver: {3}", new object[]
				{
					cloudId,
					subscriptionGuid,
					userMailboxGuid,
					timeSpan
				});
				bool flag = num == 1;
				if (flag)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveLoadByOneForMailSubmission), userMailboxGuid);
				}
			};
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E96C File Offset: 0x0000CB6C
		protected override SyncResourceMonitor[] InitializeHealthMonitoring()
		{
			this.SyncTransportResourceMonitor = this.CreateSyncTransportResourceMonitor(base.SyncLogSession, this.maxPendingMessages);
			this.SyncTransportUserResourceMonitor = this.CreateSyncTransportUserResourceMonitor(base.SyncLogSession, this.maxPendingMessagesPerUser);
			return new SyncResourceMonitor[]
			{
				this.SyncTransportResourceMonitor,
				this.SyncTransportUserResourceMonitor
			};
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		protected override SubscriptionSubmissionResult GetResultForResourceUnhealthy(SyncResourceMonitorType syncResourceMonitorType)
		{
			switch (syncResourceMonitorType)
			{
			case SyncResourceMonitorType.ServerTransportQueue:
				return SubscriptionSubmissionResult.ServerTransportQueueUnhealthy;
			case SyncResourceMonitorType.UserTransportQueue:
				return SubscriptionSubmissionResult.UserTransportQueueUnhealthy;
			default:
				throw new InvalidOperationException("Invalid syncResourceMonitorType found: " + syncResourceMonitorType);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EA04 File Offset: 0x0000CC04
		protected override bool CanAcceptWorkBasedOnResourceSpecificChecks(out SubscriptionSubmissionResult result)
		{
			result = SubscriptionSubmissionResult.Success;
			return true;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		protected override void AddWorkItem(AggregationWorkItem workItem)
		{
			base.AddWorkItem(workItem);
			int maxDownloadItemsPerConnection = AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(workItem.AggregationType);
			this.SyncTransportResourceMonitor.AddLoad(maxDownloadItemsPerConnection);
			this.SyncTransportUserResourceMonitor.AddLoad(workItem.UserMailboxGuid, maxDownloadItemsPerConnection);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EA4F File Offset: 0x0000CC4F
		protected virtual SyncTransportResourceMonitor CreateSyncTransportResourceMonitor(SyncLogSession syncLogSession, int maxPendingMessages)
		{
			return new SyncTransportResourceMonitor(syncLogSession, maxPendingMessages);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000EA58 File Offset: 0x0000CC58
		protected virtual SyncTransportUserResourceMonitor CreateSyncTransportUserResourceMonitor(SyncLogSession syncLogSession, int maxPendingMessagesPerUser)
		{
			return new SyncTransportUserResourceMonitor(syncLogSession, maxPendingMessagesPerUser);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EA64 File Offset: 0x0000CC64
		private void RemoveLoadByOneForMailSubmission(object state)
		{
			Guid userMailboxGuid = (Guid)state;
			this.SyncTransportResourceMonitor.RemoveLoad(1);
			this.SyncTransportUserResourceMonitor.RemoveLoad(userMailboxGuid, 1);
		}

		// Token: 0x04000198 RID: 408
		private readonly int maxPendingMessages;

		// Token: 0x04000199 RID: 409
		private readonly int maxPendingMessagesPerUser;
	}
}
