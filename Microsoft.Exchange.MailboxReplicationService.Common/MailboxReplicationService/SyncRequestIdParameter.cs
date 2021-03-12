using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public sealed class SyncRequestIdParameter : MRSRequestIdParameter
	{
		// Token: 0x060010B8 RID: 4280 RVA: 0x00027113 File Offset: 0x00025313
		public SyncRequestIdParameter()
		{
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0002711B File Offset: 0x0002531B
		public SyncRequestIdParameter(SyncRequest request) : base(request)
		{
			base.MailboxId = request.Mailbox;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00027130 File Offset: 0x00025330
		public SyncRequestIdParameter(RequestJobObjectId requestJobId) : base(requestJobId)
		{
			if (requestJobId.User != null)
			{
				base.MailboxId = requestJobId.User.ObjectId;
				return;
			}
			if (requestJobId.TargetUser != null)
			{
				base.MailboxId = requestJobId.TargetUser.ObjectId;
				return;
			}
			if (requestJobId.IndexEntry != null)
			{
				base.MailboxId = (requestJobId.IndexEntry.TargetUserId ?? requestJobId.IndexEntry.RequestIndexId.Mailbox);
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000271A5 File Offset: 0x000253A5
		public SyncRequestIdParameter(SyncRequestStatistics requestStats) : base(requestStats)
		{
			base.MailboxId = requestStats.TargetUserId;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000271BA File Offset: 0x000253BA
		public SyncRequestIdParameter(RequestIndexEntryObjectId identity) : base(identity)
		{
			if (identity.IndexId != null)
			{
				base.MailboxId = identity.IndexId.Mailbox;
				return;
			}
			if (identity.RequestObject != null)
			{
				base.MailboxId = identity.RequestObject.TargetMailbox;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000271F6 File Offset: 0x000253F6
		public SyncRequestIdParameter(string request) : base(request)
		{
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000271FF File Offset: 0x000253FF
		internal SyncRequestIdParameter(Guid requestGuid, OrganizationId orgId, string mailboxName) : base(requestGuid, orgId, mailboxName)
		{
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002720A File Offset: 0x0002540A
		public static SyncRequestIdParameter Parse(string request)
		{
			return new SyncRequestIdParameter(request);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00027214 File Offset: 0x00025414
		public static SyncRequestIdParameter Create(ADUser requestOwner, string requestName)
		{
			ArgumentValidator.ThrowIfNull("requestName", requestName);
			ArgumentValidator.ThrowIfNull("requestOwner", requestOwner);
			ArgumentValidator.ThrowIfNull("requestOwner.OrganizationId", requestOwner.OrganizationId);
			ArgumentValidator.ThrowIfNull("requestOwner.OrganizationId.OrganizationalUnit", requestOwner.OrganizationId.OrganizationalUnit);
			return new SyncRequestIdParameter(string.Format("{0}/{1}/{2}\\{3}", new object[]
			{
				requestOwner.OrganizationId.OrganizationalUnit.Parent,
				requestOwner.OrganizationId.OrganizationalUnit.Name,
				requestOwner.Name,
				requestName
			}));
		}
	}
}
