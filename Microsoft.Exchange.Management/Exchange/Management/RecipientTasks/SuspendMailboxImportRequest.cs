using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C98 RID: 3224
	[Cmdlet("Suspend", "MailboxImportRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMailboxImportRequest : SuspendRequest<MailboxImportRequestIdParameter>
	{
	}
}
