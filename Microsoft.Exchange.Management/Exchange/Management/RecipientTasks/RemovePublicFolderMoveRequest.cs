using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA9 RID: 3241
	[Cmdlet("Remove", "PublicFolderMoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemovePublicFolderMoveRequest : RemoveRequest<PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x06007C66 RID: 31846 RVA: 0x001FDA1C File Offset: 0x001FBC1C
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new PublicFolderMoveRequest(entry).ToString();
		}
	}
}
