using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9C RID: 3228
	[Cmdlet("Resume", "MailboxRelocationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMailboxRelocationRequest : ResumeRequest<MailboxRelocationRequestIdParameter>
	{
	}
}
