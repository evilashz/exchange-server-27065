using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C91 RID: 3217
	[Cmdlet("Suspend", "MailboxExportRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMailboxExportRequest : SuspendRequest<MailboxExportRequestIdParameter>
	{
	}
}
