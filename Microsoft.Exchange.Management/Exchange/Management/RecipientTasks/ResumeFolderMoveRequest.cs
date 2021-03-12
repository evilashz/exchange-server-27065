using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6B RID: 3179
	[Cmdlet("Resume", "FolderMoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeFolderMoveRequest : ResumeRequest<FolderMoveRequestIdParameter>
	{
	}
}
