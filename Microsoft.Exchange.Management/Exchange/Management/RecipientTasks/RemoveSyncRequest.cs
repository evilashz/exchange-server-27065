using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC3 RID: 3267
	[Cmdlet("Remove", "SyncRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveSyncRequest : RemoveRequest<SyncRequestIdParameter>
	{
		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x06007D62 RID: 32098 RVA: 0x00200814 File Offset: 0x001FEA14
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.Identity.MailboxId);
			}
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x00200826 File Offset: 0x001FEA26
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Identity != null && this.Identity.OrganizationId != null)
			{
				base.CurrentOrganizationId = this.Identity.OrganizationId;
			}
			return base.CreateSession();
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x0020085A File Offset: 0x001FEA5A
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new SyncRequest(entry).ToString();
		}
	}
}
