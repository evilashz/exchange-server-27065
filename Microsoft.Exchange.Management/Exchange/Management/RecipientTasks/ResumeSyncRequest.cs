using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC1 RID: 3265
	[Cmdlet("Resume", "SyncRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeSyncRequest : ResumeRequest<SyncRequestIdParameter>
	{
		// Token: 0x06007D5C RID: 32092 RVA: 0x00200778 File Offset: 0x001FE978
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Identity != null && this.Identity.OrganizationId != null)
			{
				base.CurrentOrganizationId = this.Identity.OrganizationId;
			}
			return base.CreateSession();
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x06007D5D RID: 32093 RVA: 0x002007AC File Offset: 0x001FE9AC
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.Identity.MailboxId);
			}
		}
	}
}
