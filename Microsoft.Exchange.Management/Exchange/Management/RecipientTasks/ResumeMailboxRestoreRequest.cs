using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA3 RID: 3235
	[Cmdlet("Resume", "MailboxRestoreRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMailboxRestoreRequest : ResumeRequest<MailboxRestoreRequestIdParameter>
	{
	}
}
