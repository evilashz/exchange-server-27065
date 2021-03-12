using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CBD RID: 3261
	[Cmdlet("Get", "SyncRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncRequestStatistics : GetRequestStatistics<SyncRequestIdParameter, SyncRequestStatistics>
	{
		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x06007D05 RID: 32005 RVA: 0x001FF67E File Offset: 0x001FD87E
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(base.Identity.MailboxId);
			}
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x001FF690 File Offset: 0x001FD890
		protected override IConfigDataProvider CreateSession()
		{
			if (base.Identity != null && base.Identity.OrganizationId != null)
			{
				base.CurrentOrganizationId = base.Identity.OrganizationId;
			}
			return base.CreateSession();
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x001FF6C4 File Offset: 0x001FD8C4
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryTargetUserNotNull(index);
		}
	}
}
