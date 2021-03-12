using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionWorkItemCompletedEventArgs : SubscriptionWorkItemEventArgs<SubscriptionWorkItemCompletedEventResult>
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00008E78 File Offset: 0x00007078
		public SubscriptionWorkItemCompletedEventArgs(SyncLogSession syncLogSession, Guid subscriptionId, ISyncWorkerData subscription, bool isSyncNow, Exception workItemResultException, StoreObjectId subscriptionMessageId, Guid mailboxGuid, string userLegacyDn, Guid tenantGuid, OrganizationId organizationId, MailboxSession mailboxSession) : base(new SubscriptionWorkItemCompletedEventResult(), syncLogSession, subscriptionId, workItemResultException, subscriptionMessageId, mailboxGuid, userLegacyDn, tenantGuid, organizationId)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			this.subscription = subscription;
			this.mailboxSession = mailboxSession;
			this.isSyncNow = isSyncNow;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008EC0 File Offset: 0x000070C0
		public bool IsSyncNow
		{
			get
			{
				return this.isSyncNow;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008EC8 File Offset: 0x000070C8
		public ISyncWorkerData Subscription
		{
			get
			{
				return this.subscription;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008ED0 File Offset: 0x000070D0
		public MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x0400010A RID: 266
		private readonly ISyncWorkerData subscription;

		// Token: 0x0400010B RID: 267
		private readonly MailboxSession mailboxSession;

		// Token: 0x0400010C RID: 268
		private readonly bool isSyncNow;
	}
}
