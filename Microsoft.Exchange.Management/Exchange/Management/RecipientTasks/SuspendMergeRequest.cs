using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C89 RID: 3209
	[Cmdlet("Suspend", "MergeRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMergeRequest : SuspendRequest<MergeRequestIdParameter>
	{
	}
}
