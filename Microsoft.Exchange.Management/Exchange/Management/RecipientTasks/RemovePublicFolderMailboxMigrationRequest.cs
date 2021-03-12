using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CBB RID: 3259
	[Cmdlet("Remove", "PublicFolderMailboxMigrationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemovePublicFolderMailboxMigrationRequest : RemoveRequest<PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x06007CFB RID: 31995 RVA: 0x001FF51B File Offset: 0x001FD71B
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new PublicFolderMailboxMigrationRequest(entry).ToString();
		}

		// Token: 0x04003DAB RID: 15787
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";
	}
}
