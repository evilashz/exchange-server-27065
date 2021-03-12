using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C69 RID: 3177
	[Cmdlet("Remove", "FolderMoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveFolderMoveRequest : RemoveRequest<FolderMoveRequestIdParameter>
	{
		// Token: 0x0600794A RID: 31050 RVA: 0x001EE89C File Offset: 0x001ECA9C
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new FolderMoveRequest(entry).ToString();
		}
	}
}
