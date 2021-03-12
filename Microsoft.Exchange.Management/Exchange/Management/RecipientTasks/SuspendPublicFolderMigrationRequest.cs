using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB4 RID: 3252
	[Cmdlet("Suspend", "PublicFolderMigrationRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendPublicFolderMigrationRequest : SuspendRequest<PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x06007CAF RID: 31919 RVA: 0x001FE6BB File Offset: 0x001FC8BB
		protected override void CheckIndexEntry()
		{
		}
	}
}
