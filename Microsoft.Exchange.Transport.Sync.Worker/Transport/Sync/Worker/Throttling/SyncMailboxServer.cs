using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncMailboxServer : SyncResource
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000C98C File Offset: 0x0000AB8C
		protected SyncMailboxServer(Guid mailboxServerGuid, string mailboxServer, SyncLogSession syncLogSession) : base(syncLogSession, string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
		{
			mailboxServerGuid,
			mailboxServer
		}))
		{
			base.Initialize();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000C9CA File Offset: 0x0000ABCA
		protected override int MaxConcurrentWorkInUnknownState
		{
			get
			{
				return AggregationConfiguration.Instance.MaxItemsForMailboxServerInUnknownHealthState;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000C9D6 File Offset: 0x0000ABD6
		protected override SubscriptionSubmissionResult ResourceHealthUnknownResult
		{
			get
			{
				return SubscriptionSubmissionResult.MailboxServerCpuUnknown;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		protected override SubscriptionSubmissionResult MaxConcurrentWorkAgainstResourceLimitReachedResult
		{
			get
			{
				return SubscriptionSubmissionResult.MaxConcurrentMailboxSubmissions;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		internal static SyncMailboxServer CreateSyncMailboxServer(Guid mailboxServerGuid, string mailboxServer, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxServerGuid", mailboxServerGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			return new SyncMailboxServer(mailboxServerGuid, mailboxServer, syncLogSession);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CA18 File Offset: 0x0000AC18
		protected override SyncResourceMonitor[] InitializeHealthMonitoring()
		{
			ResourceKey local = ProcessorResourceKey.Local;
			return new SyncResourceMonitor[]
			{
				this.CreateSyncResourceMonitor(local, SyncResourceMonitorType.MailboxCPU)
			};
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CA3E File Offset: 0x0000AC3E
		protected virtual SyncResourceMonitor CreateSyncResourceMonitor(ResourceKey resourceKey, SyncResourceMonitorType syncResourceMonitorType)
		{
			return new SyncResourceMonitor(base.SyncLogSession, resourceKey, syncResourceMonitorType);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CA4D File Offset: 0x0000AC4D
		protected override SubscriptionSubmissionResult GetResultForResourceUnhealthy(SyncResourceMonitorType syncResourceMonitorType)
		{
			return SubscriptionSubmissionResult.MailboxServerCpuUnhealthy;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CA51 File Offset: 0x0000AC51
		protected override bool CanAcceptWorkBasedOnResourceSpecificChecks(out SubscriptionSubmissionResult result)
		{
			if (!base.CanAddOneMoreConcurrentRequestToResource())
			{
				result = this.MaxConcurrentWorkAgainstResourceLimitReachedResult;
				return false;
			}
			result = SubscriptionSubmissionResult.Success;
			return true;
		}
	}
}
