using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB2 RID: 3250
	[Cmdlet("Resume", "PublicFolderMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumePublicFolderMigrationRequest : ResumeRequest<PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x06007C9E RID: 31902 RVA: 0x001FE47C File Offset: 0x001FC67C
		protected override void CheckIndexEntry()
		{
		}
	}
}
