using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA5 RID: 3237
	[Cmdlet("Suspend", "MailboxRestoreRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMailboxRestoreRequest : SuspendRequest<MailboxRestoreRequestIdParameter>
	{
	}
}
