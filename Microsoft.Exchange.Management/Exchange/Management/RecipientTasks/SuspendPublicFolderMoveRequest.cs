using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAC RID: 3244
	[Cmdlet("Suspend", "PublicFolderMoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendPublicFolderMoveRequest : SuspendRequest<PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x06007C75 RID: 31861 RVA: 0x001FDAFF File Offset: 0x001FBCFF
		protected override void CheckIndexEntry()
		{
		}
	}
}
