using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAA RID: 3242
	[Cmdlet("Resume", "PublicFolderMoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumePublicFolderMoveRequest : ResumeRequest<PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x06007C68 RID: 31848 RVA: 0x001FDA31 File Offset: 0x001FBC31
		protected override void CheckIndexEntry()
		{
		}
	}
}
