using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C8E RID: 3214
	[Cmdlet("Remove", "MailboxExportRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMailboxExportRequest : RemoveRequest<MailboxExportRequestIdParameter>
	{
		// Token: 0x06007BA4 RID: 31652 RVA: 0x001FADA1 File Offset: 0x001F8FA1
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new MailboxExportRequest(entry).ToString();
		}
	}
}
