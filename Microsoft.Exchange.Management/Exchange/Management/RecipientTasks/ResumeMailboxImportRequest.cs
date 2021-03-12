using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C96 RID: 3222
	[Cmdlet("Resume", "MailboxImportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMailboxImportRequest : ResumeRequest<MailboxImportRequestIdParameter>
	{
	}
}
