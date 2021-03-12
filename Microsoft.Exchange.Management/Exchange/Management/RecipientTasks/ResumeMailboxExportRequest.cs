using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C8F RID: 3215
	[Cmdlet("Resume", "MailboxExportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMailboxExportRequest : ResumeRequest<MailboxExportRequestIdParameter>
	{
	}
}
