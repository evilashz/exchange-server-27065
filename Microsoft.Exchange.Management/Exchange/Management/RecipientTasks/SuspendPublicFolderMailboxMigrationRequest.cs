using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB9 RID: 3257
	[Cmdlet("Suspend", "PublicFolderMailboxMigrationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendPublicFolderMailboxMigrationRequest : SuspendRequest<PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x06007CF8 RID: 31992 RVA: 0x001FF509 File Offset: 0x001FD709
		protected override void CheckIndexEntry()
		{
		}

		// Token: 0x04003DAA RID: 15786
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";
	}
}
