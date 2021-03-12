using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB1 RID: 3249
	[Cmdlet("Remove", "PublicFolderMigrationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemovePublicFolderMigrationRequest : RemoveRequest<PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x06007C9C RID: 31900 RVA: 0x001FE467 File Offset: 0x001FC667
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new PublicFolderMigrationRequest(entry).ToString();
		}
	}
}
