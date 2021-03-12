using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9E RID: 3230
	[Cmdlet("Suspend", "MailboxRelocationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMailboxRelocationRequest : SuspendRequest<MailboxRelocationRequestIdParameter>
	{
	}
}
