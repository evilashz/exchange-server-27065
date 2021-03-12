using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C95 RID: 3221
	[Cmdlet("Remove", "MailboxImportRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMailboxImportRequest : RemoveRequest<MailboxImportRequestIdParameter>
	{
		// Token: 0x06007BD8 RID: 31704 RVA: 0x001FB649 File Offset: 0x001F9849
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new MailboxImportRequest(entry).ToString();
		}
	}
}
