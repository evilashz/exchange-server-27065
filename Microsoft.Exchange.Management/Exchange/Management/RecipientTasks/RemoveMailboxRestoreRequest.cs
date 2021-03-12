using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA2 RID: 3234
	[Cmdlet("Remove", "MailboxRestoreRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMailboxRestoreRequest : RemoveRequest<MailboxRestoreRequestIdParameter>
	{
		// Token: 0x06007C41 RID: 31809 RVA: 0x001FD270 File Offset: 0x001FB470
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new MailboxRestoreRequest(entry).ToString();
		}
	}
}
