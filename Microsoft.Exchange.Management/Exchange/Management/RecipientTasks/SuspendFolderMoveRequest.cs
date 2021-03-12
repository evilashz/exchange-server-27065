using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6F RID: 3183
	[Cmdlet("Suspend", "FolderMoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendFolderMoveRequest : SuspendRequest<FolderMoveRequestIdParameter>
	{
	}
}
