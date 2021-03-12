using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9B RID: 3227
	[Cmdlet("Remove", "MailboxRelocationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMailboxRelocationRequest : RemoveRequest<MailboxRelocationRequestIdParameter>
	{
		// Token: 0x06007C00 RID: 31744 RVA: 0x001FC277 File Offset: 0x001FA477
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new MailboxRelocationRequest(entry).ToString();
		}
	}
}
