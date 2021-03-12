using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionWorkItemEventArgs<R> : SubscriptionEventArgs<R> where R : SubscriptionEventResult
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public SubscriptionWorkItemEventArgs(R result, SyncLogSession syncLogSession, Guid subscriptionId, Exception workItemResultException, StoreObjectId subscriptionMessageId, Guid mailboxGuid, string userLegacyDn, Guid tenantGuid, OrganizationId organizationId) : base(syncLogSession, result)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", syncLogSession);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDn", userLegacyDn);
			this.subscriptionId = subscriptionId;
			this.workItemResultException = workItemResultException;
			this.subscriptionMessageId = subscriptionMessageId;
			this.mailboxGuid = mailboxGuid;
			this.userLegacyDn = userLegacyDn;
			this.tenantGuid = tenantGuid;
			this.organizationId = organizationId;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008E3E File Offset: 0x0000703E
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008E46 File Offset: 0x00007046
		public Exception WorkItemResultException
		{
			get
			{
				return this.workItemResultException;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008E4E File Offset: 0x0000704E
		public StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008E56 File Offset: 0x00007056
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008E5E File Offset: 0x0000705E
		public string UserLegacyDn
		{
			get
			{
				return this.userLegacyDn;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00008E66 File Offset: 0x00007066
		public Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008E6E File Offset: 0x0000706E
		internal OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x04000103 RID: 259
		private readonly Guid subscriptionId;

		// Token: 0x04000104 RID: 260
		private readonly Exception workItemResultException;

		// Token: 0x04000105 RID: 261
		private readonly StoreObjectId subscriptionMessageId;

		// Token: 0x04000106 RID: 262
		private readonly Guid mailboxGuid;

		// Token: 0x04000107 RID: 263
		private readonly string userLegacyDn;

		// Token: 0x04000108 RID: 264
		private readonly Guid tenantGuid;

		// Token: 0x04000109 RID: 265
		private readonly OrganizationId organizationId;
	}
}
