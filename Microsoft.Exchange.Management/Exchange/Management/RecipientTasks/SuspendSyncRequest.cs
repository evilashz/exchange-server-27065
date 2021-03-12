using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC2 RID: 3266
	[Cmdlet("Suspend", "SyncRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendSyncRequest : SuspendRequest<SyncRequestIdParameter>
	{
		// Token: 0x06007D5F RID: 32095 RVA: 0x002007C6 File Offset: 0x001FE9C6
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Identity != null && this.Identity.OrganizationId != null)
			{
				base.CurrentOrganizationId = this.Identity.OrganizationId;
			}
			return base.CreateSession();
		}

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x06007D60 RID: 32096 RVA: 0x002007FA File Offset: 0x001FE9FA
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.Identity.MailboxId);
			}
		}
	}
}
